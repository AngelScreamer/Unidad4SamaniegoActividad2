using System;
using System.Collections.Generic;

namespace Unidad4SamaniegoActividad2.Models
{
    public partial class Docente
    {
        public Docente()
        {
            Alumno = new HashSet<Alumno>();
        }

        public int Id { get; set; }
        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public ulong? Activo { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
    }
}
