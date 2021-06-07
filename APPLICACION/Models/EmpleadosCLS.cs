using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Importacion_Vehiculos.Models
{
    public class EmpleadosCLS
    {
        //Id_Empleado Id_Departamento Nombre Apellidos   Dpi Puesto  Fecha_Nacimiento Direccion   Email Telefono    Jefe
        [Display(Name = "Código Empleado")]
        public int id_empleado { get; set; }
        [Display(Name = "Id_Departamento")]
        public int id_departamento { get; set; }
        [Display(Name = "Nombres")]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }
        [Display(Name = "DPI")]
        public string dpi { get; set; }
        [Display(Name = "Puesto / Cargo")]
        public string puesto { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_nacimiento { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        [Display(Name = "Correo electrónico")]
        public string email { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Jefe")]
        public int jefe { get; set; }

        //Informacion adicional
        [Display(Name = "Departamento")]
        public string departamento { get; set; }

    }
}