using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Proyecto
    {
        public Proyecto()
        {
            usuario_creador = new Usuario();
        }

        public int id_proyecto { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_ultima_modif { get; set; }
        public DateTime fecha_baja { get; set; }
        public Usuario usuario_creador { get; set; }
        public string descripcion { get; set; }
        public string urlTesting { get; set; }
        public string urlProduccion { get; set; }
    }
}
