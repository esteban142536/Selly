using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using Infraestructure;
using System.Reflection;
using Infraestructure.Models;
using ApplicationCore.Services;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {

        IServiseProducto serviseProducto = new ServiseProducto();

        public ActionResult CrearPDFProductos()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<producto> lista = null;
            try
            {
                // Extraer informacion

                lista = serviseProducto.listadoProducto();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Libros")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(5, true);


                // los Encabezados
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("precio individual");
                table.AddHeaderCell("Tipo de categoria");
                table.AddHeaderCell("Total de stock");
                table.AddHeaderCell("Imagen");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.nombre));
                    table.AddCell(new Paragraph(item.costoUnitario.ToString()));
                    table.AddCell(new Paragraph(item.TipoCategoria.Descripcion));
                    table.AddCell(new Paragraph(item.totalStock.ToString()));

                    // Convierte la imagen que viene en Bytes en imagen para PDF
                    Image image = new Image(ImageDataFactory.Create(item.imagen));
                    // Tamaño de la imagen
                    image = image.SetHeight(75).SetWidth(75);
                    table.AddCell(image);
                }
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }



    }
}
