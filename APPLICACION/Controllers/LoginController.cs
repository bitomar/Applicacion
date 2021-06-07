using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Importacion_Vehiculos.Models;


namespace Importacion_Vehiculos.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CerrarSession()
        {
            return RedirectToAction("Index");
        }

        public string Login(UsuarioCLS oUsuario)
        {
            string mensaje = "";

            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();
                mensaje += "<ul class='list-group'>";

                foreach (var item in query)
                {
                    mensaje += "<li class='list-group-item'>" + item + "</li>";
                }

                mensaje += "</ul>";
            }
            else
            {
                string usuario = oUsuario.usuario;
                string password = oUsuario.password;

                using (var bd = new Importacion_VehiculosEntities())
                {
                    int numeroVeces = bd.Usuario.Where(p => p.Usuario1 == usuario
                    && p.Password == password).Count();
                    mensaje = numeroVeces.ToString();
                    if (mensaje == "0") mensaje = "Usuario o contraseña incorrecta";
                    else
                    {
                        Usuario ousuario = bd.Usuario.Where(p => p.Usuario1 == usuario
                    && p.Password == password).First();
                        //Todo el objeto Usuario
                        Session["Usuario"] = oUsuario;
                    }
                }
            }

            return mensaje;
        }

    }
}