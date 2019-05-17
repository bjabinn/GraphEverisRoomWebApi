using ApiRooms.Graph.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Graph
{
    public class RecruitingDbContext:DbContext
    {
        // Define an IDbSet for each Entity...
        public virtual IDbSet<Cliente> Cliente { get; set; }
        public virtual IDbSet<Maestro> Maestro { get; set; }
        public virtual IDbSet<Proyecto> Proyecto { get; set; }
        public virtual IDbSet<TipoMaestro> TipoMaestro { get; set; }
        public virtual IDbSet<Usuario> Usuario { get; set; }
        public virtual IDbSet<Centro> Centro { get; set; }
        public virtual IDbSet<Oficina> Oficina { get; set; }
        public virtual IDbSet<CuentaToken> CuentaToken { get; set; }
        public virtual IDbSet<BlackListSala> BlackListSala { get; set; }

        public RecruitingDbContext()
            : base("EverisRecruiting")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonaLibreIdioma>()
                .HasRequired<Maestro>(m => m.NivelIdioma)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonaLibreIdioma>()
                .HasRequired<Maestro>(m => m.Idioma)
                .WithMany()
                .WillCascadeOnDelete(false);

           
        }
    }
}