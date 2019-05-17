using ApiRooms.Graph.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph.Entities
{
    [Table("CuentaToken")]
    public class CuentaToken : BaseEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("CuentaTokenId")]
        public int CuentaTokenId { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("Token")]
        [Required]
        public string Token { get; set; }

        [Column("FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("FechaExpiracion")]
        public DateTime? FechaExpiracion { get; set; }

        [Column("FechaCreacionToken")]
        public DateTime? FechaCreacionToken { get; set; }

        [Column("FechaExpiracionToken")]
        public DateTime? FechaExpiracionToken { get; set; }


        #endregion

        #region Navigation Properties

        public virtual ICollection<Centro> Centros { get; set; }


        #endregion
    }
}
