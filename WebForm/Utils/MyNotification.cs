using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Utils
{
    public class MyNotification
    {
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public string Titulo { get; set; }

        public MyNotification()
        {
            this.Tipo = "Info";
            this.Mensaje = "";
            this.Titulo = "";
        }
    }
}