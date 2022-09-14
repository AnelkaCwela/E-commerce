using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OurShop.Migrations
{
    public partial class Ace : Migration
    {
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
                name: "BrandDeatailTbl",
                columns: table => new
                {
                    BrandDeatailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BussneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentityDocument = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    BussnessRegistration = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandDeatailTbl", x => x.BrandDeatailId);
                });

            migrationBuilder.CreateTable(
                name: "BrandModelTbl",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BrandSlogn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrandLogo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModelTbl", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "CityModelTbl",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityModelTbl", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "ColorModelTbl",
                columns: table => new
                {
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorModelTbl", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerModelTbl",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerUserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModelTbl", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStatuseTbl",
                columns: table => new
                {
                    DeliveryStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatuseDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStatuseTbl", x => x.DeliveryStatuseId);
                });

            migrationBuilder.CreateTable(
                name: "GanderModelTbl",
                columns: table => new
                {
                    GanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gander = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GanderModelTbl", x => x.GanderId);
                });

            migrationBuilder.CreateTable(
                name: "IteamStatuseTbl",
                columns: table => new
                {
                    IteamStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IteamStatuse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IteamStatuseTbl", x => x.IteamStatuseId);
                });

            migrationBuilder.CreateTable(
                name: "OderTypeTbl",
                columns: table => new
                {
                    OderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OderType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OderTypeTbl", x => x.OderTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeModelTbl",
                columns: table => new
                {
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeModelTbl", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "QoutationBySuplierTbl",
                columns: table => new
                {
                    QoutationStatuseSuplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QoutationStatuseSuplier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QoutationBySuplierTbl", x => x.QoutationStatuseSuplierId);
                });

            migrationBuilder.CreateTable(
                name: "QoutationStatuseTbl",
                columns: table => new
                {
                    QoutationStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QoutationStatuse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QoutationStatuseTbl", x => x.QoutationStatuseId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategoryTbl",
                columns: table => new
                {
                    ServiceCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategoryTbl", x => x.ServiceCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReserveStatuseTbl",
                columns: table => new
                {
                    ServiceReserveStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReserveStatuse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReserveStatuseTbl", x => x.ServiceReserveStatuseId);
                });

            migrationBuilder.CreateTable(
                name: "SeviceStauseTbl",
                columns: table => new
                {
                    SeviceStauseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeviceStause = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeviceStauseTbl", x => x.SeviceStauseId);
                });

            migrationBuilder.CreateTable(
                name: "SizeModelTbl",
                columns: table => new
                {
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeModelTbl", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "StatusTbl",
                columns: table => new
                {
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Statuse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTbl", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierStatuseTbl",
                columns: table => new
                {
                    SupplierStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierStatuse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierStatuseTbl", x => x.SupplierStatuseId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierTypeTbl",
                columns: table => new
                {
                    SupplierTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierTypeTbl", x => x.SupplierTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TypeModelTbl",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeModelTbl", x => x.TypeId);
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
                name: "ContactDetailTbl",
                columns: table => new
                {
                    ContactDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailTbl", x => x.ContactDetailId);
                    table.ForeignKey(
                        name: "FK_ContactDetailTbl_BrandModelTbl_BrandId",
                        column: x => x.BrandId,
                        principalTable: "BrandModelTbl",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SububTbl",
                columns: table => new
                {
                    SubrbId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SububTbl", x => x.SubrbId);
                    table.ForeignKey(
                        name: "FK_SububTbl_CityModelTbl_CityId",
                        column: x => x.CityId,
                        principalTable: "CityModelTbl",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeTbl",
                columns: table => new
                {
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeTbl", x => x.ServiceTypeId);
                    table.ForeignKey(
                        name: "FK_ServiceTypeTbl_ServiceCategoryTbl_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategoryTbl",
                        principalColumn: "ServiceCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierModelTbl",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistartionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandDeatailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierModelTbl", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_SupplierModelTbl_BrandDeatailTbl_BrandDeatailId",
                        column: x => x.BrandDeatailId,
                        principalTable: "BrandDeatailTbl",
                        principalColumn: "BrandDeatailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierModelTbl_BrandModelTbl_BrandId",
                        column: x => x.BrandId,
                        principalTable: "BrandModelTbl",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierModelTbl_SupplierStatuseTbl_SupplierStatuseId",
                        column: x => x.SupplierStatuseId,
                        principalTable: "SupplierStatuseTbl",
                        principalColumn: "SupplierStatuseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierModelTbl_SupplierTypeTbl_SupplierTypeId",
                        column: x => x.SupplierTypeId,
                        principalTable: "SupplierTypeTbl",
                        principalColumn: "SupplierTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryModelTbl",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModelTbl", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_CategoryModelTbl_TypeModelTbl_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypeModelTbl",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationModel",
                columns: table => new
                {
                    QoutationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QoutationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefranceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQoutationPrice = table.Column<double>(type: "float", nullable: false),
                    Refrance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubrbId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QoutationStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QoutationStatuseSuplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationModel", x => x.QoutationId);
                    table.ForeignKey(
                        name: "FK_QuotationModel_CustomerModelTbl_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerModelTbl",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationModel_OderTypeTbl_OderTypeId",
                        column: x => x.OderTypeId,
                        principalTable: "OderTypeTbl",
                        principalColumn: "OderTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationModel_QoutationBySuplierTbl_QoutationStatuseSuplierId",
                        column: x => x.QoutationStatuseSuplierId,
                        principalTable: "QoutationBySuplierTbl",
                        principalColumn: "QoutationStatuseSuplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationModel_QoutationStatuseTbl_QoutationStatuseId",
                        column: x => x.QoutationStatuseId,
                        principalTable: "QoutationStatuseTbl",
                        principalColumn: "QoutationStatuseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationModel_SububTbl_SubrbId",
                        column: x => x.SubrbId,
                        principalTable: "SububTbl",
                        principalColumn: "SubrbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationModelTbl",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Long = table.Column<float>(type: "real", nullable: false),
                    Zoom = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModelTbl", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_LocationModelTbl_SupplierModelTbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModelTbl",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingTbl",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTbl", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_RatingTbl_CustomerModelTbl_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerModelTbl",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingTbl_SupplierModelTbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModelTbl",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentModelTbl",
                columns: table => new
                {
                    ResidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentModelTbl", x => x.ResidentId);
                    table.ForeignKey(
                        name: "FK_ResidentModelTbl_CityModelTbl_CityId",
                        column: x => x.CityId,
                        principalTable: "CityModelTbl",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidentModelTbl_SupplierModelTbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModelTbl",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTbl",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    desctription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeviceStauseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTbl", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_ServiceTbl_ServiceTypeTbl_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypeTbl",
                        principalColumn: "ServiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTbl_SeviceStauseTbl_SeviceStauseId",
                        column: x => x.SeviceStauseId,
                        principalTable: "SeviceStauseTbl",
                        principalColumn: "SeviceStauseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTbl_SupplierModelTbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModelTbl",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IteamModelTbl",
                columns: table => new
                {
                    IteamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItermName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItermDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageIteam = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IteamStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IteamModelTbl", x => x.IteamId);
                    table.ForeignKey(
                        name: "FK_IteamModelTbl_CategoryModelTbl_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryModelTbl",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamModelTbl_GanderModelTbl_GanderId",
                        column: x => x.GanderId,
                        principalTable: "GanderModelTbl",
                        principalColumn: "GanderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamModelTbl_IteamStatuseTbl_IteamStatuseId",
                        column: x => x.IteamStatuseId,
                        principalTable: "IteamStatuseTbl",
                        principalColumn: "IteamStatuseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamModelTbl_SupplierModelTbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "SupplierModelTbl",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryModelTbl",
                columns: table => new
                {
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    deliverydate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QoutationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryModelTbl", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_DeliveryModelTbl_DeliveryStatuseTbl_DeliveryStatuseId",
                        column: x => x.DeliveryStatuseId,
                        principalTable: "DeliveryStatuseTbl",
                        principalColumn: "DeliveryStatuseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryModelTbl_QuotationModel_QoutationId",
                        column: x => x.QoutationId,
                        principalTable: "QuotationModel",
                        principalColumn: "QoutationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveTbl",
                columns: table => new
                {
                    ReserveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReserveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceReserveStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReserveStatuseModelServiceReserveStatuseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveTbl", x => x.ReserveId);
                    table.ForeignKey(
                        name: "FK_ReserveTbl_CustomerModelTbl_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerModelTbl",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReserveTbl_ServiceReserveStatuseTbl_ServiceReserveStatuseModelServiceReserveStatuseId",
                        column: x => x.ServiceReserveStatuseModelServiceReserveStatuseId,
                        principalTable: "ServiceReserveStatuseTbl",
                        principalColumn: "ServiceReserveStatuseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReserveTbl_ServiceTbl_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceTbl",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageModelTbl",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IteamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModelTbl", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ImageModelTbl_IteamModelTbl_IteamId",
                        column: x => x.IteamId,
                        principalTable: "IteamModelTbl",
                        principalColumn: "IteamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IteamDetailTbl",
                columns: table => new
                {
                    IteamDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stock = table.Column<bool>(type: "bit", nullable: false),
                    NoOfIteam = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IteamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IteamDetailTbl", x => x.IteamDetailId);
                    table.ForeignKey(
                        name: "FK_IteamDetailTbl_ColorModelTbl_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ColorModelTbl",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamDetailTbl_IteamModelTbl_IteamId",
                        column: x => x.IteamId,
                        principalTable: "IteamModelTbl",
                        principalColumn: "IteamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IteamDetailTbl_SizeModelTbl_SizeId",
                        column: x => x.SizeId,
                        principalTable: "SizeModelTbl",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeModelTbl",
                columns: table => new
                {
                    LikeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IteamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeModelTbl", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_LikeModelTbl_CustomerModelTbl_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerModelTbl",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeModelTbl_IteamModelTbl_IteamId",
                        column: x => x.IteamId,
                        principalTable: "IteamModelTbl",
                        principalColumn: "IteamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationDetailModelTbl",
                columns: table => new
                {
                    QoutationDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantty = table.Column<int>(type: "int", nullable: false),
                    QoutationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IteamDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationDetailModelTbl", x => x.QoutationDetailId);
                    table.ForeignKey(
                        name: "FK_QuotationDetailModelTbl_IteamDetailTbl_IteamDetailId",
                        column: x => x.IteamDetailId,
                        principalTable: "IteamDetailTbl",
                        principalColumn: "IteamDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationDetailModelTbl_QuotationModel_QoutationId",
                        column: x => x.QoutationId,
                        principalTable: "QuotationModel",
                        principalColumn: "QoutationId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CategoryModelTbl_TypeId",
                table: "CategoryModelTbl",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailTbl_BrandId",
                table: "ContactDetailTbl",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryModelTbl_DeliveryStatuseId",
                table: "DeliveryModelTbl",
                column: "DeliveryStatuseId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryModelTbl_QoutationId",
                table: "DeliveryModelTbl",
                column: "QoutationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModelTbl_IteamId",
                table: "ImageModelTbl",
                column: "IteamId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamDetailTbl_ColorId",
                table: "IteamDetailTbl",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamDetailTbl_IteamId",
                table: "IteamDetailTbl",
                column: "IteamId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamDetailTbl_SizeId",
                table: "IteamDetailTbl",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamModelTbl_CategoryId",
                table: "IteamModelTbl",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamModelTbl_GanderId",
                table: "IteamModelTbl",
                column: "GanderId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamModelTbl_IteamStatuseId",
                table: "IteamModelTbl",
                column: "IteamStatuseId");

            migrationBuilder.CreateIndex(
                name: "IX_IteamModelTbl_SupplierId",
                table: "IteamModelTbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeModelTbl_CustomerId",
                table: "LikeModelTbl",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeModelTbl_IteamId",
                table: "LikeModelTbl",
                column: "IteamId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationModelTbl_SupplierId",
                table: "LocationModelTbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetailModelTbl_IteamDetailId",
                table: "QuotationDetailModelTbl",
                column: "IteamDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetailModelTbl_QoutationId",
                table: "QuotationDetailModelTbl",
                column: "QoutationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationModel_CustomerId",
                table: "QuotationModel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationModel_OderTypeId",
                table: "QuotationModel",
                column: "OderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationModel_QoutationStatuseId",
                table: "QuotationModel",
                column: "QoutationStatuseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationModel_QoutationStatuseSuplierId",
                table: "QuotationModel",
                column: "QoutationStatuseSuplierId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationModel_SubrbId",
                table: "QuotationModel",
                column: "SubrbId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTbl_CustomerId",
                table: "RatingTbl",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTbl_SupplierId",
                table: "RatingTbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveTbl_CustomerId",
                table: "ReserveTbl",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveTbl_ServiceId",
                table: "ReserveTbl",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveTbl_ServiceReserveStatuseModelServiceReserveStatuseId",
                table: "ReserveTbl",
                column: "ServiceReserveStatuseModelServiceReserveStatuseId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentModelTbl_CityId",
                table: "ResidentModelTbl",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentModelTbl_SupplierId",
                table: "ResidentModelTbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTbl_ServiceTypeId",
                table: "ServiceTbl",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTbl_SeviceStauseId",
                table: "ServiceTbl",
                column: "SeviceStauseId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTbl_SupplierId",
                table: "ServiceTbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeTbl_ServiceCategoryId",
                table: "ServiceTypeTbl",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SububTbl_CityId",
                table: "SububTbl",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierModelTbl_BrandDeatailId",
                table: "SupplierModelTbl",
                column: "BrandDeatailId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierModelTbl_BrandId",
                table: "SupplierModelTbl",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierModelTbl_SupplierStatuseId",
                table: "SupplierModelTbl",
                column: "SupplierStatuseId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierModelTbl_SupplierTypeId",
                table: "SupplierModelTbl",
                column: "SupplierTypeId");
        }

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
                name: "ContactDetailTbl");

            migrationBuilder.DropTable(
                name: "DeliveryModelTbl");

            migrationBuilder.DropTable(
                name: "ImageModelTbl");

            migrationBuilder.DropTable(
                name: "LikeModelTbl");

            migrationBuilder.DropTable(
                name: "LocationModelTbl");

            migrationBuilder.DropTable(
                name: "PaymentTypeModelTbl");

            migrationBuilder.DropTable(
                name: "QuotationDetailModelTbl");

            migrationBuilder.DropTable(
                name: "RatingTbl");

            migrationBuilder.DropTable(
                name: "ReserveTbl");

            migrationBuilder.DropTable(
                name: "ResidentModelTbl");

            migrationBuilder.DropTable(
                name: "StatusTbl");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DeliveryStatuseTbl");

            migrationBuilder.DropTable(
                name: "IteamDetailTbl");

            migrationBuilder.DropTable(
                name: "QuotationModel");

            migrationBuilder.DropTable(
                name: "ServiceReserveStatuseTbl");

            migrationBuilder.DropTable(
                name: "ServiceTbl");

            migrationBuilder.DropTable(
                name: "ColorModelTbl");

            migrationBuilder.DropTable(
                name: "IteamModelTbl");

            migrationBuilder.DropTable(
                name: "SizeModelTbl");

            migrationBuilder.DropTable(
                name: "CustomerModelTbl");

            migrationBuilder.DropTable(
                name: "OderTypeTbl");

            migrationBuilder.DropTable(
                name: "QoutationBySuplierTbl");

            migrationBuilder.DropTable(
                name: "QoutationStatuseTbl");

            migrationBuilder.DropTable(
                name: "SububTbl");

            migrationBuilder.DropTable(
                name: "ServiceTypeTbl");

            migrationBuilder.DropTable(
                name: "SeviceStauseTbl");

            migrationBuilder.DropTable(
                name: "CategoryModelTbl");

            migrationBuilder.DropTable(
                name: "GanderModelTbl");

            migrationBuilder.DropTable(
                name: "IteamStatuseTbl");

            migrationBuilder.DropTable(
                name: "SupplierModelTbl");

            migrationBuilder.DropTable(
                name: "CityModelTbl");

            migrationBuilder.DropTable(
                name: "ServiceCategoryTbl");

            migrationBuilder.DropTable(
                name: "TypeModelTbl");

            migrationBuilder.DropTable(
                name: "BrandDeatailTbl");

            migrationBuilder.DropTable(
                name: "BrandModelTbl");

            migrationBuilder.DropTable(
                name: "SupplierStatuseTbl");

            migrationBuilder.DropTable(
                name: "SupplierTypeTbl");
        }
    }
}
