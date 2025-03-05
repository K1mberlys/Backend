using Data.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected DataContext() { }

        public DbSet<AddressTypeEntity> Addresses { get; set; }
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<ArticlePriceListEntity> ArticlePriceLists { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<ProjectArticleEntity> ProjectArticles { get; set; }
        public DbSet<CustomerAddressEntity> CustomersAddresses { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserAddressEntity> UserAddresses { get; set; }
        public DbSet<PostalCodeEntity> Postalcodes { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; } = null!;
        public DbSet<StatusTypeEntity> Statuses { get; set; }
        public DbSet<UserRolesEntity> UserRoles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        //Tagit hjälp av ChatGTP 4o
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<UserRolesEntity>().HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRolesEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<UserRolesEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.SetNull); 

           
            modelBuilder.Entity<RoleEntity>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<StatusTypeEntity>()
                .HasMany(s => s.Projects)
                .WithOne(p => p.StatusType)
                .HasForeignKey(p => p.StatusTypeId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<InvoiceEntity>()
                .HasOne(i => i.Project)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<InvoiceEntity>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProjectEntity>()
            //    .HasOne(p => p.StatusType)
            //    .WithMany()
            //    .HasForeignKey(p => p.StatusTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectEntity>()
            //      .HasOne(p => p.Customer)
            //      .WithMany(c => c.Projects)
            //      .HasForeignKey(p => p.CustomerId)
            //      .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectEntity>()
            //    .HasOne(p => p.ProjectManager)
            //    .WithMany()
            //    .HasForeignKey(p => p.ProjectManagerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectArticleEntity>()
            //   .HasOne(pa => pa.Project)
            //   .WithMany(p => p.ProjectArticles)
            //   .HasForeignKey(pa => pa.ProjectId)
            //   .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


       
    }
}
