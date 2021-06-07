using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Importacion_Vehiculos.Models
{
    public class MarcaCLS
    {
        [Display(Name ="Código Marca")]
        public int id_marca { get; set; }
        [Display(Name = "Marca")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima 100 caracteres")]
        public string nombre_marca { get; set; }
    }
}