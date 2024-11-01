using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modelo.Entidades;


namespace Modelo.AppContext
{
    public class ContextoApp: IdentityDbContext
    {
        public ContextoApp(DbContextOptions<ContextoApp> opcion): base(opcion) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ExamenMedico>()
                .HasOne(R => R.Paciente)
                .WithMany(R => R.ExamenMedico)
                .HasForeignKey(R => R.IdPaciente);

            builder.Entity<ExamenMedico>()
               .HasOne(R => R.Prueba)
               .WithMany(R => R.ExamenMedico)
               .HasForeignKey(R => R.IdPrueba);

            builder.Entity<Cita>()
                .HasOne(R => R.Medico)
                .WithMany(R => R.Citas)
                .HasForeignKey(R => R.MedicoId);

            builder.Entity<Cita>()
                .HasOne(R => R.Paciente)
                .WithMany(R => R.Citas)
                .HasForeignKey(R => R.PacienteId);
        }

        public DbSet<Cita> Citas  { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prueba> Pruebas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ExamenMedico> ExamenesMedicos { get; set; }
    }
}
