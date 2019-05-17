using EverNext.Domain.Model.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    public class ModifiableEntity : BaseEntity
    {
        #region Scalar Properties

        [Column("UsuarioCreacionId")]
        [Required]
        [ForeignKey("Usuario")]
        public int CreatedBy { get; set; }

        [Column("UsuarioModificacionId")]
        [ForeignKey("UsuarioModificacion")]
        public int? ModifiedBy { get; set; }

        [Column("FechaCreacion")]
        [Required]
        public DateTime Created { get; set; }

        [Column("FechaModificacion")]
        public DateTime? Modified { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Usuario Usuario { get; set; }

        [NavigationProperty]
        public virtual Usuario UsuarioModificacion { get; set; }

        #endregion
    }
}