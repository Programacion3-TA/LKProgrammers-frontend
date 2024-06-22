using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.CalendarioAlumno
{
    public partial class CalendarioAlumno : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        private cursoHorario[] CursosHorarios;
        protected string[,] Calendario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Minutos"] = Session["Minutos"] ?? 30;
            LblBloques.Text = $"{Session["Minutos"]}";

            daoServicio = new LKServicioWebClient();
            CursosHorarios = daoServicio.listarCursosHorarioAlumnos(((alumno)Session["Usuario"]).dni);
            RenderizarCalendario((int)Session["Minutos"]);
            // Pruebas estaticas
            //CursosHorarios = new cursoHorario[]
            //{
            //    new cursoHorario{idsalon=1, profesor="12345678", curso=new curso{id=1,nombre="Matematicas", descripcion="XD"}, horarioDictado=
            //        new horario[] {
            //            new horario{ id=1, horaInicio=new tiempo{ hora=8, minuto=0}, horaFin=new tiempo{ hora=10, minuto=0}, dia=diaSemana.Lunes },
            //            new horario{ id=1, horaInicio=new tiempo{ hora=8, minuto=0}, horaFin=new tiempo{ hora=10, minuto=0}, dia=diaSemana.Martes },
            //        }
            //    },
            //    new cursoHorario{idsalon=2, profesor="12345679", curso=new curso{id=1,nombre="Lenguaje", descripcion="XD"}, horarioDictado=
            //        new horario[] {
            //            new horario{ id=1, horaInicio=new tiempo{ hora=10, minuto=0}, horaFin=new tiempo{ hora=12, minuto=0}, dia=diaSemana.Lunes },
            //            new horario{ id=1, horaInicio=new tiempo{ hora=10, minuto=0}, horaFin=new tiempo{ hora=12, minuto=0}, dia=diaSemana.Martes },
            //        }
            //    },
            //    new cursoHorario{idsalon=2, profesor="12345679", curso=new curso{id=1,nombre="Ciencias y tecnología", descripcion="XD"}, horarioDictado=
            //        new horario[] {
            //            new horario{ id=1, horaInicio=new tiempo{ hora=8, minuto=0}, horaFin=new tiempo{ hora=10, minuto=0}, dia=diaSemana.Miercoles },
            //            new horario{ id=1, horaInicio=new tiempo{ hora=10, minuto=0}, horaFin=new tiempo{ hora=12, minuto=0}, dia=diaSemana.Viernes },
            //        }
            //    },
            //    new cursoHorario{idsalon=2, profesor="12345679", curso=new curso{id=1,nombre="Historia", descripcion="XD"}, horarioDictado=
            //        new horario[] {
            //            new horario{ id=1, horaInicio=new tiempo{ hora=8, minuto=0}, horaFin=new tiempo{ hora=10, minuto=0}, dia=diaSemana.Jueves },
            //            new horario{ id=1, horaInicio=new tiempo{ hora=10, minuto=0}, horaFin=new tiempo{ hora=12, minuto=0}, dia=diaSemana.Miercoles },
            //        }
            //    }
            //};
            //RenderizarCalendario((int) Session["Minutos"]);
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            int num = (int) Session["Minutos"];
            if (num >= 60) return;
            num += 15;
            RenderizarCalendario(num);
            Session["Minutos"] = num;
            LblBloques.Text = $"{num}";
        }
        protected void BtnRestar_Click(object sender, EventArgs e)
        {
            int num = (int)Session["Minutos"];
            if (num <= 15) return;
            num -= 15;
            RenderizarCalendario(num);
            Session["Minutos"] = num;
            LblBloques.Text = $"{num}";
        }

        protected void RenderizarCalendario(int bloqueMinutos = 30)
        {
            if (CursosHorarios == null) CursosHorarios = new cursoHorario[] { };

            // Calendario
            CalendarHeader.Text = string.Join(
                "",
                Enum.GetValues(typeof(diaSemana))
                    .Cast<diaSemana>()
                    .ToList()
                    .Select( dia => $"<th scope=\"col\" class=\"text-center\">{dia}</th>")
            );

            // Estilización de los horarios
            string[] bgPermitidos = { "#6ea8fe", "#a370f7", "#e685b5", "#ea868f", "#feb272", "#ffda6a", "#75b798", "#a98eda", "#6edff6", "#79dfc1" };
            string[] fontPermitidos = { "#031633", "#140330", "#2b0a1a", "#2c0b0e", "#331904", "#332701", "#332701", "#160d27", "#032830", "#06281e" };

            Dictionary<string, string> bgCursos = new Dictionary<string, string> { };
            Dictionary<string, string> fontCursos = new Dictionary<string, string> { };

            Func<tiempo, int> ObtenerTotalMinutos = (tiempo t)=> t.hora * 60 + t.minuto;
            Func<int, string> horasAFormato = (int tiempoEnMinutos) => $"{tiempoEnMinutos / 60:D2}:{tiempoEnMinutos % 60:D2} - {(tiempoEnMinutos + bloqueMinutos) / 60:D2}:{(tiempoEnMinutos + bloqueMinutos) % 60:D2}";
            // Datos generales de la tabla
            tiempo horaInicioJornadaEscolar = new tiempo { hora = 8 },
                horaFinJornadaEscolar = new tiempo { hora = 12, minuto=00 },
                horaReceso = new tiempo { hora = 10 };
            int minutosInicio = ObtenerTotalMinutos(horaInicioJornadaEscolar),
                minutosFin = ObtenerTotalMinutos(horaFinJornadaEscolar);

            // Inicializacion de la vista de calendario
            int totalFilas = (minutosFin - minutosInicio) / bloqueMinutos,
                totalColumnas = 1 + Enum.GetValues(typeof(diaSemana)).Length;
            Calendario = new string[totalFilas, totalColumnas];
            for(int i = 0; i < totalFilas; i++)
            {
                Calendario[i, 0] = horasAFormato( minutosInicio + i* bloqueMinutos);
                for(int j = 1; j < totalColumnas; j++)
                {
                    Calendario[i, j] = "-";
                }
            }

            int colorPos = 0;
            foreach (cursoHorario curHor in CursosHorarios)
            {
                bgCursos[curHor.curso.nombre] = bgPermitidos[colorPos % bgPermitidos.Length];
                fontCursos[curHor.curso.nombre] = fontPermitidos[colorPos++ % bgPermitidos.Length ];

                foreach (horario hor in curHor.horarioDictado)
                {
                    int minutosInicioCurHor = hor.horaInicio.hora * 60 + hor.horaInicio.minuto,
                        minutosFinCurHor = hor.horaFin.hora * 60 + hor.horaFin.minuto,
                        redondeoInicio = ((minutosInicioCurHor % bloqueMinutos >= bloqueMinutos / 2) ? bloqueMinutos : 0) - minutosInicioCurHor % bloqueMinutos,
                        redondeoFin = ((minutosFinCurHor % bloqueMinutos >= bloqueMinutos / 2) ? bloqueMinutos : 0) - minutosFinCurHor % bloqueMinutos;
                    minutosInicioCurHor += redondeoInicio;
                    minutosFinCurHor += redondeoFin;
                    int indI = (minutosInicioCurHor - minutosInicio) / bloqueMinutos,
                        indF = (minutosFinCurHor - minutosInicio) / bloqueMinutos;
                    for(int i = indI; (i < indF) && (i*bloqueMinutos + minutosInicio < minutosFin); i++)
                        Calendario[i, ((int) hor.dia) + 1] = curHor?.curso?.nombre;
                }
            }
            string html = "";
            for (int i = 0; i < totalFilas; i++)
            {
                html += $"<tr><th scope=\"row\" class=\"text-center min-w-200\">{Calendario[i, 0]}</th>";
                if(minutosInicio + i* bloqueMinutos == ObtenerTotalMinutos(horaReceso))
                {
                    html += "<td colspan=\"6\" class=\"text-center\" style=\"color: #fff;background-color: #969696\">Recreo</td></tr>";
                    continue;
                }
                for(int j = 1; j < totalColumnas; j++)
                {
                    html += (Calendario[i, j].CompareTo("-") == 0)?
                        MyReact.CreateComponent("td", $"class=\"text-center\"", "-") :
                        MyReact.CreateComponent("td", $"class=\"text-center\" style=\"color:{fontCursos[Calendario[i, j]]}; background-color:{bgCursos[Calendario[i, j]]}\"", Calendario[i, j]);
                }
                html += "</tr>";
            }

            CalendarContainer.Text = html;
        }

        protected void BtnReporteHorario_Click(object sender, EventArgs e)
        {

        }
    }
}