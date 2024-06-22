using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using WebForm.ServicioWS;

namespace WebForm.Utils
{
    public class PdfGenerator
    {
        public static MemoryStream ReporteHorario(string[,] calendario, alumno alum)
        {
            MemoryStream memoryStream = new MemoryStream();

            // Crear un documento PDF
            PdfWriter pdfWriter = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());
            Document document = new Document(pdfDocument);

            // [i,0] -> Color de fondo
            // [i,1] -> Color de fuente
            DeviceRgb[,] colores = new DeviceRgb[,]
            {
                { new DeviceRgb(0xcf, 0xe2, 0xff), new DeviceRgb(0x08, 0x42, 0x98) },
                { new DeviceRgb(0xe0, 0xcf, 0xfc), new DeviceRgb(0x3d, 0x0a, 0x91) },
                { new DeviceRgb(0xe2, 0xd9, 0xf3), new DeviceRgb(0x43, 0x28, 0x74) },
                { new DeviceRgb(0xf7, 0xd6, 0xe6), new DeviceRgb(0x80, 0x1f, 0x4f) },
                { new DeviceRgb(0xf8, 0xd7, 0xda), new DeviceRgb(0x84, 0x20, 0x29) },
                { new DeviceRgb(0xff, 0xe5, 0xd0), new DeviceRgb(0x98, 0x4c, 0x0c) },
                { new DeviceRgb(0xff, 0xf3, 0xcd), new DeviceRgb(0x99, 0x74, 0x04) },
                { new DeviceRgb(0xd1, 0xe7, 0xdd), new DeviceRgb(0x0f, 0x51, 0x32) },
                { new DeviceRgb(0xd2, 0xf4, 0xea), new DeviceRgb(0x13, 0x79, 0x5b) },
                { new DeviceRgb(0xcf, 0xf4, 0xfc), new DeviceRgb(0x08, 0x79, 0x90) },
            };
            int totalColores = colores.GetLength(0);
            Dictionary<string, int> coloresUsados = new Dictionary<string, int> { };

            // Agregar contenido al documento
            //document.Add(new Image(ImageDataFactory.Create("~/Public/img/logoColegio.jpeg")));
            Div divisionTitulo = new Div().SetMarginBottom(20f).SetWidth(UnitValue.CreatePercentValue(100));
            divisionTitulo.Add(
                new Paragraph("Reporte de Horario")
                    .SetBold()
                    .SetFontSize(36f)
            );

            divisionTitulo.Add(
                new Table(2)
                    .AddCell(
                        new Cell()
                            .Add(new Paragraph("Fecha actual"))
                            .SetBackgroundColor(new DeviceRgb(0xde, 0xe2, 0xe6))
                    )
                    .AddCell(
                        new Cell()
                            .Add(new Paragraph( DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("es-ES")) ))
                    )
            );

            document.Add(divisionTitulo);

            document.Add(
                new Table(6)
                    .SetWidth(UnitValue.CreatePercentValue(100))
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetMarginBottom(20f)
                    // Fila 1
                    .AddCell(
                        new Cell(1,6).Add(new Paragraph("Datos generales")).SetBackgroundColor(new DeviceRgb(0xde, 0xe2, 0xe6))
                    )
                    // Fila 2
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Nombre del Alumno")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 3).Add(new Paragraph($"{alum.nombres} {alum.apellidoPaterno} {alum.apellidoMaterno}"))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("DNI del Alumno")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph($"{alum.dni}"))
                    )
                    // Fila 3
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Grado")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph($"{alum.grado.ToString()}"))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Salon")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Insertar salon"))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Telefono")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph($"{alum.telefono}"))
                    )
                    // Fila 4
                    .AddCell(
                        new Cell(1, 1).Add(new Paragraph("Nombre del tutor")).SetBackgroundColor(new DeviceRgb(0xe9, 0xec, 0xef))
                    )
                    .AddCell(
                        new Cell(1, 3).Add(new Paragraph("Insertar nombre"))
                    )
            );

            int totalFilas = calendario.GetLength(0),
                totalColumnas = calendario.GetLength(1);
            Table tablaCalendario = new Table(new float[] { 1,1,1,1,1,1,1})
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            
            tablaCalendario.AddHeaderCell(
                new Cell()
                    .Add(
                        new Paragraph("Hora")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetStrokeWidth(2.3f)
                            .SetBold()
                    )
                    .SetBorder(
                        new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6),  1.0f)
                    )
                    .SetBorderBottom(
                        new SolidBorder(new DeviceRgb(0, 0, 0), 2.0f)
                    )
                    .SetPaddings(2f, 4f, 2f, 4f)
            );

            foreach (diaSemana dia in Enum.GetValues(typeof(diaSemana)))
                tablaCalendario.AddHeaderCell(
                    new Cell()
                        .Add(
                            new Paragraph(dia.ToString())
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetStrokeWidth(2.3f)
                                .SetBold()
                        )
                        .SetBorder(
                            new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6), 1.0f)
                        )
                        .SetBorderBottom(
                            new SolidBorder(new DeviceRgb(0,0,0), 2.0f)
                        )
                        .SetPaddings(2f, 4f, 2f, 4f)
                );
            string contenidoCelda;
            int pos = 0;
            for (int i = 0; i < totalFilas; i++)
            {
                tablaCalendario
                    .AddCell(
                        new Cell()
                            .Add(new Paragraph(calendario[i, 0]).SetBold())
                            .SetBorder(
                                new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6), 1.0f)
                            )
                    );
                if (calendario[i, 1].CompareTo("Recreo") == 0)
                {
                    tablaCalendario
                        .AddCell(
                            new Cell(1, totalColumnas - 1)
                                .Add(
                                    new Paragraph("Recreo")
                                        .SetFontColor( new DeviceRgb(0x49, 0x50, 0x57) )
                                )
                                .SetBackgroundColor( new DeviceRgb(0xf8, 0xf9, 0xfa) )
                                .SetBorder(
                                    new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6), 1.0f)
                                )
                            )
                        .SetTextAlignment(TextAlignment.CENTER);
                    continue;
                }

                for (int j = 1; j < totalColumnas; j++)
                {
                    contenidoCelda = calendario[i, j];
                    if ((!coloresUsados.ContainsKey(contenidoCelda)) && contenidoCelda.CompareTo("-")!=0)
                    {
                        coloresUsados[contenidoCelda] = pos++ % totalColores;
                    }
                    
                    if (contenidoCelda.CompareTo("-") != 0)
                    {
                        tablaCalendario
                            .AddCell(
                                new Cell()
                                    .Add(
                                        new Paragraph(contenidoCelda)
                                            .SetFontColor(colores[coloresUsados[contenidoCelda], 1])
                                    )
                                    .SetBackgroundColor(colores[coloresUsados[contenidoCelda], 0])
                                    .SetBorder(
                                        new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6), 1.0f)
                                    )
                                )
                            .SetTextAlignment(TextAlignment.CENTER);
                    }
                    else
                    {
                        tablaCalendario
                            .AddCell(
                                new Cell()
                                    .Add(new Paragraph("-"))
                                    .SetBorder(
                                        new SolidBorder(new DeviceRgb(0xde, 0xe2, 0xe6), 1.0f)
                                    )
                            );
                    }
                }
            }
            document.Add(tablaCalendario);
            // Cerrar el documento
            document.Close();

            return memoryStream;
        }
    }
}