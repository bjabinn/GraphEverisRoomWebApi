using EverNext.Domain.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("Maestro")]
    public class Maestro : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("MaestroId")]
        public int MaestroId { get; set; }

        [Column("TipoMaestroId")]
        [Required]
        [ForeignKey("TipoMaestro")]
        public int TipoMaestroId { get; set; }

        [Column("Maestro")]
        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [Column("Orden")]
        public int? Orden { get; set; }

        [Column("CentroId")]
        [ForeignKey("Centro")]
        public int? CentroId { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual TipoMaestro TipoMaestro { get; set; }

        [NavigationProperty]
        public virtual Centro Centro { get; set; }

        #endregion
    }
}