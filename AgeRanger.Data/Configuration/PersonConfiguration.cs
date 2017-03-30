using AgeRanger.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Configuration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("Person");
            Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            Property(c => c.LastName).IsRequired().HasMaxLength(50);
            Property(c => c.Age).IsRequired();
        }
    }
}
