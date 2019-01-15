using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class B
    {
        public B()
        {
            Ab = new HashSet<Ab>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Ab> Ab { get; set; }
    }
}
