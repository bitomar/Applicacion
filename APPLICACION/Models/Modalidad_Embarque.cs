//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Importacion_Vehiculos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Modalidad_Embarque
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modalidad_Embarque()
        {
            this.Embarque = new HashSet<Embarque>();
        }
    
        public int Id_Modalidad_Embarque { get; set; }
        public string Modalidad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Embarque> Embarque { get; set; }
    }
}
