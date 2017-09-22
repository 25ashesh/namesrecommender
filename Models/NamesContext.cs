using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NamesRecommender.Models
{
    public class NamesContext:DbContext
    {
        public DbSet<NameDetail> Names { get; set; }
        public DbSet<NameGender> Genders { get; set; }
        public DbSet<NameCategory> Categories { get; set; }
        public DbSet<NameType> Types { get; set; }
        public DbSet<NameOrigin> Origins { get; set; }
        public DbSet<NameLength> Lengths { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}