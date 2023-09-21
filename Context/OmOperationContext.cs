using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiOmTransaction.Models;

namespace WebApiOmTransaction.Context;

public class OmOperationContext : DbContext
{
    public OmOperationContext(DbContextOptions<OmOperationContext> options)
        : base(options)
    {
    }

    public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Operation> Operations { get; set; } = null!;
    public DbSet<Versement> Versements { get; set; } = null!;
    public DbSet<Entreprise> Entreprises { get; set; } = null!;
    public DbSet<SimCodeAgent> SimCodeAgents { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Utilisateur>()
            .HasMany(u => u.Clients)
            .WithOne(u => u.Utilisateur)
            .HasForeignKey(u => u.UtilisateurId);
        
        modelBuilder.Entity<Utilisateur>()
            .HasMany(u => u.Versements)
            .WithOne(u => u.Utilisateur)
            .HasForeignKey(u => u.UtilisateurId);

        modelBuilder.Entity<Entreprise>()
            .HasMany(s => s.SimCodeAgents)
            .WithOne(s => s.Entreprise)
            .HasForeignKey(s => s.EntrepriseId);

        modelBuilder.Entity<Entreprise>()
            .HasMany(s => s.Clients)
            .WithOne(s => s.Entreprise)
            .HasForeignKey(s => s.EntrepriseId);

        modelBuilder.Entity<Entreprise>()
          .HasMany(s => s.Operations)
          .WithOne(s => s.Entreprise)
          .HasForeignKey(s => s.EntrepriseId);

        modelBuilder.Entity<Entreprise>()
          .HasMany(s => s.Utilisateurs)
          .WithOne(s => s.Entreprise)
          .HasForeignKey(s => s.EntrepriseId);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Operations)
            .WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId);
        
        modelBuilder.Entity<Utilisateur>()
            .HasMany(u => u.Operations)
            .WithOne(u => u.Utilisateur)
            .HasForeignKey(u => u.UtilisateurId);

        modelBuilder.Entity<Role>()
            .HasMany(u => u.Utilisateurs)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Operation>()
            .HasMany(op => op.Versements)
            .WithOne(op => op.Operation)
            .HasForeignKey(op => op.OperationId);

        modelBuilder.Entity<SimCodeAgent>()
           .HasMany(op => op.Operations)
           .WithOne(op => op.simCodeAgent)
           .HasForeignKey(op => op.SimCodeAgentId);

    }

}