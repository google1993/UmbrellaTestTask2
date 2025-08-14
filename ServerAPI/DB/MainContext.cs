using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;
using ServerAPI.DB;

namespace ServerAPI.DB
{

    public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
    {
        public DbSet<License> License { get; set; }
        public DbSet<LicenseKey> LicenseKey { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Errors> Errors { get; set; }
        public DbSet<Dumps> Dumps { get; set; }
        public DbSet<Pics> Pics { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<AccessKeys> AccessKeys { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<Versions> Versions { get; set; }

        public DbSet<UserCompanyActivation> UserCompanyActivation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<License>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired(false);

            modelBuilder.Entity<License>()
                .HasMany(l => l.LicenseKeys)
                .WithOne(lk => lk.License)
                .HasForeignKey(lk => lk.LicenseId)
                .IsRequired(false)
                ;

            modelBuilder.Entity<License>()
            .HasIndex(l => l.MacAddress)
            .IsUnique()
            .HasFilter("[MacAddress] IS NOT NULL");

            modelBuilder.Entity<LicenseKey>()
                .HasIndex(lk => lk.KeyValue);

            modelBuilder.Entity<Versions>()
                .HasIndex(v => v.Version);

            //Errors
            modelBuilder.Entity<Errors>()
                .HasIndex(x => x.ErrorGUID)
                .IsUnique();

            //Dumps
            modelBuilder.Entity<Dumps>()
                .HasOne(x => x.Error)
                .WithOne(x => x.Dump)
                .HasForeignKey<Dumps>(x => x.ErrorId);

            //Pics
            modelBuilder.Entity<Pics>()
                .HasOne(x => x.Error)
                .WithMany(x => x.Pics)
                .HasForeignKey(x => x.ErrorId);

            //Users
            modelBuilder.Entity<Users>()
                .HasIndex(x => x.Login)
                .IsUnique();
            modelBuilder.Entity<Users>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(x => x.ToTable("UserRoles"));
            modelBuilder.Entity<Users>()
                .HasMany(x => x.Permissions)
                .WithMany(x => x.Users)
                .UsingEntity(x => x.ToTable("UserPermissions"));

            //AccessKeys
            modelBuilder.Entity<AccessKeys>()
                .HasIndex(x=>x.Key)
                .IsUnique();
            modelBuilder.Entity<AccessKeys>()
                .HasOne(x => x.User)
                .WithMany(x => x.AccessKeys)
                .HasForeignKey(x => x.UserId);

            //Roles
            modelBuilder.Entity<Roles>()
                .HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .UsingEntity(x => x.ToTable("RolePermissions"));


        }
    }
}
