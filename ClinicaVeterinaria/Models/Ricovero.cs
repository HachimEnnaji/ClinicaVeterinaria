namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ricovero")]
    public partial class Ricovero
    {
        [Key]
        public int IdRicovero { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataInizio { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        public bool IsAttivo { get; set; }

        public int id_Animale_FK { get; set; }

        public virtual Animale Animale { get; set; }
    }
}
