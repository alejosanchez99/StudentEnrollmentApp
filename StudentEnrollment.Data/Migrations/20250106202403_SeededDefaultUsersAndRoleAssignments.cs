using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndRoleAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bd96e52a-dddb-4bd4-b287-3032d0a82c8a", "603af229-8d4b-43d0-893f-e89ceccfa0d9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "28e2ad41-8b4a-4db1-a728-9712d6d1f6f1", "b2803cf5-13f2-4c4d-8aab-c9e7bfbf2cca" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28e2ad41-8b4a-4db1-a728-9712d6d1f6f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd96e52a-dddb-4bd4-b287-3032d0a82c8a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "603af229-8d4b-43d0-893f-e89ceccfa0d9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2803cf5-13f2-4c4d-8aab-c9e7bfbf2cca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42358d3e-3c22-45e1-be81-6caa7ba865ef", null, "User", "USER" },
                    { "d1b5952a-2162-46c7-b29e-1a2a68922c14", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, "00df0590-892d-483b-8ff6-18e6c02a9121", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEFTvjjDRpRky4jRo57FBFOjY/ajUv11VaDgTKFUIx7n6u3wNhll+Jfl11a+qsqUS6g==", null, false, "4ee9c5bf-7423-4a10-b337-440a6c697343", false, "schooluser@localhost.com" },
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, "fc02befe-5a18-4206-ba19-9bc374b08cde", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEHAA+65RCdtXPecMYsq9BgNHYZFwICE6zpUqPY0NgCCAt3Bqlaryan1GlcKDT89doQ==", null, false, "0fe57bf3-5bc3-43c1-8534-d37bc43c3753", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "3f4631bd-f907-4409-b416-ba356312e659" },
                    { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "408aa945-3d84-4421-8342-7269ec64d949" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42358d3e-3c22-45e1-be81-6caa7ba865ef", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d1b5952a-2162-46c7-b29e-1a2a68922c14", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42358d3e-3c22-45e1-be81-6caa7ba865ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b5952a-2162-46c7-b29e-1a2a68922c14");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28e2ad41-8b4a-4db1-a728-9712d6d1f6f1", null, "Administrator", "ADMINISTRATOR" },
                    { "bd96e52a-dddb-4bd4-b287-3032d0a82c8a", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "603af229-8d4b-43d0-893f-e89ceccfa0d9", 0, "1430282b-e269-42a1-82f2-fc6292c48321", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCAHOST.COM", "schooluser@localhost.com", "AQAAAAIAAYagAAAAEJY+2uYHY0bQEq9ihIC5FGC+Zrwei4N8dK2BsWwLk4kB9+WJyrSyd/D3NuGwqBVzzw==", null, false, "f1ae516c-330d-4649-b5cc-200faf3042ef", false, "schooluser@localhost.com" },
                    { "b2803cf5-13f2-4c4d-8aab-c9e7bfbf2cca", 0, "dd65e01d-fa8b-4809-b5a5-db7f3564df9d", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCAHOST.COM", "schooladmin@localhost.com", "AQAAAAIAAYagAAAAEPbT5GjLLXawMawddyH3C85wb8yUx2JTrqQ8B5Fjmh+VkgoN9qxwS4mzVjcVkeDobA==", null, false, "326c170d-90b2-4db9-a120-6675c5acc295", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "bd96e52a-dddb-4bd4-b287-3032d0a82c8a", "603af229-8d4b-43d0-893f-e89ceccfa0d9" },
                    { "28e2ad41-8b4a-4db1-a728-9712d6d1f6f1", "b2803cf5-13f2-4c4d-8aab-c9e7bfbf2cca" }
                });
        }
    }
}
