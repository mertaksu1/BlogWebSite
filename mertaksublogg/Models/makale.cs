namespace mertaksublogg.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("makale")]
    public partial class makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public makale()
        {
            yorums = new HashSet<yorum>();
        }

        public int makaleid { get; set; }

        [StringLength(50)]
        public string baslik { get; set; }

        [StringLength(250)]
        public string ozet { get; set; }

        public string icerik { get; set; }

        [StringLength(500)]
        public string buyukfoto { get; set; }

        [StringLength(500)]
        public string kucukfoto { get; set; }

        public DateTime? tarih { get; set; }

        public int? kategoriid { get; set; }

        public int? uyeid { get; set; }

        public int? okunma { get; set; }

        public virtual kategori kategori { get; set; }

        public virtual uye uye { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
