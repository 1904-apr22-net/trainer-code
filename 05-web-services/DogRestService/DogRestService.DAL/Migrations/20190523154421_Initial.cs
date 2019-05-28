using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogRestService.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Breed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dog",
                columns: new[] { "Id", "Breed", "Name" },
                values: new object[] { 1, "Beagle", "Fred" });
        }

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
                name: "Dog");
    }
}
