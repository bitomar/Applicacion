using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Importacion_Vehiculos.Models
{
    public class UsuarioCLS
    {
        [Display(Name = "Id Usuario")]
        public int id_usuario { get; set; }
        [Display(Name = "Usuario")]
        public string usuario { get; set; }
        [Display(Name = "Contraseña")]
        public string password { get; set; }        
        [Display(Name = "Empleado")]
        public int id_empleado { get; set; }
        public int habilitado { get; set; }
    }
}