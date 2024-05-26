using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AlumnoN : Usuario
    {
        public int Codigo { set; get; }
        public string Grado { set; get; }
    }
}
