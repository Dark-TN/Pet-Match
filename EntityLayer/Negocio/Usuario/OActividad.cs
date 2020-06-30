using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Negocio.Usuario
{
    public class OActividad
    {
        public int idActividad { get; set; }
        public int idAlumno { get; set; }
        public String nombre { get; set; }
        public int creditos { get; set; }
        public int tiempoCreditos { get; set; }
        public int idAreaActividad { get; set; }
        public int idCoordinador { get; set; }
        public String descripcion { get; set; }
        public String horario { get; set; }
        public String nombreArea { get; set; }
        public String nombreCoordinador { get; set; }
        public String nombreAlumno { get; set; }
        public float creditosInscritos { get; set; }
        public OActividad()
        {
            this.idActividad = 0;
            this.idAlumno = 0;
            this.nombre = String.Empty;
            this.creditos = 0;
            this.tiempoCreditos = 0;
            this.idAreaActividad = 0;
            this.idCoordinador = 0;
            this.descripcion = String.Empty;
            this.horario = String.Empty;
            this.nombreArea = String.Empty;
            this.nombreCoordinador = String.Empty;
            this.nombreAlumno = String.Empty;
            this.creditosInscritos = 0;
        }
    }
}
