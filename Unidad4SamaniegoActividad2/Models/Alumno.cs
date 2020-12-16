using System;
using System.Collections.Generic;

namespace Unidad4SamaniegoActividad2.Models
{
    public partial class Alumno
    {
        public int Id { get; set; }
        public string NumeroDeControl { get; set; }
        public string Nombre { get; set; }
        public int IdDocente { get; set; }

        public virtual Docente IdDocenteNavigation { get; set; }
    }
}
