using EverNext.Domain.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("PermisoRol")]
    public class PermisoRol : BaseEntity
    {
        #region Scalar Properties

        [Key, Column("PermisoId", Order = 0)]
        [Required]
        [ForeignKey("Permiso")]
        public int PermisoId { get; set; }

        [Key, Column("RolId", Order = 1)]
        [Required]
        [ForeignKey("Rol")]
        public int RolId { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Permiso Permiso { get; set; }

        [NavigationProperty]
        public virtual Rol Rol { get; set; }

        #endregion
    }
}