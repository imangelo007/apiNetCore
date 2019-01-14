using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Cursos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
    }
}
