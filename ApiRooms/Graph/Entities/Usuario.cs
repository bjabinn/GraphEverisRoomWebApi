using EverNext.Domain.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Entities
{
        [Table("Usuario")]
        public class Usuario : BaseEntity
        {
            #region Scalar Properties

            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("UsuarioId")]
            public int UsuarioId { get; set; }

            [Column("Usuario")]
            [Required]
            [StringLength(100)]
            public string Nombre { get; set; }

            [Column("Username")]
            [Required]
            [StringLength(100)]
            public string UserName { get; set; }

            [Column("Email")]
            [StringLength(100)]
            public string Email { get; set; }

            [Column("Aplicacion")]
            [StringLength(100)]
            public string Aplicacion { get; set; }

            [Column("Centro")]
            [ForeignKey("Centro")]
            public int? CentroId { get; set; }

            #endregion

            #region Navigation Properties

            [NavigationProperty]
            public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }


            [NavigationProperty]
            public virtual Centro Centro { get; set; }
            #endregion
        }
}