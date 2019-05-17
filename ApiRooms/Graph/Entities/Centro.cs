using EverNext.Domain.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Entities
{
    [Table("Centro")]
    public class Centro : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("CentroId")]
        public int CentroId { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        [Column("CuentaTokenId")]
        [ForeignKey("CuentaToken")]
        public int? CuentaTokenId { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual ICollection<Usuario> Usuarios { get; set; }

        [NavigationProperty]
        public virtual ICollection<Proyecto> Proyectos { get; set; }

        [NavigationProperty]
        public virtual CuentaToken CuentaToken { get; set; }



        #endregion
    }
}