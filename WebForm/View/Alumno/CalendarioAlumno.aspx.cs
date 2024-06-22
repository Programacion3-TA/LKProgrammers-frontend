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
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            cursoHorario[] Horarios = daoServicio.listarCursosHorarioAlumnos( ( (alumno)Session["Usuario"]).dni );
            RenderizarCalendario(Horarios);
        }

        protected void RenderizarCalendario(cursoHorario[] cursosHorarios)
        {
            // Obtencíón de colores
            string[] bgPermitidos = { "#6ea8fe", "#a370f7", "#e685b5", "#ea868f", "#feb272", "#ffda6a", "#75b798", "#a98eda", "#6edff6", "#79dfc1" };
            string[] fontPermitidos = { "#031633", "#140330", "#2b0a1a", "#2c0b0e", "#331904", "#332701", "#332701", "#160d27", "#032830", "#06281e" };

            Dictionary<string, string> bgCursos = new Dictionary<string, string> { };
            Dictionary<string, string> fontCursos = new Dictionary<string, string> { };
            // Datos generales de la tabla
            const int BloqueMinutos = 15;
            tiempo horaInicioJornadaEscolar = new tiempo { hora = 8 };
            tiempo horaFinJornadaEscolar = new tiempo { hora = 12, minuto=30 };
            tiempo horaReceso = new tiempo { hora = 10 };

            SortedList<int, Dictionary<diaSemana, string>> datosCalendario = new SortedList<int, Dictionary<diaSemana, string>>();
            for( int horaControl = horaInicioJornadaEscolar.hora*60 + horaInicioJornadaEscolar.minuto; horaControl < horaFinJornadaEscolar.hora * 60 + horaFinJornadaEscolar.minuto; horaControl += BloqueMinutos )
            {
                datosCalendario.Add(horaControl, new Dictionary<diaSemana, string> { });
                foreach (diaSemana dia in Enum.GetValues(typeof(diaSemana)))
                {
                    datosCalendario[horaControl][dia] = "-";
                }
            }

            int colorPos = 0;
            foreach (cursoHorario curHor in cursosHorarios)
            {
                bgCursos[curHor.curso.nombre] = bgPermitidos[colorPos];
                fontCursos[curHor.curso.nombre] = fontPermitidos[colorPos];
                colorPos++;
                colorPos = (colorPos == bgPermitidos.Length) ? 0 : colorPos;

                foreach (horario hor in curHor.horarioDictado)
                {
                    int minutosInicio = hor.horaInicio.hora * 60 + hor.horaInicio.minuto,
                        minutosFin = hor.horaFin.hora * 60 + hor.horaFin.minuto,
                        redondeoInicio = (minutosInicio % BloqueMinutos >= BloqueMinutos / 2) ? BloqueMinutos : 0,
                        redondeoFin = (minutosFin % BloqueMinutos >= BloqueMinutos / 2) ? BloqueMinutos : 0;
                    minutosInicio += redondeoInicio - (minutosInicio % BloqueMinutos);
                    minutosFin += redondeoFin - (minutosFin % BloqueMinutos);
                    do
                    {
                        datosCalendario[minutosInicio][hor.dia] = curHor.curso.nombre;
                        minutosInicio += BloqueMinutos;
                    } while (minutosFin > minutosInicio);
                }
            }

            Func<int, string> horasAFormato = (int tiempoEnMinutos) => $"{tiempoEnMinutos / 60:D2}:{tiempoEnMinutos % 60:D2} - {(tiempoEnMinutos + BloqueMinutos) / 60:D2}:{(tiempoEnMinutos + BloqueMinutos) % 60:D2}";
            string html = "";
            foreach(KeyValuePair<int, Dictionary<diaSemana, string>> parHoraDatos in datosCalendario)
            {
                html += $"<tr><th scope=\"row\" class=\"text-center min-w-200\">{horasAFormato(parHoraDatos.Key)}</th>";
                if(parHoraDatos.Key == horaReceso.hora*60 + horaReceso.minuto)
                {
                    html += "<td colspan=\"6\" class=\"text-center\">Recreo</td></tr>";
                    continue;
                }
                foreach(KeyValuePair<diaSemana, string> parDiaCurso in parHoraDatos.Value)
                {
                    html += (parDiaCurso.Value.CompareTo("-") == 0)?
                        MyReact.CreateComponent("td", $"class=\"text-center\"", "-") :
                        MyReact.CreateComponent("td", $"class=\"text-center\" style=\"color:{fontCursos[parDiaCurso.Value]}; background-color:{bgCursos[parDiaCurso.Value]}\"", parDiaCurso.Value);
                }
                html += "</tr>";
            }

            CalendarContainer.Text = html;
        }
    }
}