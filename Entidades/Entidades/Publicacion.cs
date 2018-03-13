using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Publicacion
    {
        public int id_publicacion { get; set; }
        public Proyecto proyecto { get; set; }
        public Usuario usuario_publicador { get; set; }
        public Usuario usuario_evaluador { get; set; }
        public string version { get; set; }
        public byte[] archivo_devolucion { get; set; }
        public DateTime fecha_estimada_resolucion { get; set; }
        public DateTime fecha_devolucion { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public EstadoPublicacion estado { get; set; }
    }
}
