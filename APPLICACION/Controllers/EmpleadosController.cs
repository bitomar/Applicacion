using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Importacion_Vehiculos.Models;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing;

namespace Importacion_Vehiculos.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Index()
        {          

            List<EmpleadosCLS> ListaEmpleados = null;
            using (var bd = new Importacion_VehiculosEntities())
            {
                ListaEmpleados = (from empleados in bd.Empleados
                                  join departamento in bd.Departamento
                                  on empleados.Id_Departamento equals departamento.Id_Departamento
                                  select new EmpleadosCLS
                                  {
                                      id_empleado = empleados.Id_Empleado,
                                      departamento = departamento.Departamento1,
                                      nombre = empleados.Nombre,
                                      apellidos = empleados.Apellidos,
                                      dpi = empleados.Dpi,
                                      puesto = empleados.Puesto,
                                      fecha_nacimiento = empleados.Fecha_Nacimiento,
                                      direccion = empleados.Direccion,
                                      email = empleados.Email,
                                      telefono = empleados.Telefono

                                  }).ToList();
                Session["lista"] = ListaEmpleados;
            }

                return View(ListaEmpleados);
        }

        /*Generar archivo de Excel*/

        public FileResult Reporte_Empleados()
        {
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                // Todo el documento
                ExcelPackage ep = new ExcelPackage();
                //crar una hoja 
                ep.Workbook.Worksheets.Add("Reporte Empleados");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                // ponemos nombre de las columnas
                ew.Cells[1, 1].Value = "";
                ew.Cells[2, 2].Value = "Importación de Vehículos";
                ew.Cells[3, 2].Value = "Grupo #5";
                ew.Cells[4, 2].Value = "Bases de Datos II -Proyecto Final-";
                ew.Cells[5, 1].Value = "";
                ew.Cells[6, 1].Value = "Código Empleado";
                ew.Cells[6, 2].Value = "Departamento";
                ew.Cells[6, 3].Value = "Nombres";
                ew.Cells[6, 4].Value = "Apellidos";
                ew.Cells[6, 5].Value = "DPI";
                ew.Cells[6, 6].Value = "Puesto / Cargo";
                ew.Cells[6, 7].Value = "Fecha de Nacimiento";
                ew.Cells[6, 8].Value = "Dirección";
                ew.Cells[6, 9].Value = "Correo electrónico";
                ew.Cells[6, 10].Value = "Teléfono";


                ew.Cells[4, 4].Value = "Reporte de Empleados generado " + DateTime.Now;


                ew.Column(1).Width = 40;
                ew.Column(2).Width = 30;
                ew.Column(3).Width = 30;
                ew.Column(4).Width = 30;
                ew.Column(5).Width = 30;
                ew.Column(6).Width = 30;
                ew.Column(7).Width = 30;
                ew.Column(8).Width = 30;
                ew.Column(9).Width = 30;
                ew.Column(10).Width = 15;


                using (var range = ew.Cells[6, 1, 6, 10])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);

                }
                /////////////////// agregado

                using (var range = ew.Cells[2, 2, 4, 4])
                {
                    range.Style.Font.Size = 12;
                    range.Style.Font.Color.SetColor(Color.Black);
                    range.Style.Font.Bold = true;
                }

                Image img = Image.FromFile(Server.MapPath("~/images/Logo_import.png"));
                ExcelPicture logo = ew.Drawings.AddPicture("Picture_Name", img);
                logo.SetPosition(0, 0, 0, 0);
                logo.SetSize(282, 102);



                ///////////////// agregado
                List<EmpleadosCLS> lista = (List<EmpleadosCLS>)Session["lista"];
                int nregistros = lista.Count;
                //Pendiente
                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 7, 1].Value = lista[i].id_empleado;
                    ew.Cells[i + 7, 2].Value = lista[i].departamento;
                    ew.Cells[i + 7, 3].Value = lista[i].nombre;
                    ew.Cells[i + 7, 4].Value = lista[i].apellidos;
                    ew.Cells[i + 7, 5].Value = lista[i].dpi;
                    ew.Cells[i + 7, 6].Value = lista[i].puesto;
                    ew.Cells[i + 7, 7].Value = lista[i].fecha_nacimiento.ToString("dd/MM/yyyy");
                    ew.Cells[i + 7, 8].Value = lista[i].direccion;
                    ew.Cells[i + 7, 9].Value = lista[i].email;
                    ew.Cells[i + 7, 10].Value = lista[i].telefono;
                }
                //Pendiente
                ep.SaveAs(ms);
                buffer = ms.ToArray();

            }
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}