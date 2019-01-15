using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Ab
    {
        public int Ida { get; set; }
        public int Idb { get; set; }
        public int Id { get; set; }

        public A IdaNavigation { get; set; }
        public B IdbNavigation { get; set; }
    }
}
