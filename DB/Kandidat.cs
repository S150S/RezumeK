//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rezume.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kandidat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kandidat()
        {
            this.Obrazovanie = new HashSet<Obrazovanie>();
            this.RezumeV = new HashSet<RezumeV>();
        }
    
        public int Id_kandidat { get; set; }
        public string Familiy { get; set; }
        public string Imya { get; set; }
        public string Otchestvj { get; set; }
        public System.DateTime Date_rogd { get; set; }
        public string Telefon { get; set; }
        public string Pochta { get; set; }
        public byte[] Photo { get; set; }
        public string Login_v_sistemu { get; set; }
        public string Parol_v_sistemu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Obrazovanie> Obrazovanie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RezumeV> RezumeV { get; set; }
    }
}
