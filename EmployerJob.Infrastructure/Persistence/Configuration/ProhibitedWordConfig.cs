using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmployerJob.Domain.Entities;

namespace EmployerJob.Infrastructure.Persistence.Configuration
{
    internal class ProhibitedWordConfig : IEntityTypeConfiguration<ProhibitedWord>
    {
        public void Configure(EntityTypeBuilder<ProhibitedWord> builder)
        {
            builder.HasKey(r => r.Id);

            builder.ToTable("ProhibitedWords");

            builder.HasData(GetProhibitedWords());
        }

        private ProhibitedWord[] GetProhibitedWords()
        {
            return new ProhibitedWord[]
            {
                new ProhibitedWord
                {
                    Id = 1,
                    Word = "sakıncalı1"
                },

                new ProhibitedWord
                {
                    Id = 2,
                    Word = "sakıncalı2"
                }
            };
        }
    }
}
