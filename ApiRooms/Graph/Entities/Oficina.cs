using EverNext.Domain.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("Oficina")]
    public class Oficina : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("OficinaId")]
        public int OficinaId { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [Column("CentroId")]
        [ForeignKey("CentroId")]
        public int Centro { get; set; }


        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Centro CentroId { get; set; }

        #endregion
    }
}