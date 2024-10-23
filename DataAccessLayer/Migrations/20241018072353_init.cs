using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseCosts",
                columns: table => new
                {
                    BaseCostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TentativeCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCosts", x => x.BaseCostID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "CurrentUrls",
                columns: table => new
                {
                    CurrentUrlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentUrls", x => x.CurrentUrlId);
                });

            migrationBuilder.CreateTable(
                name: "DayCostCategory",
                columns: table => new
                {
                    DayCostCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCostCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCostCategory", x => x.DayCostCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityID);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemID);
                });

            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    MealTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.MealTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PackageCategories",
                columns: table => new
                {
                    PackageCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageCategories", x => x.PackageCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    PaymentStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentStatusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionImages",
                columns: table => new
                {
                    PromotionImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionImages", x => x.PromotionImageID);
                });

            migrationBuilder.CreateTable(
                name: "RequestUrls",
                columns: table => new
                {
                    RequestUrlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUrls", x => x.RequestUrlId);
                });

            migrationBuilder.CreateTable(
                name: "RoomSubTypes",
                columns: table => new
                {
                    RoomSubTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSubTypes", x => x.RoomSubTypeID);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TransportationCatagories",
                columns: table => new
                {
                    TransportationCatagoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportationCatagoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationCatagories", x => x.TransportationCatagoryID);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTypes",
                columns: table => new
                {
                    TransportationTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationTypes", x => x.TransportationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TransportProviders",
                columns: table => new
                {
                    TransportProviderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportProviders", x => x.TransportProviderID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    GuideID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferredBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.GuideID);
                    table.ForeignKey(
                        name: "FK_Guides_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateID);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageSubCategories",
                columns: table => new
                {
                    PackageSubCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageCategoryID = table.Column<int>(type: "int", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageSubCategories", x => x.PackageSubCategoryID);
                    table.ForeignKey(
                        name: "FK_PackageSubCategories_PackageCategories_PackageCategoryID",
                        column: x => x.PackageCategoryID,
                        principalTable: "PackageCategories",
                        principalColumn: "PackageCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromotionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromotionImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.PromotionID);
                    table.ForeignKey(
                        name: "FK_Promotions_PromotionImages_PromotionImageID",
                        column: x => x.PromotionImageID,
                        principalTable: "PromotionImages",
                        principalColumn: "PromotionImageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlServices",
                columns: table => new
                {
                    UrlServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentUrlId = table.Column<int>(type: "int", nullable: false),
                    RequestUrlId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlServices", x => x.UrlServiceId);
                    table.ForeignKey(
                        name: "FK_UrlServices_CurrentUrls_CurrentUrlId",
                        column: x => x.CurrentUrlId,
                        principalTable: "CurrentUrls",
                        principalColumn: "CurrentUrlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrlServices_RequestUrls_RequestUrlId",
                        column: x => x.RequestUrlId,
                        principalTable: "RequestUrls",
                        principalColumn: "RequestUrlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportation",
                columns: table => new
                {
                    TransportationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransportProviderID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TransportationTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportation", x => x.TransportationID);
                    table.ForeignKey(
                        name: "FK_Transportation_TransportProviders_TransportProviderID",
                        column: x => x.TransportProviderID,
                        principalTable: "TransportProviders",
                        principalColumn: "TransportProviderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportation_TransportationTypes_TransportationTypeID",
                        column: x => x.TransportationTypeID,
                        principalTable: "TransportationTypes",
                        principalColumn: "TransportationTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Locations_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PackageCategoryID = table.Column<int>(type: "int", nullable: false),
                    PackageSubCategoryID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageID);
                    table.ForeignKey(
                        name: "FK_Packages_PackageCategories_PackageCategoryID",
                        column: x => x.PackageCategoryID,
                        principalTable: "PackageCategories",
                        principalColumn: "PackageCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Packages_PackageSubCategories_PackageSubCategoryID",
                        column: x => x.PackageSubCategoryID,
                        principalTable: "PackageSubCategories",
                        principalColumn: "PackageSubCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_Hotels_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationGalleries",
                columns: table => new
                {
                    LocationGalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    ImageCaption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGalleries", x => x.LocationGalleryID);
                    table.ForeignKey(
                        name: "FK_LocationGalleries_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfTravelers = table.Column<int>(type: "int", nullable: false),
                    IsCoupleBooking = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayWiseTourCosts",
                columns: table => new
                {
                    DayWiseTourCostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    OtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "0"),
                    DayCostCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWiseTourCosts", x => x.DayWiseTourCostID);
                    table.ForeignKey(
                        name: "FK_DayWiseTourCosts_DayCostCategory_DayCostCategoryID",
                        column: x => x.DayCostCategoryID,
                        principalTable: "DayCostCategory",
                        principalColumn: "DayCostCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayWiseTourCosts_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageAccounts",
                columns: table => new
                {
                    PackageAccountsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    TotalFoodCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTransPortCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAccomodationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEarn = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageAccounts", x => x.PackageAccountsID);
                    table.ForeignKey(
                        name: "FK_PackageAccounts_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageBudgets",
                columns: table => new
                {
                    PackageBudgetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    EstimateedFoodCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedTransportCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedAccomodationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageBudgets", x => x.PackageBudgetID);
                    table.ForeignKey(
                        name: "FK_PackageBudgets_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageDetails",
                columns: table => new
                {
                    PackageDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    PackageDuration = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickupPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumPerson = table.Column<int>(type: "int", nullable: false),
                    MinimumPerson = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageDetails", x => x.PackageDetailsID);
                    table.ForeignKey(
                        name: "FK_PackageDetails_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageExcludes",
                columns: table => new
                {
                    ExcludeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    ExcludeDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageExcludes", x => x.ExcludeID);
                    table.ForeignKey(
                        name: "FK_PackageExcludes_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageFacilities",
                columns: table => new
                {
                    PackageFacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFacilities", x => x.PackageFacilityID);
                    table.ForeignKey(
                        name: "FK_PackageFacilities_Facilities_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "Facilities",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageFacilities_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageFAQ",
                columns: table => new
                {
                    PackageFAQID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FAQTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FAQDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFAQ", x => x.PackageFAQID);
                    table.ForeignKey(
                        name: "FK_PackageFAQ_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageFoodItems",
                columns: table => new
                {
                    PackageFoodItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealTypeID = table.Column<int>(type: "int", nullable: false),
                    FoodItemID = table.Column<int>(type: "int", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    PackageDayNumber = table.Column<int>(type: "int", nullable: false),
                    FoodQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodUnitPrice = table.Column<double>(type: "float", nullable: false),
                    ItemTotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScheduleTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFoodItems", x => x.PackageFoodItemID);
                    table.ForeignKey(
                        name: "FK_PackageFoodItems_FoodItems_FoodItemID",
                        column: x => x.FoodItemID,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageFoodItems_MealTypes_MealTypeID",
                        column: x => x.MealTypeID,
                        principalTable: "MealTypes",
                        principalColumn: "MealTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageFoodItems_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageGallery",
                columns: table => new
                {
                    PackageGalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    ImageCaption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageGallery", x => x.PackageGalleryID);
                    table.ForeignKey(
                        name: "FK_PackageGallery_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageIncludes",
                columns: table => new
                {
                    IncludeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    IncludeDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageIncludes", x => x.IncludeID);
                    table.ForeignKey(
                        name: "FK_PackageIncludes_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageLocation",
                columns: table => new
                {
                    PackageLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageLocation", x => x.PackageLocationID);
                    table.ForeignKey(
                        name: "FK_PackageLocation_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageLocation_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTransportations",
                columns: table => new
                {
                    PackageTransportationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    TransportationTypeID = table.Column<int>(type: "int", nullable: false),
                    TransportationCatagoryID = table.Column<int>(type: "int", nullable: false),
                    TransportationID = table.Column<int>(type: "int", nullable: false),
                    SeatBooked = table.Column<int>(type: "int", nullable: false),
                    PackageTransportationDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PerHeadTransportCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemTransportTotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTransportations", x => x.PackageTransportationID);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_TransportationCatagories_TransportationCatagoryID",
                        column: x => x.TransportationCatagoryID,
                        principalTable: "TransportationCatagories",
                        principalColumn: "TransportationCatagoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_TransportationTypes_TransportationTypeID",
                        column: x => x.TransportationTypeID,
                        principalTable: "TransportationTypes",
                        principalColumn: "TransportationTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTransportations_Transportation_TransportationID",
                        column: x => x.TransportationID,
                        principalTable: "Transportation",
                        principalColumn: "TransportationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageUsers",
                columns: table => new
                {
                    PackageUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    PackageResponsibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuideID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUsers", x => x.PackageUserID);
                    table.ForeignKey(
                        name: "FK_PackageUsers_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageUsers_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "GuideID");
                    table.ForeignKey(
                        name: "FK_PackageUsers_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TourVouchers",
                columns: table => new
                {
                    TourVoucherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourVoucherCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoucherUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourVouchers", x => x.TourVoucherID);
                    table.ForeignKey(
                        name: "FK_TourVouchers_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID");
                });

            migrationBuilder.CreateTable(
                name: "HotelFacilities",
                columns: table => new
                {
                    HotelFacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilities", x => x.HotelFacilityID);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_Facilities_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "Facilities",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    HotelImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.HotelImageID);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false),
                    RoomSubTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomSubTypes_RoomSubTypeID",
                        column: x => x.RoomSubTypeID,
                        principalTable: "RoomSubTypes",
                        principalColumn: "RoomSubTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    PromotionID = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    SubmittedPromo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripePaymentIntentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStatus_PaymentStatusID",
                        column: x => x.PaymentStatusID,
                        principalTable: "PaymentStatus",
                        principalColumn: "PaymentStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Promotions_PromotionID",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageTransportationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatsID);
                    table.ForeignKey(
                        name: "FK_Seats_PackageTransportations_PackageTransportationID",
                        column: x => x.PackageTransportationID,
                        principalTable: "PackageTransportations",
                        principalColumn: "PackageTransportationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourVoucherID = table.Column<int>(type: "int", nullable: false),
                    ScheduleTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    TentativeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TentativeCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DayCostCategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedule_DayCostCategory_DayCostCategoryID",
                        column: x => x.DayCostCategoryID,
                        principalTable: "DayCostCategory",
                        principalColumn: "DayCostCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_TourVouchers_TourVoucherID",
                        column: x => x.TourVoucherID,
                        principalTable: "TourVouchers",
                        principalColumn: "TourVoucherID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackageAccommodations",
                columns: table => new
                {
                    PackageAccommodationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageAccommodations", x => x.PackageAccommodationID);
                    table.ForeignKey(
                        name: "FK_PackageAccommodations_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackageAccommodations_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CurrentUrls",
                columns: new[] { "CurrentUrlId", "Title", "Url" },
                values: new object[,]
                {
                    { 1, "API Base URL", "/api" },
                    { 2, "Dashboard", "/dashboard" },
                    { 3, "List Users", "/users" },
                    { 4, "Add User", "/users/add" },
                    { 5, "Edit User", "/users/edit/:id" },
                    { 6, "List Categories", "/categories" },
                    { 7, "Add Category", "/categories/add" },
                    { 8, "Edit Category", "/categories/edit/:id" },
                    { 9, "List Sub Categories", "/sub-categories" },
                    { 10, "Add Sub Category", "/sub-categories/add" },
                    { 11, "Edit Sub Category", "/sub-categories/edit/:id" },
                    { 12, "List Countries", "/countries" },
                    { 13, "Add Country", "/countries/add" },
                    { 14, "Edit Country", "/countries/edit/:id" },
                    { 15, "List States", "/states" },
                    { 16, "Add State", "/states/add" },
                    { 17, "Edit State", "/states/edit/:id" },
                    { 18, "List Packages", "/packages" },
                    { 19, "Add Package", "/packages/add" },
                    { 20, "Edit Package", "/packages/edit/:id" },
                    { 21, "Add Package Details", "/packages/details/add/:id" },
                    { 22, "List Schedules", "/schedules" },
                    { 23, "Add Schedule", "/schedules/add" },
                    { 24, "Edit Schedule", "/schedules/edit/:id" },
                    { 25, "List Tour Vouchers", "/tour-vouchers" },
                    { 26, "Add Tour Voucher", "/tour-vouchers/add" },
                    { 27, "Edit Tour Voucher", "/tour-vouchers/edit/:id" },
                    { 28, "List Students", "/students" },
                    { 29, "Add Student", "/students/add" },
                    { 30, "Edit Student", "/students/edit/:id" },
                    { 31, "More Example Path", "/more/path/example" }
                });

            migrationBuilder.InsertData(
                table: "RequestUrls",
                columns: new[] { "RequestUrlId", "Url", "UrlName" },
                values: new object[,]
                {
                    { 1, "/api", "API Base URL" },
                    { 2, "/dashboard", "Dashboard" },
                    { 3, "/users", "List Users" },
                    { 4, "/users/add", "Add User" },
                    { 5, "/users/edit/:id", "Edit User" },
                    { 6, "/categories", "List Categories" },
                    { 7, "/categories/add", "Add Category" },
                    { 8, "/categories/edit/:id", "Edit Category" },
                    { 9, "/sub-categories", "List Sub Categories" },
                    { 10, "/sub-categories/add", "Add Sub Category" },
                    { 11, "/sub-categories/edit/:id", "Edit Sub Category" },
                    { 12, "/countries", "List Countries" },
                    { 13, "/countries/add", "Add Country" },
                    { 14, "/countries/edit/:id", "Edit Country" },
                    { 15, "/states", "List States" },
                    { 16, "/states/add", "Add State" },
                    { 17, "/states/edit/:id", "Edit State" },
                    { 18, "/packages", "List Packages" },
                    { 19, "/packages/add", "Add Package" },
                    { 20, "/packages/edit/:id", "Edit Package" },
                    { 21, "/packages/details/add/:id", "Add Package Details" },
                    { 22, "/schedules", "List Schedules" },
                    { 23, "/schedules/add", "Add Schedule" },
                    { 24, "/schedules/edit/:id", "Edit Schedule" },
                    { 25, "/tour-vouchers", "List Tour Vouchers" },
                    { 26, "/tour-vouchers/add", "Add Tour Voucher" },
                    { 27, "/tour-vouchers/edit/:id", "Edit Tour Voucher" },
                    { 28, "/students", "List Students" },
                    { 29, "/students/add", "Add Student" },
                    { 30, "/students/edit/:id", "Edit Student" },
                    { 31, "/more/path/example", "More Example Path" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApplicationUserID",
                table: "Bookings",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PackageID",
                table: "Bookings",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_DayWiseTourCosts_DayCostCategoryID",
                table: "DayWiseTourCosts",
                column: "DayCostCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_DayWiseTourCosts_PackageID",
                table: "DayWiseTourCosts",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_ApplicationUserID",
                table: "Guides",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_FacilityID",
                table: "HotelFacilities",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_HotelID",
                table: "HotelFacilities",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelID",
                table: "HotelImages",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationID",
                table: "Hotels",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationGalleries_LocationID",
                table: "LocationGalleries",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StateID",
                table: "Locations",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageAccommodations_PackageID",
                table: "PackageAccommodations",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageAccommodations_RoomID",
                table: "PackageAccommodations",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageAccounts_PackageID",
                table: "PackageAccounts",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageBudgets_PackageID",
                table: "PackageBudgets",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageDetails_PackageID",
                table: "PackageDetails",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageExcludes_PackageID",
                table: "PackageExcludes",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFacilities_FacilityID",
                table: "PackageFacilities",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFacilities_PackageID",
                table: "PackageFacilities",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFAQ_PackageID",
                table: "PackageFAQ",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFoodItems_FoodItemID",
                table: "PackageFoodItems",
                column: "FoodItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFoodItems_MealTypeID",
                table: "PackageFoodItems",
                column: "MealTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFoodItems_PackageID",
                table: "PackageFoodItems",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageGallery_PackageID",
                table: "PackageGallery",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageIncludes_PackageID",
                table: "PackageIncludes",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageLocation_LocationID",
                table: "PackageLocation",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageLocation_PackageID",
                table: "PackageLocation",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackageCategoryID",
                table: "Packages",
                column: "PackageCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackageSubCategoryID",
                table: "Packages",
                column: "PackageSubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageSubCategories_PackageCategoryID",
                table: "PackageSubCategories",
                column: "PackageCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_PackageID",
                table: "PackageTransportations",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_TransportationCatagoryID",
                table: "PackageTransportations",
                column: "TransportationCatagoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_TransportationID",
                table: "PackageTransportations",
                column: "TransportationID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransportations_TransportationTypeID",
                table: "PackageTransportations",
                column: "TransportationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUsers_ApplicationUserID",
                table: "PackageUsers",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUsers_GuideID",
                table: "PackageUsers",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_PackageUsers_PackageID",
                table: "PackageUsers",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingID",
                table: "Payments",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodID",
                table: "Payments",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusID",
                table: "Payments",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PromotionID",
                table: "Payments",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_PromotionImageID",
                table: "Promotions",
                column: "PromotionImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApplicationUserID",
                table: "Reviews",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PackageID",
                table: "Reviews",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelID",
                table: "Rooms",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomSubTypeID",
                table: "Rooms",
                column: "RoomSubTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DayCostCategoryID",
                table: "Schedule",
                column: "DayCostCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_PackageID",
                table: "Schedule",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TourVoucherID",
                table: "Schedule",
                column: "TourVoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_PackageTransportationID",
                table: "Seats",
                column: "PackageTransportationID");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryID",
                table: "States",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TourVouchers_PackageID",
                table: "TourVouchers",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Transportation_TransportationTypeID",
                table: "Transportation",
                column: "TransportationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transportation_TransportProviderID",
                table: "Transportation",
                column: "TransportProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_UrlServices_CurrentUrlId",
                table: "UrlServices",
                column: "CurrentUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlServices_RequestUrlId",
                table: "UrlServices",
                column: "RequestUrlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BaseCosts");

            migrationBuilder.DropTable(
                name: "DayWiseTourCosts");

            migrationBuilder.DropTable(
                name: "HotelFacilities");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "LocationGalleries");

            migrationBuilder.DropTable(
                name: "PackageAccommodations");

            migrationBuilder.DropTable(
                name: "PackageAccounts");

            migrationBuilder.DropTable(
                name: "PackageBudgets");

            migrationBuilder.DropTable(
                name: "PackageDetails");

            migrationBuilder.DropTable(
                name: "PackageExcludes");

            migrationBuilder.DropTable(
                name: "PackageFacilities");

            migrationBuilder.DropTable(
                name: "PackageFAQ");

            migrationBuilder.DropTable(
                name: "PackageFoodItems");

            migrationBuilder.DropTable(
                name: "PackageGallery");

            migrationBuilder.DropTable(
                name: "PackageIncludes");

            migrationBuilder.DropTable(
                name: "PackageLocation");

            migrationBuilder.DropTable(
                name: "PackageUsers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "UrlServices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "DayCostCategory");

            migrationBuilder.DropTable(
                name: "TourVouchers");

            migrationBuilder.DropTable(
                name: "PackageTransportations");

            migrationBuilder.DropTable(
                name: "CurrentUrls");

            migrationBuilder.DropTable(
                name: "RequestUrls");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomSubTypes");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PromotionImages");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "TransportationCatagories");

            migrationBuilder.DropTable(
                name: "Transportation");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "PackageSubCategories");

            migrationBuilder.DropTable(
                name: "TransportProviders");

            migrationBuilder.DropTable(
                name: "TransportationTypes");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "PackageCategories");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
