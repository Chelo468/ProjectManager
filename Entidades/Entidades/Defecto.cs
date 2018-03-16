using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Defecto
    {
        public Defecto()
        {
            usuario_detector = new Usuario();
            usuario_resolutor = new Usuario();
            estado = new EstadoDefecto();
            publicacion = new Publicacion();
        }

        public int id_defecto { get; set; }
        public Usuario usuario_detector { get; set; }
        public Usuario usuario_resolutor { get; set; }
        public string descripcion { get; set; }
        public EstadoDefecto estado { get; set; }
        public Publicacion publicacion { get; set; }
        public DateTime fecha_deteccion { get; set; }
        public DateTime fecha_resolucion { get; set; }
    }
}
