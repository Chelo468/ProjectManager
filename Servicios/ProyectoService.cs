using Datos;
using Entidades;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ProyectoService
    {
        public List<Proyecto> getByIdUser(int id_usuario)
        {
            List<Proyecto> proyectos = ProyectoDataProvider.getByIdUser(id_usuario);

            return proyectos;
        }

        public int crear(Proyecto proyecto, ref string respuesta)
        {
            try
            {
                return ProyectoDataProvider.crear(proyecto);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
                return -1;
            }
            
        }

        public Proyecto getById(int idProyecto, ref string error)
        {
            try
            {
                return ProyectoDataProvider.getById(idProyecto);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public void actualizar(Proyecto proyecto, ref string resultado)
        {
            try
            {
                ProyectoDataProvider.actualizar(proyecto);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
        }

        public void eliminar(Proyecto proyecto, ref string resultado)
        {
            try
            {
                ProyectoDataProvider.eliminar(proyecto);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
        }

        public List<Proyecto> getAll(ref string resultado)
        {
            try
            {
                return ProyectoDataProvider.getAll();
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                return new List<Proyecto>();
            }
        }

        public List<Proyecto_Usuario> getUsersById(int id_proyecto, ref string error)
        {
            try
            {
                return Proyecto_UsuarioDataProvider.getUsersByIdProyecto(id_proyecto);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return new List<Proyecto_Usuario>();
            }
        }

        public void agregarUsuario(int id_proyecto, int id_usuario, int id_rol, ref string resultado)
        {
            try
            {
                Proyecto_UsuarioDataProvider.agregarUsuarioAProyecto(id_proyecto, id_usuario, id_rol);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
        }

        public void eliminarUsuario(int id_proyecto, int id_usuario, int id_rol, ref string resultado)
        {
            try
            {
                Proyecto_UsuarioDataProvider.eliminarUsuarioAProyecto(id_proyecto, id_usuario, id_rol);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
        }
    }
}
