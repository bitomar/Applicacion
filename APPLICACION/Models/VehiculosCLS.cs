using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Importacion_Vehiculos.Models
{
    public class VehiculosCLS
    {
        [Display(Name = "Id Vehiculo")]
        public int id_vehiculo { get; set; }

        [Display(Name = "Marca")]
        [Required]
        public int id_marca { get; set; }

        [Display(Name = "Clasificacion del Vehiculo")]
        [Required]
        public int id_clasificacion { get; set; }

        [Display(Name = "Motor CC")]
        [Required]
        public string motor_cc { get; set; }

        [Display(Name = "Linea")]
        [Required]
        [StringLength(150,ErrorMessage ="Longitud máxima 150 caracteres")]
        public string linea { get; set; }

        [Display(Name = "Año")]
        [Required]
        public int anio { get; set; }

        [Display(Name = "Color")]
        [Required]
        [StringLength(150, ErrorMessage = "Longitud máxima 150 caracteres")]
        public string color { get; set; }

        [Display(Name = "Número de asientos")]
        [Required]
        public int numero_asientos { get; set; }

        [Display(Name = "Tonelaje Kg")]
        [Required]
        public double tonelaje { get; set; }

        [Display(Name = "Peso Kg")]
        [Required]
        public double peso { get; set; }

        //propiedades adicionales
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Display(Name = "Clasificación Vehiculo")]
        public string clasificacion { get; set; }

    }
}