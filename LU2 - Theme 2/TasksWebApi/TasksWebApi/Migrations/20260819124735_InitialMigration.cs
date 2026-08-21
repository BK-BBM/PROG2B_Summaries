using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }


        /* As mentioned, once we run a migration using 
         * 
         * dotnet ef migrations add "MigrationName"
         * 
         * Entity Framework will put this script together.
         * 
         * The void Up() method is what will get pushed to SQL server
         * meaning,that's the code that ends up creating our table.
         * 
         * The void Down() method is when we rollback the migration we just did
         * and it will delete our table (DropTable)
         
         */
    }
}
