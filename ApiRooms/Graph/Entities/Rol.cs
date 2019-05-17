using EverNext.Domain.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Entities
{
    [Table("Rol")]
    public class Rol : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("RolId")]
        public int RolId { get; set; }

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
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }

        [NavigationProperty]
        public virtual ICollection<PermisoRol> PermisoRol { get; set; }

        #endregion


    }
}