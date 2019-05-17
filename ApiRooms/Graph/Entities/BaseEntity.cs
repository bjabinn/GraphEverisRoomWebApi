using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    public class BaseEntity
    {
        [Column("Activo")]
        [Required]
        public bool IsActivo { get; set; }
    }
}