using EverNext.Domain.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRooms.Graph.Entities
{
    [Table("PersonaLibre")]
    public class PersonaLibre : ModifiableEntity
    {
        #region Scalar Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("PersonaLibreId")]
        public int PersonaLibreId { get; set; }

        [Column("NroEmpleado")]
        [Required]
        public int NroEmpleado { get; set; }

        [Column("Nombre")]
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Column("Apellidos")]
        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Column("Categoria")]
        [Required]
        [StringLength(255)]
        public string Categoria { get; set; }

        [Column("Linea")]
        [StringLength(255)]
        public string Linea { get; set; }

        [Column("Celda")]
        [StringLength(255)]
        public string Celda { get; set; }

        [Column("FechaLiberacion")]
        [Required]
        public DateTime FechaLiberacion { get; set; }

        [Column("NecesidadId")]
        [ForeignKey("Necesidad")]
        public int? NecesidadId { get; set; }

        [Column("Comentario")]
        [MaxLength]
        public string Comentario { get; set; }

        [Column("TipoTecnologiaId")]
        [ForeignKey("TipoTecnologia")]
        public int? TipoTecnologiaId { get; set; }

        [Column("SinNecesidadAsignada")]
        [Required]
        public bool SinNecesidadAsignada { get; set; }

        #endregion

        #region Navigation Properties

        [NavigationProperty]
        public virtual Necesidad Necesidad { get; set; }

        [NavigationProperty]
        public virtual Maestro TipoTecnologia { get; set; }

        [NavigationProperty]
        public virtual ICollection<PersonaLibreIdioma> LibreIdiomas { get; set; }

        #endregion
    }
}