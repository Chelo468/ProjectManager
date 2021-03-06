﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class UsuarioDataProvider : GenericDataProvider
    {
        private static Usuario Mapear(DataRow lector)
        {
            Usuario usuario = new Usuario();

            usuario.id_usuario = lector["id_usuario"] != null ? Convert.ToInt32(lector["id_usuario"].ToString()) : 0;
            usuario.login_name = lector["login_name"] != null ? lector["login_name"].ToString() : "";
            usuario.password = lector["password"] != null ? lector["password"].ToString() : "";
            usuario.token_clave = lector["token_clave"] != null ? lector["token_clave"].ToString() : "";
            usuario.fecha_alta = lector["fecha_alta"] != null ? Convert.ToDateTime(lector["fecha_alta"].ToString()) : new DateTime();
            usuario.email = lector["email"] != null ? lector["email"].ToString() : "";
            usuario.habilitado = lector["habilitado"] != null ? Convert.ToBoolean(lector["habilitado"].ToString()) : false;
            usuario.nombre = lector["nombre"].ToString();
            usuario.apellido = lector["apellido"].ToString();
            usuario.telefono = lector["telefono"].ToString();

            Rol rol = new Rol();
            try
            {
                rol.id_rol = Convert.ToInt32(lector["id_rol"].ToString());
                rol.nombre = lector["nombre_rol"].ToString();
                usuario.roles.Add(rol);
            }
            catch (Exception){}

            return usuario;

        }

       
        public static Usuario getById(int id)
        {
            Usuario usuario = new Usuario();

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("id_usuario", id);

            DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "usuariosGetById", parametros);

            if(usuarioResult.Rows.Count > 0)
            { 
                usuario = Mapear(usuarioResult.Rows[0]);
                usuario.roles = RolDataProvider.getByIdUsuario(usuario.id_usuario);
            }
            
            return usuario;
        }

        public static Usuario getByUserNamePassword(string login_name, string password)
        {
            try
            {
                Usuario usuario = new Usuario();

                SqlParameter[] parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("login_name", login_name);
                parametros[1] = new SqlParameter("password", password);

                DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "usuariosGetByUserNamePassword", parametros);

                if (usuarioResult.Rows.Count > 0)
                    usuario = Mapear(usuarioResult.Rows[0]);

                return usuario;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "UsuarioDataProvider", "getByUserNamePassword");
                throw ex;
            }
            
        }

        public static List<Usuario> getAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "usuariosGetAll", null);

            for (int i = 0; i < usuarioResult.Rows.Count; i++)
            {
                Usuario user = new Usuario();
                user = Mapear(usuarioResult.Rows[i]);

                usuarios.Add(user);
            }

            return usuarios;
        }


        public static List<Usuario> getByFilters(string usuario, int id_rol)
        {
            List<Usuario> usuarios = new List<Usuario>();

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("login_name", usuario);
            parametros[1] = new SqlParameter("id_rol", id_rol);

            DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "usuariosGetByFilters", parametros);

            for (int i = 0; i < usuarioResult.Rows.Count; i++)
            {
                Usuario user = new Usuario();
                user = Mapear(usuarioResult.Rows[i]);
                user.roles = RolDataProvider.getByIdUsuario(user.id_usuario);
                //if (usuarios.Count > 0)
                //{
                //    var usuarioAgregado = usuarios.Where(x => x.id_usuario == user.id_usuario).FirstOrDefault();

                //    if (usuarioAgregado != null)
                //    {
                //        usuarioAgregado.roles.AddRange(user.roles);
                //    }
                //    else
                //    {
                //        usuarios.Add(user);
                //    }
                //}
                //else
                //{
                    usuarios.Add(user);
                //}
            }

            return usuarios;
        }

        public static int crear(Usuario nuevoUsuario)
        {
            try
            {
                int usuario = 0;
                SqlParameter[] parametros = new SqlParameter[9];
                parametros[0] = new SqlParameter("nombre", nuevoUsuario.nombre);
                parametros[1] = new SqlParameter("apellido", nuevoUsuario.apellido);
                parametros[2] = new SqlParameter("login_name", nuevoUsuario.login_name);
                parametros[3] = new SqlParameter("password", nuevoUsuario.password);
                parametros[4] = new SqlParameter("telefono", nuevoUsuario.telefono);
                parametros[5] = new SqlParameter("email", nuevoUsuario.email);
                parametros[6] = new SqlParameter("fecha_alta", nuevoUsuario.fecha_alta);
                parametros[7] = new SqlParameter("habilitado", nuevoUsuario.habilitado);
                parametros[8] = new SqlParameter("token_clave", nuevoUsuario.token_clave);

                DataTable usuarioResult = executeQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "usuariosInsert", parametros);

                if (usuarioResult.Rows.Count > 0)
                    usuario = Convert.ToInt32(usuarioResult.Rows[0][0].ToString());

                return usuario;
            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "UsuarioDataProvider", "crear");
                throw ex;
            }
        }

        public static void updateRoles(Usuario user)
        {
            try
            {
                SqlParameter[] parametrosDelete = new SqlParameter[1];

                parametrosDelete[0] = new SqlParameter("id_usuario", user.id_usuario);

                executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "rolesUsuarioDelete", parametrosDelete);

                SqlParameter[] parametrosInsert = new SqlParameter[2];
                for (int i = 0; i < user.roles.Count; i++)
                {
                    parametrosInsert[0] = new SqlParameter("id_usuario", user.id_usuario);
                    parametrosInsert[1] = new SqlParameter("id_rol", user.roles[i].id_rol);

                    executeNonQueryProc(ConfiguracionDataProvider.obtenerCadenaConexion(), "rolesUsuarioInsert", parametrosInsert);
                }

            }
            catch (Exception ex)
            {
                LogueadorService.loguear(ex.ToString(), "Datos", "UsuarioDataProvider", "crear");
                throw ex;
            }
        }

       



    }
}
