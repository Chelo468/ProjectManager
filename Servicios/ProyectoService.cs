using Datos;
using Entidades;
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

        public Proyecto getById(int idProyecto, int idUsuario, ref string error)
        {
            try
            {
                return ProyectoDataProvider.getById(idProyecto, idUsuario);
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
    }
}
