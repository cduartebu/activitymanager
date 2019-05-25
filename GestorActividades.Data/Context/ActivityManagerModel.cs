namespace GestorActividades.Data.Context
{
    using GestorActividades.Infrastructure.Models;
    using System.Data.Entity;

    public partial class ActivityManagerModel : DbContext
    {
        public ActivityManagerModel()
            : base("name=ActivityManager")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Deliverable> Deliverables { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}