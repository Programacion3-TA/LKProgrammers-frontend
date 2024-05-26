using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Administrador : Usuario
    {
        public int Codigo { get; set; }
        public string Puesto { get; set; }


    }
}
