using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaceHeap.Common.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    Popularity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                },
                constraints: table =>
                {
                    table
                        .PrimaryKey("PK_Developers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table
                        .UniqueConstraint("AK_Developers_Name", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Developers_Name",
                table: "Developers",
                column: "Name"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Developers");
        }
    }
}
