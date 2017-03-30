using AgeRanger.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Configuration
{
    public class AgeGroupConfiguration: EntityTypeConfiguration<AgeGroup>
    {
        public AgeGroupConfiguration()
        {
            ToTable("AgeGroup");
            Property(g => g.Description).IsRequired().HasMaxLength(50);
        }
    }
}
