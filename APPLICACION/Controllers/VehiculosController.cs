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
    public class VehiculosController : Controller
    {
        // GET: Vehiculos
        public ActionResult Index(VehiculosCLS oVehiculosCLS)
        {
            ListarCombos();
            List<VehiculosCLS> ListaRespuesta = new List<VehiculosCLS>();
            List<VehiculosCLS> listaVehiculos = null;

            using (var bd = new Importacion_VehiculosEntities())
            {

                listaVehiculos = (from vehiculos in bd.Vehiculos
                                  join marca in bd.Marca
                                  on vehiculos.Id_Marca equals marca.Id_Marca
                                  join clasificacion_vehiculo in bd.Clasificacion_Vehiculo
                                  on vehiculos.Id_Clasificacion equals clasificacion_vehiculo.Id_Clasificacion
                                  select new VehiculosCLS
                                  {
                                      marca = marca.Nombre_Marca,
                                      clasificacion = clasificacion_vehiculo.Clasificacion,
                                      motor_cc = vehiculos.Motor_cc,
                                      linea = vehiculos.Linea,
                                      anio = vehiculos.Anio,
                                      color = vehiculos.Color,
                                      numero_asientos = vehiculos.Numero_Asientos,
                                      tonelaje = vehiculos.Tonelaje,
                                      peso = vehiculos.Peso,
                                      id_marca = marca.Id_Marca,
                                      id_clasificacion = clasificacion_vehiculo.Id_Clasificacion
                                  }).ToList();

                if (oVehiculosCLS.id_marca == 0 && oVehiculosCLS.id_clasificacion == 0)
                {
                    ListaRespuesta = listaVehiculos;
                }
                else
                {
                    //Filtro por marca
                    if (oVehiculosCLS.id_marca != 0)
                    {
                        listaVehiculos = listaVehiculos.Where(p => p.id_marca.ToString().Contains(oVehiculosCLS.id_marca.ToString())).ToList();
                    }
                    //Filtro por clasificacion
                    if (oVehiculosCLS.id_clasificacion != 0)
                    {
                        listaVehiculos = listaVehiculos.Where(p => p.id_clasificacion.ToString().Contains(oVehiculosCLS.id_clasificacion.ToString())).ToList();
                    }

                    ListaRespuesta = listaVehiculos;                    
                }

                Session["lista"] = ListaRespuesta;
            }
                return View(ListaRespuesta);
        }

        

        private void llenarMarca()
        {
            //Combobox Marca
            List<SelectListItem> listaMarca;
            using (var bd = new Importacion_VehiculosEntities())
            {
                listaMarca = (from marca in bd.Marca
                              orderby marca.Nombre_Marca ascending
                              select new SelectListItem
                              {
                                  Text = marca.Nombre_Marca,
                                  Value = marca.Id_Marca.ToString()
                              }).ToList();
                listaMarca.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listamarca = listaMarca;
            }
        }
        //Fin Combobox Marca
        

        private void llenarClasificacion()
        {
            List<SelectListItem> listaClasificacion;
            using (var bd = new Importacion_VehiculosEntities())
            {
                listaClasificacion = (from clasificacion_vehiculo in bd.Clasificacion_Vehiculo
                                      orderby clasificacion_vehiculo.Clasificacion ascending
                                      select new SelectListItem
                              {
                                  Text = clasificacion_vehiculo.Clasificacion,
                                  Value = clasificacion_vehiculo.Id_Clasificacion.ToString()
                              }).ToList();
                listaClasificacion.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaclasificacion = listaClasificacion;
            }
        }
        //Combobox Clasificacion
        //Fin de Combobox Clasificacion

            //Vista agregar
        public ActionResult Agregar()
        {
            ListarCombos();
            //llenarMarca();
            //ViewBag.listamarca = listaMarca;

            //llenarClasificacion();
            //ViewBag.listaclasificacion = listaClasificacion;

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(VehiculosCLS oVehiculosCLS)
        {
            if (!ModelState.IsValid)
            {
                ListarCombos();
                ////Combobox Marca
                //llenarMarca();
                //ViewBag.listamarca = listaMarca;
                ////Combobox Clasificacion
                //llenarClasificacion();
                //ViewBag.listaclasificacion = listaClasificacion;

                return View(oVehiculosCLS);
            }
            using (var bd = new Importacion_VehiculosEntities())
            { 
                Vehiculos oVehiculos = new Vehiculos();
                oVehiculos.Id_Marca = oVehiculosCLS.id_marca;
                oVehiculos.Id_Clasificacion = oVehiculosCLS.id_clasificacion;
                oVehiculos.Motor_cc = oVehiculosCLS.motor_cc;
                oVehiculos.Linea = oVehiculosCLS.linea;
                oVehiculos.Anio = oVehiculosCLS.anio;
                oVehiculos.Color = oVehiculosCLS.color;
                oVehiculos.Numero_Asientos = oVehiculosCLS.numero_asientos;
                oVehiculos.Tonelaje = oVehiculosCLS.tonelaje;
                oVehiculos.Peso = oVehiculosCLS.peso;
                bd.Vehiculos.Add(oVehiculos);
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public void ListarCombos()
        {
            llenarClasificacion();
            llenarMarca();
        }

        /*Generar archivo de Excel*/
        
        public FileResult Reporte_Excel()
        {
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                // Todo el documento
                ExcelPackage ep = new ExcelPackage();
                //crar una hoja 
                ep.Workbook.Worksheets.Add("Reporte");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                // ponemos nombre de las columnas
                ew.Cells[1, 1].Value = "";
                ew.Cells[2, 2].Value = "Importación de Vehículos";
                ew.Cells[3, 2].Value = "Grupo #5";
                ew.Cells[4, 2].Value = "Bases de Datos II -Proyecto Final-";
                ew.Cells[5, 1].Value = "";
                ew.Cells[6, 1].Value = "Marca";
                ew.Cells[6, 2].Value = "Clasificación Vehículo";
                ew.Cells[6, 3].Value = "Motor CC";
                ew.Cells[6, 4].Value = "Linea";
                ew.Cells[6, 5].Value = "Año";
                ew.Cells[6, 6].Value = "Color";
                ew.Cells[6, 7].Value = "Número de asientos";
                ew.Cells[6, 8].Value = "Tonelaje Kg";
                ew.Cells[6, 9].Value = "Peso Kg";
                //ew.Cells[6, 10].Value = "Peso Kg";


                ew.Cells[4, 4].Value = "Reporte Vehículos generado " + DateTime.Now;


                ew.Column(1).Width = 40;
                ew.Column(2).Width = 35;
                ew.Column(3).Width = 15;
                ew.Column(4).Width = 15;
                ew.Column(5).Width = 10;
                ew.Column(6).Width = 10;
                ew.Column(7).Width = 20;
                ew.Column(8).Width = 15;
                ew.Column(9).Width = 15;
                //ew.Column(10).Width = 15;


                using (var range = ew.Cells[6, 1, 6, 9])
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
                List<VehiculosCLS> lista = (List<VehiculosCLS>)Session["lista"];
                int nregistros = lista.Count;
                //Pendiente
                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 7, 1].Value = lista[i].marca;
                    ew.Cells[i + 7, 2].Value = lista[i].clasificacion;
                    ew.Cells[i + 7, 3].Value = lista[i].motor_cc;
                    ew.Cells[i + 7, 4].Value = lista[i].linea;
                    ew.Cells[i + 7, 5].Value = lista[i].anio;
                    ew.Cells[i + 7, 6].Value = lista[i].color;
                    ew.Cells[i + 7, 7].Value = lista[i].numero_asientos;
                    ew.Cells[i + 7, 8].Value = lista[i].tonelaje;
                    ew.Cells[i + 7, 9].Value = lista[i].peso;
                }
                //Pendiente
                ep.SaveAs(ms);
                buffer = ms.ToArray();

            }
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}