using EverNext.Domain.Model.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("Cliente")]
    public class Cliente : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ClienteId")]
        public int ClienteId { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual ICollection<Proyecto> Proyectos { get; set; }

        #endregion
    }
}