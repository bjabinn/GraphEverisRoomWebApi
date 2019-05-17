using EverNext.Domain.Model.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("Permiso")]
    public class Permiso : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("PermisoId")]
        public int PermisoId { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column("Descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual ICollection<PermisoRol> PermisoRol { get; set; }

        #endregion


    }
}