using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options )
        : base(options)

    {
        
    }

    public DbSet<Genero> Generos { get; set; }

    public DbSet<Pokemon> Pokemons { get; set; }  

    public DbSet<PokemonTipo> PokemonTipos  { get; set; }

    public DbSet<Regiao> Regioes { get; set; }

    public DbSet<Tipo> Tipos { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Muitos para muitos do Pokemon Tipo
        // Chave da primaria composta
        builder.Entity<PokemonTipo>().HasKey(
            pt => new{pt.PokemonNumero,pt.TipoId}
        );

        //Chave estrangeira PokemonTipo > Pokemon
        builder.Entity<PokemonTipo>()
            .HasOne(pt => pt.Pokemon)
            .WithMany(p => p.Tipos)
            .HasForeignKey(pt => pt.PokemonNumero);

        // Chave estrangeira PokemonTipo- tipo


        builder.Entity<PokemonTipo>()
            .HasOne(p => p.Tipo)
            .WithMany(pt => pt.Pokemons)
            .HasForeignKey(pt => pt.TipoId);
        #endregion
    }
}
