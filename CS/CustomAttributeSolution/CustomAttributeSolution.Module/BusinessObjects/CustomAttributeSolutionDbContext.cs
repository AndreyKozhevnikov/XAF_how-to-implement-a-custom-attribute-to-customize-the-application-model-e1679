using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using dxTestSolution.Module.BusinessObjects;

namespace CustomAttributeSolution.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class CustomAttributeSolutionContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<CustomAttributeSolutionEFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new CustomAttributeSolutionEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class CustomAttributeSolutionDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomAttributeSolutionEFCoreDbContext> {
	public CustomAttributeSolutionEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<CustomAttributeSolutionEFCoreDbContext>();
		//optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CustomAttributeSolution");
        //optionsBuilder.UseChangeTrackingProxies();
        //optionsBuilder.UseObjectSpaceLinkProxies();
		//return new CustomAttributeSolutionEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(CustomAttributeSolutionContextInitializer))]
public class CustomAttributeSolutionEFCoreDbContext : DbContext {
	public CustomAttributeSolutionEFCoreDbContext(DbContextOptions<CustomAttributeSolutionEFCoreDbContext> options) : base(options) {
	}
    //public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<MyTask> MyTasks { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
    }
}
