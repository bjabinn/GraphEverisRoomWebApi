using EverNext.Domain.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("Proyecto")]
    public class Proyecto : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ProyectoId")]
        public int ProyectoId { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column("ClienteId")]
        [Required]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [Column("Persona")]
        [MaxLength]
        public string Persona { get; set; }

        [Column("CuentaCargo")]
        [MaxLength]
        public string CuentaCargo { get; set; }

        [Column("SectorId")]
        [ForeignKey("Sector")]
        public int? SectorId { get; set; }

        [Column("ServicioId")]
        [ForeignKey("Servicio")]
        public int? ServicioId { get; set; }

        [Column("Centro")]
        [ForeignKey("Centro")]
        public int? CentroId { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Cliente Cliente { get; set; }

        [NavigationProperty]
        public virtual Maestro Sector { get; set; }

        [NavigationProperty]
        public virtual Maestro Servicio { get; set; }

        [NavigationProperty]
        public virtual Centro Centro { get; set; }


        #endregion
    }
}