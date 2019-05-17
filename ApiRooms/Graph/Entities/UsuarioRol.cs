using EverNext.Domain.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Entities
{
    [Table("UsuarioRol")]
    public class UsuarioRol : BaseEntity
    {
        #region Scalar Properties

        [Key, Column("UsuarioId", Order = 0)]
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Key, Column("RolId", Order = 1)]
        [Required]
        [ForeignKey("Rol")]
        public int RolId { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Usuario Usuario { get; set; }

        [NavigationProperty]
        public virtual Rol Rol { get; set; }

        #endregion


    }
}