using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Importacion_Vehiculos.Models;

namespace Importacion_Vehiculos.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            List<MarcaCLS> listaMarca = null;
            using (var bd = new Importacion_VehiculosEntities())
            {
                listaMarca = (from marca in bd.Marca
                                             select new MarcaCLS
                                             {
                                                 id_marca = marca.Id_Marca,
                                                 nombre_marca = marca.Nombre_Marca
                                             }).ToList();
            }

                return View(listaMarca);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        //insersion de datos

        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oMarcaCLS);
            }
            else
            {
                using (var bd = new Importacion_VehiculosEntities())
                {
                    Marca oMarca = new Marca();
                    oMarca.Nombre_Marca = oMarcaCLS.nombre_marca;
                    bd.Marca.Add(oMarca);
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }



    }
}