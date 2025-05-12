using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace ITI.Shipping.Infrastructure.Presistence.Data
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options):IdentityDbContext<ApplicationUser , ApplicationRole , string>(options)
    {
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CitySetting> CitySettings { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderReport> OrderReports { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ShippingType> ShippingTypes { get; set; }
        public virtual DbSet<WeightSetting> WeightSettings { get; set; }
        public virtual DbSet<CourierReport> CourierReports { get; set; }
        public virtual DbSet<SpecialCityCost> SpecialCityCost { get; set; }
        public virtual DbSet<SpecialCourierRegion> SpecialCourierRegion { get; set; }

        //chating 

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Shipment> Shipments { get; set; } // Optional if tracking shipments  


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //chating
            // Configure relationships  
            // تكوينات الـ Chatbot (محسنة)
            builder.Entity<Conversation>(entity =>
            {
                entity.HasIndex(c => c.UserId).IsUnique(false);
                entity.Property(c => c.StartDate).HasDefaultValueSql("GETUTCDATE()");
            });

            builder.Entity<ChatMessage>(entity =>
            {
                entity.Property(m => m.Timestamp).HasDefaultValueSql("GETUTCDATE()");
                entity.HasIndex(m => m.ConversationId);
            });

            // العلاقات مع المستخدمين
            builder.Entity<Conversation>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(null!,DefaultUser.AdminPassword);
            builder.Entity<ApplicationUser>()
                .Property(a => a.CanceledOrder)
                .HasPrecision(18,2);
            builder.Entity<ApplicationUser>()
                .Property(a => a.DeductionCompanyFromOrder)
            .HasPrecision(18,2);
            builder.Entity<ApplicationUser>()
                .Property(a => a.PickupPrice)
            .HasPrecision(18,2);
            builder.Entity<CitySetting>()
                .Property(c => c.StandardShippingCost)
                .HasPrecision(18,2);
            builder.Entity<CitySetting>()
                .Property(c => c.pickupShippingCost)
                .HasPrecision(18,2);
            builder.Entity<Order>()
                .Property(o => o.OrderCost)
                .HasPrecision(18,2);
            builder.Entity<Order>()
                .Property(o => o.TotalWeight)
                .HasPrecision(18,2);
            builder.Entity<Product>()
                .Property(p => p.Weight)
                .HasPrecision(18,2);
            builder.Entity<ShippingType>()
                .Property(s => s.BaseCost)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.CostPerKg)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.MaxWeight)
                .HasPrecision(18,2);
            builder.Entity<WeightSetting>()
                .Property(w => w.MinWeight)
                .HasPrecision(18,2);
            builder.Entity<SpecialCityCost>()
                .Property(s => s.Price)
                .HasPrecision(18,2);
            builder.Entity<Order>()
               .Property(o => o.ShippingCost)
               .HasPrecision(18,2);

            builder.Entity<ApplicationUser>()
             .HasData(new ApplicationUser
             {
                 Id = DefaultUser.AdminId,
                 FullName = "Weso Admin",
                 UserName = DefaultUser.AdminEmail,
                 NormalizedUserName = DefaultUser.AdminEmail.ToUpper(),
                 Email = DefaultUser.AdminEmail,
                 NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
                 SecurityStamp = DefaultUser.AdminSecurityStamp,
                 ConcurrencyStamp = DefaultUser.AdminConcurrencyStamp,
                 PasswordHash = hashedPassword
             });
            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = DefaultRole.AdminRoleId,
                    Name = DefaultRole.Admin,
                    NormalizedName = DefaultRole.Admin.ToUpper(),
                    ConcurrencyStamp = DefaultRole.AdminRoleConcurrencyStamp
                });
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = DefaultRole.AdminRoleId,
                    UserId = DefaultUser.AdminId
                });
            builder.Entity<ApplicationRole>()
              .HasData(new ApplicationRole
              {
                  Id = DefaultRole.CourierRoleId,
                  Name = DefaultRole.Courier,
                  NormalizedName = DefaultRole.Courier.ToUpper(),
                  ConcurrencyStamp = DefaultRole.CourierRoleConcurrencyStamp
              });
            builder.Entity<ApplicationRole>()
              .HasData(new ApplicationRole
              {
                  Id = DefaultRole.MerchantrRoleId,
                  Name = DefaultRole.Merchant,
                  NormalizedName = DefaultRole.Merchant.ToUpper(),
                  ConcurrencyStamp = DefaultRole.MerchantRoleConcurrencyStamp
              });
            string[] CourierPermissions = [
                Permissions.ViewOrders,
                Permissions.UpdateOrders,
                ];
            string[] MerchantPermissions = [
                Permissions.ViewOrders,
                Permissions.AddOrders,
                ];


            var permissions = Permissions.GetAllPermissions();
            var adminClaims = new List<IdentityRoleClaim<string>>();
            var courierClaims = new List<IdentityRoleClaim<string>>();
            var merchantClaims = new List<IdentityRoleClaim<string>>();

            for(int i = 0;i < permissions.Count;i++)
            {
                adminClaims.Add(new IdentityRoleClaim<string>
                {
                    Id = i + 1,
                    RoleId = DefaultRole.AdminRoleId,
                    ClaimType = Permissions.Type,
                    ClaimValue = permissions[i]
                });

                if(i < CourierPermissions.Length)
                {
                    courierClaims.Add(new IdentityRoleClaim<string>
                    {
                        Id = permissions.Count + i + 1,
                        RoleId = DefaultRole.CourierRoleId,
                        ClaimType = Permissions.Type,
                        ClaimValue = CourierPermissions[i]
                    });
                }

                if(i < MerchantPermissions.Length)
                {
                    merchantClaims.Add(new IdentityRoleClaim<string>
                    {
                        Id = permissions.Count * 2 + i + 1,
                        RoleId = DefaultRole.MerchantrRoleId,
                        ClaimType = Permissions.Type,
                        ClaimValue = MerchantPermissions[i]
                    });
                }
            }

            builder.Entity<IdentityRoleClaim<string>>()
                .HasData(adminClaims);
            builder.Entity<IdentityRoleClaim<string>>()
               .HasData(courierClaims);
            builder.Entity<IdentityRoleClaim<string>>()
               .HasData(merchantClaims);


            base.OnModelCreating(builder);
        }
    }
}