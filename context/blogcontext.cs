using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace blog.Models
{
    public class blogcontext : DbContext
    {


        public blogcontext()
            : base("name=blogcontext")
        {

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<blogcontext, Migrations.Configuration>("blogcontext"));
            Database.SetInitializer<blogcontext>(null);

        }

        public DbSet<kullanici> kullanicilar { get; set; }
        public DbSet<blog> bloglar { get; set; }
        public DbSet<token> tokens { get; set; }
        public DbSet<rol> rols { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            base.OnModelCreating(modelBuilder);

        }
        public class blogcontextDbInitializer : DropCreateDatabaseIfModelChanges<blogcontext>
        {
            protected override void Seed(blogcontext context)
            {
                base.Seed(context);
            }
        }

    }
}
