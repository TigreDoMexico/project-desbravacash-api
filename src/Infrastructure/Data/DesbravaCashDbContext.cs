using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public class DesbravaCashDbContext(DbContextOptions<DesbravaCashDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

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
            e.Property(x => x.Cargo).HasColumnName("cargo").IsRequired();
            e.Property(x => x.Admin).HasColumnName("admin").IsRequired();
            e.Property(x => x.UnidadeId).HasColumnName("unidade_id").IsRequired();
            e.HasOne(x => x.Unidade)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.UnidadeId);
        });

        modelBuilder.Entity<Unidade>(e =>
        {
            e.ToTable("unidade");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Nome).HasColumnName("nome").IsRequired();
        });

        modelBuilder.Entity<Transacao>(e =>
        {
            e.ToTable("transacao");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            e.Property(x => x.Status).HasColumnName("status").IsRequired();
            e.Property(x => x.Valor).HasColumnName("valor").IsRequired();
            e.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            e.Property(x => x.CriadoEm).HasColumnName("criado_em").IsRequired();
            e.Property(x => x.CriadoPor).HasColumnName("criado_por").IsRequired();
            e.HasOne(x => x.Unidade)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.UnidadeId);
            e.HasOne(x => x.CriadoPorUsuario)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.CriadoPor);
        });
    }
}
