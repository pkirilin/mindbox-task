using GeometricService.Domain;
using GeometricService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GeometricService.Infrastructure
{
    class GeometricServiceContext : DbContext
    {
        public GeometricServiceContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Figure> Figures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Figure>()
                .Property(f => f.Parameters)
                .HasColumnType("BLOB")
                .HasConversion(
                    parameters => FigureParametersConverter.ConvertToBytes(parameters),
                    blobBytes => FigureParametersConverter.ConvertToDoubleArray(blobBytes));
        }
    }
}
