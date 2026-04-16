using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public class DesbravaCashDbContext(DbContextOptions<DesbravaCashDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("usuario");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            e.Property(x => x.Telefone).HasColumnName("telefone").IsRequired();
            e.Property(x => x.Senha).HasColumnName("senha").IsRequired();
        });
    }
}
