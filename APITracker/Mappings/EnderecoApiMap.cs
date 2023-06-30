using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using APITracker.Entities;

namespace APITracker.Mappings
{  
    public class EnderecoApiMap : IEntityTypeConfiguration<EnderecoApi>
    {
        public void Configure(EntityTypeBuilder<EnderecoApi> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder.ToTable("EnderecoApi");

            builder.Property(p => p.Descricao).HasMaxLength(200);

            builder.Property(p => p.Endereco).HasMaxLength(200);

            builder.Property(p => p.Error)
                .HasMaxLength(4000);

            builder.Property(p => p.Body).HasMaxLength(4000);
        }
    }
}
