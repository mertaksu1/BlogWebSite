namespace mertaksublogg.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uye")]
    public partial class uye
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public uye()
        {
            makales = new HashSet<makale>();
            yorums = new HashSet<yorum>();
        }

        public int uyeid { get; set; }

        [StringLength(50)]
        public string kullaniciadi { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string sifre { get; set; }

        [StringLength(50)]
        public string adisoyadi { get; set; }

        [StringLength(1000)]
        public string foto { get; set; }

        public int? yetkiid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<makale> makales { get; set; }

        public virtual yetki yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
