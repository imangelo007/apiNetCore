using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cursos = new HashSet<Cursos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Cursos> Cursos { get; set; }
    }
}
