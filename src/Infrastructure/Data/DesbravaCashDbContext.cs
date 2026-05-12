using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Solicitacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public class DesbravaCashDbContext(DbContextOptions<DesbravaCashDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Unidade> Unidades => Set<Unidade>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();
    public DbSet<Desafio> Desafios => Set<Desafio>();
    public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();

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
            e.Property(x => x.Role).HasColumnName("role").IsRequired();
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

        modelBuilder.Entity<Desafio>(e =>
        {
            e.ToTable("desafio");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            e.Property(x => x.Pontuacao).HasColumnName("pontuacao").IsRequired();
            e.Property(x => x.DataConclusao).HasColumnName("data_conclusao").IsRequired();
            e.Property(x => x.PodeSolicitar).HasColumnName("pode_solicitar").IsRequired();
            e.Ignore(x => x.Solicitado);
            e.Ignore(x => x.Concluido);
        });
        
        modelBuilder.Entity<Transacao>(e =>
        {
            e.ToTable("transacao");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
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

        modelBuilder.Entity<Solicitacao>(e =>
        {
            e.ToTable("solicitacao");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            e.Property(x => x.Status).HasColumnName("status").IsRequired();
            e.Property(x => x.Valor).HasColumnName("valor").IsRequired();
            e.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();
            e.Property(x => x.CriadoEm).HasColumnName("criado_em").IsRequired();
            e.Property(x => x.CriadoPor).HasColumnName("criado_por").IsRequired();
            e.Property(x => x.UnidadeId).HasColumnName("unidade_id").IsRequired();
            e.Property(x => x.DesafioId).HasColumnName("desafio_id");
            e.Property(x => x.TransacaoId).HasColumnName("transacao_id");
            e.HasOne(x => x.Unidade)
                .WithMany()
                .HasForeignKey(x => x.UnidadeId);
            e.HasOne(x => x.CriadoPorUsuario)
                .WithMany()
                .HasForeignKey(x => x.CriadoPor);
            e.HasOne(x => x.Desafio)
                .WithMany()
                .HasForeignKey(x => x.DesafioId);
            e.HasOne(x => x.Transacao)
                .WithMany()
                .HasForeignKey(x => x.TransacaoId);
        });
    }
}
