using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.Data.Configuration;
using AgeRanger.Model;


namespace AgeRanger.Data
{
    public class AgeRangerEntities : DbContext
    {
        public AgeRangerEntities() : base("AgeRangerEntities") { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            //modelBuilder.Configurations.Add(new CategoryConfiguration());
        }
    }
}
