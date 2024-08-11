using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProductManagement.BL.Models.Product;
using ProductManagement.BL.Models.User;
using ProductManagement.Common.Helpers;
using ProductManagement.DAL.Helpers;

#nullable disable

namespace ProductManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAudits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LogMessage = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAudits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
            
            var userAdmin = new UserAddRequest
            {
                Username = "admin",
                Password = "admin",
                Role = UserRole.Admin,
                Name = "Admin",
                Phone = "+998901234567"
            };
            
            var user = new UserAddRequest
            {
                Username = "user",
                Password = "user",
                Role = UserRole.User,
                Name = "User",
                Phone = "+998907654321"
            };
            Cryptography.CreatePasswordHash(userAdmin.Password, out var passwordHashAdmin, out var passwordSaltAdmin);
            Cryptography.CreatePasswordHash(userAdmin.Password, out var passwordHash, out var passwordSalt);
            migrationBuilder.InsertData("Users",
                new[] { "Username", "PasswordHash", "PasswordSalt", "Role", "Name", "Phone", "CreatedAt" },
                new object[] { userAdmin.Username, passwordHashAdmin, passwordSaltAdmin, userAdmin.Role.ToString(), userAdmin.Name, userAdmin.Phone, DateTime.UtcNow });
            migrationBuilder.InsertData("Users",
                new[] { "Username", "PasswordHash", "PasswordSalt", "Role", "Name", "Phone", "CreatedAt" },
                new object[] { user.Username, passwordHash, passwordSalt, user.Role.ToString(), user.Name, user.Phone, DateTime.UtcNow });
            
            var product = new ProductAddRequest
            {
                Title = "Product",
                Quantity = 10,
                Price = 1000,
            };
            migrationBuilder.InsertData("Products",
                new[] { "Title", "Quantity", "Price", "CreatedAt" },
                new object[] { product.Title, product.Quantity, product.Price, DateTime.UtcNow });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAudits");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
