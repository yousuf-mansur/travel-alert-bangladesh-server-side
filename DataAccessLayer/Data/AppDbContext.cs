using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<BaseCost> BaseCosts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DayWiseTourCost> DayWiseTourCosts { get; set; }
        public virtual DbSet<DayCostCategory> DayCostCategory { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FoodItem> FoodItems { get; set; }
        public virtual DbSet<Guide> Guides { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationGallery> LocationGalleries { get; set; }
        public virtual DbSet<MealType> MealTypes { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageAccommodation> PackageAccommodations { get; set; }
        public virtual DbSet<PackageAccounts> PackageAccounts { get; set; }
        public virtual DbSet<PackageBudget> PackageBudgets { get; set; }
        public virtual DbSet<PackageCategory> PackageCategories { get; set; }
        public virtual DbSet<PackageSubCategory> PackageSubCategories { get; set; }
        public virtual DbSet<PackageDetails> PackageDetails { get; set; }
        public virtual DbSet<PackageIncludes> PackageIncludes { get; set; }
        public virtual DbSet<PackageExcludes> PackageExcludes { get; set; }
        public virtual DbSet<PackageFacility> PackageFacilities { get; set; }
        public virtual DbSet<PackageFAQ> PackageFAQ { get; set; }
        public virtual DbSet<PackageFoodItem> PackageFoodItems { get; set; }
        public virtual DbSet<PackageGallery> PackageGallery { get; set; }
        public virtual DbSet<PackageLocation> PackageLocation { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<PackageTransportation> PackageTransportations { get; set; }
        public virtual DbSet<PackageUser> PackageUsers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; } = default!;
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; } = default!;
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomSubType> RoomSubTypes { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Transportation> Transportations { get; set; }
        public virtual DbSet<TransportationType> TransportationTypes { get; set; }
        public virtual DbSet<TransportProvider> TransportProviders { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<PromotionImage> PromotionImages { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }        
        public virtual DbSet<TourVoucher> TourVouchers { get; set; }
        public virtual DbSet<Transportation> Transportation { get; set; }
        public virtual DbSet<TransportationCatagory> TransportationCatagories { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<RequestUrl> RequestUrls { get; set; }
        public virtual DbSet<UrlService> UrlServices { get; set; }
        public virtual DbSet<CurrentUrl> CurrentUrls { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Seed RequestUrl with paths
            modelBuilder.Entity<RequestUrl>().HasData(
                new RequestUrl { RequestUrlId = 1, Url = "/api", UrlName = "API Base URL" },
                new RequestUrl { RequestUrlId = 2, Url = "/dashboard", UrlName = "Dashboard" },
                new RequestUrl { RequestUrlId = 3, Url = "/users", UrlName = "List Users" },
                new RequestUrl { RequestUrlId = 4, Url = "/users/add", UrlName = "Add User" },
                new RequestUrl { RequestUrlId = 5, Url = "/users/edit/:id", UrlName = "Edit User" },
                new RequestUrl { RequestUrlId = 6, Url = "/categories", UrlName = "List Categories" },
                new RequestUrl { RequestUrlId = 7, Url = "/categories/add", UrlName = "Add Category" },
                new RequestUrl { RequestUrlId = 8, Url = "/categories/edit/:id", UrlName = "Edit Category" },
                new RequestUrl { RequestUrlId = 9, Url = "/sub-categories", UrlName = "List Sub Categories" },
                new RequestUrl { RequestUrlId = 10, Url = "/sub-categories/add", UrlName = "Add Sub Category" },
                new RequestUrl { RequestUrlId = 11, Url = "/sub-categories/edit/:id", UrlName = "Edit Sub Category" },
                new RequestUrl { RequestUrlId = 12, Url = "/countries", UrlName = "List Countries" },
                new RequestUrl { RequestUrlId = 13, Url = "/countries/add", UrlName = "Add Country" },
                new RequestUrl { RequestUrlId = 14, Url = "/countries/edit/:id", UrlName = "Edit Country" },
                new RequestUrl { RequestUrlId = 15, Url = "/states", UrlName = "List States" },
                new RequestUrl { RequestUrlId = 16, Url = "/states/add", UrlName = "Add State" },
                new RequestUrl { RequestUrlId = 17, Url = "/states/edit/:id", UrlName = "Edit State" },
                new RequestUrl { RequestUrlId = 18, Url = "/packages", UrlName = "List Packages" },
                new RequestUrl { RequestUrlId = 19, Url = "/packages/add", UrlName = "Add Package" },
                new RequestUrl { RequestUrlId = 20, Url = "/packages/edit/:id", UrlName = "Edit Package" },
                new RequestUrl { RequestUrlId = 21, Url = "/packages/details/add/:id", UrlName = "Add Package Details" },
                new RequestUrl { RequestUrlId = 22, Url = "/schedules", UrlName = "List Schedules" },
                new RequestUrl { RequestUrlId = 23, Url = "/schedules/add", UrlName = "Add Schedule" },
                new RequestUrl { RequestUrlId = 24, Url = "/schedules/edit/:id", UrlName = "Edit Schedule" },
                new RequestUrl { RequestUrlId = 25, Url = "/tour-vouchers", UrlName = "List Tour Vouchers" },
                new RequestUrl { RequestUrlId = 26, Url = "/tour-vouchers/add", UrlName = "Add Tour Voucher" },
                new RequestUrl { RequestUrlId = 27, Url = "/tour-vouchers/edit/:id", UrlName = "Edit Tour Voucher" },
                new RequestUrl { RequestUrlId = 28, Url = "/students", UrlName = "List Students" },
                new RequestUrl { RequestUrlId = 29, Url = "/students/add", UrlName = "Add Student" },
                new RequestUrl { RequestUrlId = 30, Url = "/students/edit/:id", UrlName = "Edit Student" },
                new RequestUrl { RequestUrlId = 31, Url = "/more/path/example", UrlName = "More Example Path" }
            );

            // Seed CurrentUrl with paths
            modelBuilder.Entity<CurrentUrl>().HasData(
                new CurrentUrl { CurrentUrlId = 1, Url = "/api", Title = "API Base URL" },
                new CurrentUrl { CurrentUrlId = 2, Url = "/dashboard", Title = "Dashboard" },
                new CurrentUrl { CurrentUrlId = 3, Url = "/users", Title = "List Users" },
                new CurrentUrl { CurrentUrlId = 4, Url = "/users/add", Title = "Add User" },
                new CurrentUrl { CurrentUrlId = 5, Url = "/users/edit/:id", Title = "Edit User" },
                new CurrentUrl { CurrentUrlId = 6, Url = "/categories", Title = "List Categories" },
                new CurrentUrl { CurrentUrlId = 7, Url = "/categories/add", Title = "Add Category" },
                new CurrentUrl { CurrentUrlId = 8, Url = "/categories/edit/:id", Title = "Edit Category" },
                new CurrentUrl { CurrentUrlId = 9, Url = "/sub-categories", Title = "List Sub Categories" },
                new CurrentUrl { CurrentUrlId = 10, Url = "/sub-categories/add", Title = "Add Sub Category" },
                new CurrentUrl { CurrentUrlId = 11, Url = "/sub-categories/edit/:id", Title = "Edit Sub Category" },
                new CurrentUrl { CurrentUrlId = 12, Url = "/countries", Title = "List Countries" },
                new CurrentUrl { CurrentUrlId = 13, Url = "/countries/add", Title = "Add Country" },
                new CurrentUrl { CurrentUrlId = 14, Url = "/countries/edit/:id", Title = "Edit Country" },
                new CurrentUrl { CurrentUrlId = 15, Url = "/states", Title = "List States" },
                new CurrentUrl { CurrentUrlId = 16, Url = "/states/add", Title = "Add State" },
                new CurrentUrl { CurrentUrlId = 17, Url = "/states/edit/:id", Title = "Edit State" },
                new CurrentUrl { CurrentUrlId = 18, Url = "/packages", Title = "List Packages" },
                new CurrentUrl { CurrentUrlId = 19, Url = "/packages/add", Title = "Add Package" },
                new CurrentUrl { CurrentUrlId = 20, Url = "/packages/edit/:id", Title = "Edit Package" },
                new CurrentUrl { CurrentUrlId = 21, Url = "/packages/details/add/:id", Title = "Add Package Details" },
                new CurrentUrl { CurrentUrlId = 22, Url = "/schedules", Title = "List Schedules" },
                new CurrentUrl { CurrentUrlId = 23, Url = "/schedules/add", Title = "Add Schedule" },
                new CurrentUrl { CurrentUrlId = 24, Url = "/schedules/edit/:id", Title = "Edit Schedule" },
                new CurrentUrl { CurrentUrlId = 25, Url = "/tour-vouchers", Title = "List Tour Vouchers" },
                new CurrentUrl { CurrentUrlId = 26, Url = "/tour-vouchers/add", Title = "Add Tour Voucher" },
                new CurrentUrl { CurrentUrlId = 27, Url = "/tour-vouchers/edit/:id", Title = "Edit Tour Voucher" },
                new CurrentUrl { CurrentUrlId = 28, Url = "/students", Title = "List Students" },
                new CurrentUrl { CurrentUrlId = 29, Url = "/students/add", Title = "Add Student" },
                new CurrentUrl { CurrentUrlId = 30, Url = "/students/edit/:id", Title = "Edit Student" },
                new CurrentUrl { CurrentUrlId = 31, Url = "/more/path/example", Title = "More Example Path" }
            );




            //// Seed RequestUrl with paths (you may skip or modify duplicates like base URL "/api")
            //modelBuilder.Entity<RequestUrl>().HasData(
            //    new RequestUrl { RequestUrlId = 1, Url = "/api", UrlName = "API Base URL" },
            //    new RequestUrl { RequestUrlId = 2, Url = "/dashboard", UrlName = "Dashboard" },
            //    new RequestUrl { RequestUrlId = 3, Url = "/users", UrlName = "List Users" },
            //    new RequestUrl { RequestUrlId = 4, Url = "/users/add", UrlName = "Add User" },
            //    new RequestUrl { RequestUrlId = 5, Url = "/users/edit/:id", UrlName = "Edit User" },

            //    new RequestUrl { RequestUrlId = 6, Url = "/categories", UrlName = "List Categories" },
            //    new RequestUrl { RequestUrlId = 7, Url = "/categories/add", UrlName = "Add Category" },
            //    new RequestUrl { RequestUrlId = 8, Url = "/categories/edit/:id", UrlName = "Edit Category" },

            //    new RequestUrl { RequestUrlId = 9, Url = "/sub-categories", UrlName = "List Sub Categories" },
            //    new RequestUrl { RequestUrlId = 10, Url = "/sub-categories/add", UrlName = "Add Sub Category" },
            //    new RequestUrl { RequestUrlId = 11, Url = "/sub-categories/edit/:id", UrlName = "Edit Sub Category" },

            //    new RequestUrl { RequestUrlId = 12, Url = "/countries", UrlName = "List Countries" },
            //    new RequestUrl { RequestUrlId = 13, Url = "/countries/add", UrlName = "Add Country" },
            //    new RequestUrl { RequestUrlId = 14, Url = "/countries/edit/:id", UrlName = "Edit Country" },

            //    new RequestUrl { RequestUrlId = 15, Url = "/states", UrlName = "List States" },
            //    new RequestUrl { RequestUrlId = 16, Url = "/states/add", UrlName = "Add State" },
            //    new RequestUrl { RequestUrlId = 17, Url = "/states/edit/:id", UrlName = "Edit State" },

            //    new RequestUrl { RequestUrlId = 18, Url = "/packages", UrlName = "List Packages" },
            //    new RequestUrl { RequestUrlId = 19, Url = "/packages/add", UrlName = "Add Package" },
            //    new RequestUrl { RequestUrlId = 20, Url = "/packages/edit/:id", UrlName = "Edit Package" },
            //    new RequestUrl { RequestUrlId = 21, Url = "/packages/details/add/:id", UrlName = "Add Package Details" },

            //    new RequestUrl { RequestUrlId = 22, Url = "/schedules", UrlName = "List Schedules" },
            //    new RequestUrl { RequestUrlId = 23, Url = "/schedules/add", UrlName = "Add Schedule" },
            //    new RequestUrl { RequestUrlId = 24, Url = "/schedules/edit/:id", UrlName = "Edit Schedule" },

            //    new RequestUrl { RequestUrlId = 25, Url = "/tour-vouchers", UrlName = "List Tour Vouchers" },
            //    new RequestUrl { RequestUrlId = 26, Url = "/tour-vouchers/add", UrlName = "Add Tour Voucher" },
            //    new RequestUrl { RequestUrlId = 27, Url = "/tour-vouchers/edit/:id", UrlName = "Edit Tour Voucher" },

            //    new RequestUrl { RequestUrlId = 28, Url = "/students", UrlName = "List Students" },
            //    new RequestUrl { RequestUrlId = 29, Url = "/students/add", UrlName = "Add Student" },
            //    new RequestUrl { RequestUrlId = 30, Url = "/students/edit/:id", UrlName = "Edit Student" },

            //    new RequestUrl { RequestUrlId = 31, Url = "/more/path/example", UrlName = "More Example Path" }
            //);

            //// Seed UrlService with paths mapped to RequestUrlId
            //modelBuilder.Entity<UrlService>().HasData(
            //    new UrlService { UrlServiceId = 1, CurrentUrl = "/dashboard", RequestUrlId = 2, Description = "Dashboard" },

            //    new UrlService { UrlServiceId = 2, CurrentUrl = "/users", RequestUrlId = 3, Description = "List Users" },
            //    new UrlService { UrlServiceId = 3, CurrentUrl = "/users/add", RequestUrlId = 4, Description = "Add User" },
            //    new UrlService { UrlServiceId = 4, CurrentUrl = "/users/edit/:id", RequestUrlId = 5, Description = "Edit User" },

            //    new UrlService { UrlServiceId = 5, CurrentUrl = "/categories", RequestUrlId = 6, Description = "List Categories" },
            //    new UrlService { UrlServiceId = 6, CurrentUrl = "/categories/add", RequestUrlId = 7, Description = "Add Category" },
            //    new UrlService { UrlServiceId = 7, CurrentUrl = "/categories/edit/:id", RequestUrlId = 8, Description = "Edit Category" },

            //    new UrlService { UrlServiceId = 8, CurrentUrl = "/sub-categories", RequestUrlId = 9, Description = "List Sub Categories" },
            //    new UrlService { UrlServiceId = 9, CurrentUrl = "/sub-categories/add", RequestUrlId = 10, Description = "Add Sub Category" },
            //    new UrlService { UrlServiceId = 10, CurrentUrl = "/sub-categories/edit/:id", RequestUrlId = 11, Description = "Edit Sub Category" },

            //    new UrlService { UrlServiceId = 11, CurrentUrl = "/countries", RequestUrlId = 12, Description = "List Countries" },
            //    new UrlService { UrlServiceId = 12, CurrentUrl = "/countries/add", RequestUrlId = 13, Description = "Add Country" },
            //    new UrlService { UrlServiceId = 13, CurrentUrl = "/countries/edit/:id", RequestUrlId = 14, Description = "Edit Country" },

            //    new UrlService { UrlServiceId = 14, CurrentUrl = "/states", RequestUrlId = 15, Description = "List States" },
            //    new UrlService { UrlServiceId = 15, CurrentUrl = "/states/add", RequestUrlId = 16, Description = "Add State" },
            //    new UrlService { UrlServiceId = 16, CurrentUrl = "/states/edit/:id", RequestUrlId = 17, Description = "Edit State" },

            //    new UrlService { UrlServiceId = 17, CurrentUrl = "/packages", RequestUrlId = 18, Description = "List Packages" },
            //    new UrlService { UrlServiceId = 18, CurrentUrl = "/packages/add", RequestUrlId = 19, Description = "Add Package" },
            //    new UrlService { UrlServiceId = 19, CurrentUrl = "/packages/edit/:id", RequestUrlId = 20, Description = "Edit Package" },
            //    new UrlService { UrlServiceId = 20, CurrentUrl = "/packages/details/add/:id", RequestUrlId = 21, Description = "Add Package Details" },

            //    new UrlService { UrlServiceId = 21, CurrentUrl = "/schedules", RequestUrlId = 22, Description = "List Schedules" },
            //    new UrlService { UrlServiceId = 22, CurrentUrl = "/schedules/add", RequestUrlId = 23, Description = "Add Schedule" },
            //    new UrlService { UrlServiceId = 23, CurrentUrl = "/schedules/edit/:id", RequestUrlId = 24, Description = "Edit Schedule" },

            //    new UrlService { UrlServiceId = 24, CurrentUrl = "/tour-vouchers", RequestUrlId = 25, Description = "List Tour Vouchers" },
            //    new UrlService { UrlServiceId = 25, CurrentUrl = "/tour-vouchers/add", RequestUrlId = 26, Description = "Add Tour Voucher" },
            //    new UrlService { UrlServiceId = 26, CurrentUrl = "/tour-vouchers/edit/:id", RequestUrlId = 27, Description = "Edit Tour Voucher" },

            //    new UrlService { UrlServiceId = 27, CurrentUrl = "/students", RequestUrlId = 28, Description = "List Students" },
            //    new UrlService { UrlServiceId = 28, CurrentUrl = "/students/add", RequestUrlId = 29, Description = "Add Student" },
            //    new UrlService { UrlServiceId = 29, CurrentUrl = "/students/edit/:id", RequestUrlId = 30, Description = "Edit Student" },

            //    new UrlService { UrlServiceId = 30, CurrentUrl = "/more/path/example", RequestUrlId = 31, Description = "More Example Path" }
            //);






            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<RoomType>()
            //.HasMany(rt => rt.RoomSubTypes)
            //.WithOne(rs => rs.RoomType)
            //.HasForeignKey(rs => rs.RoomTypeID)
            //.OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomSubType)
                .WithMany(rs => rs.Rooms)
                .HasForeignKey(r => r.RoomSubTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Package>()
            .HasOne(p => p.PackageCategory)
            .WithMany(c => c.Packages)
            .HasForeignKey(p => p.PackageCategoryID);

            modelBuilder.Entity<PackageCategory>()
                .HasMany(c => c.PackageSubCategories)
                .WithOne(sc => sc.PackageCategory)
                .HasForeignKey(sc => sc.PackageCategoryID);


            //modelBuilder.Entity<TourVoucher>()
            //    .HasOne(tv => tv.Package)
            //    .WithMany(p => p.TourVouchers)
            //    .HasForeignKey(tv => tv.PackageID)
            //    .OnDelete(DeleteBehavior.Restrict); 


            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.TourVoucher)
                .WithMany(tv => tv.Schedules)
                .HasForeignKey(s => s.TourVoucherID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Package)
                .WithMany(p => p.Schedule)
                .HasForeignKey(s => s.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PackageAccommodation>()
                .HasKey(pa => pa.PackageAccommodationID);

            modelBuilder.Entity<PackageAccommodation>()
                .HasOne(pa => pa.Room)
                .WithMany(h => h.PackageAccommodations)
                .HasForeignKey(pa => pa.RoomID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackageAccommodation>()
                .HasOne(pa => pa.Package)
                .WithMany(p => p.PackageAccommodations)
                .HasForeignKey(pa => pa.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .Property(r => r.AveragePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DayWiseTourCost>(entity =>
            {
                entity.HasKey(d => d.DayWiseTourCostID);


                entity.HasOne(d => d.DayCostCategory)
                      .WithMany(c => c.DayWiseTourCosts)
                      .HasForeignKey(d => d.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<DayCostCategory>(entity =>
            {
                entity.HasKey(c => c.DayCostCategoryID);

                entity.HasMany(c => c.Schedules)
                      .WithOne(s => s.DayCostCategory)
                      .HasForeignKey(s => s.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DayWiseTourCost>()
               .Property(r => r.OtherCost)
               .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Schedule>()
                           .Property(r => r.TentativeCost)
                           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Schedule>()
                           .Property(r => r.ActualCost)
                           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DayWiseTourCost>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalFoodCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalTransPortCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalAccomodationCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalOtherCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalEarn)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalLoss)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageAccounts>()
                .Property(r => r.TotalProfit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimateedFoodCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimatedTransportCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.EstimatedAccomodationCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.OtherCost)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<PackageBudget>()
                .Property(r => r.ProfitPercent)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageFoodItem>()
                .Property(r => r.FoodQuantity)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageFoodItem>()
                .Property(r => r.ItemTotalCost)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<Payment>()
                .Property(r => r.FinalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageTransportation>()
                .Property(r => r.PerHeadTransportCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageTransportation>()
               .Property(r => r.ItemTransportTotalCost)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(r => r.AmountPaid)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Promotion>()
                .Property(r => r.DiscountPercentage)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<DayWiseTourCost>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Schedule>(entity =>
            {

                entity.HasOne(s => s.DayCostCategory)
                      .WithMany(d => d.Schedules)
                      .HasForeignKey(s => s.DayCostCategoryID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DayCostCategory>(entity =>
            {
                entity.HasKey(d => d.DayCostCategoryID);
            });


            modelBuilder.Entity<PackageUser>()
                .HasKey(pg => pg.PackageUserID);

            modelBuilder.Entity<PackageUser>()
                .HasOne(pg => pg.Package)
                .WithMany(p => p.PackageUsers)
                .HasForeignKey(pg => pg.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DayWiseTourCost>()
             .HasOne(p => p.Package)
             .WithMany(b => b.DayWiseTourCosts)
             .HasForeignKey(p => p.PackageID)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DayWiseTourCost>()
                .Property(p => p.TotalCost)
                .HasComputedColumnSql("0");


            modelBuilder.Entity<PackageUser>()
                .HasOne(pg => pg.ApplicationUser)
                .WithMany(au => au.PackageUsers)
                .HasForeignKey(pg => pg.ApplicationUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Package)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PackageID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Package)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PackageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
