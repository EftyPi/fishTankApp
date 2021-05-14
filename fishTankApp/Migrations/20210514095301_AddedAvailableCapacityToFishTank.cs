using Microsoft.EntityFrameworkCore.Migrations;

namespace fishTankApp.Migrations
{
    public partial class AddedAvailableCapacityToFishTank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCapacity",
                table: "FishTank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FishTankId",
                table: "Fish",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Decoration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FishTankId",
                table: "Decoration",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fish_FishTankId",
                table: "Fish",
                column: "FishTankId");

            migrationBuilder.CreateIndex(
                name: "IX_Decoration_FishTankId",
                table: "Decoration",
                column: "FishTankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decoration_FishTank_FishTankId",
                table: "Decoration",
                column: "FishTankId",
                principalTable: "FishTank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_FishTank_FishTankId",
                table: "Fish",
                column: "FishTankId",
                principalTable: "FishTank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decoration_FishTank_FishTankId",
                table: "Decoration");

            migrationBuilder.DropForeignKey(
                name: "FK_Fish_FishTank_FishTankId",
                table: "Fish");

            migrationBuilder.DropIndex(
                name: "IX_Fish_FishTankId",
                table: "Fish");

            migrationBuilder.DropIndex(
                name: "IX_Decoration_FishTankId",
                table: "Decoration");

            migrationBuilder.DropColumn(
                name: "AvailableCapacity",
                table: "FishTank");

            migrationBuilder.DropColumn(
                name: "FishTankId",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Decoration");

            migrationBuilder.DropColumn(
                name: "FishTankId",
                table: "Decoration");

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });
        }
    }
}
