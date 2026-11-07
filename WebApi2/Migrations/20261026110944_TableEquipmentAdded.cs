using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopWebApi.Migrations
{
    /// <inheritdoc />
    public partial class TableEquipmentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEquipment",
                table: "Equipment");

            migrationBuilder.RenameColumn(
                name: "NumberEquipment",
                table: "Equipment",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Workshop",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_EquipmentId",
                table: "Workshop",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshop_Equipment_EquipmentId",
                table: "Workshop",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshop_Equipment_EquipmentId",
                table: "Workshop");

            migrationBuilder.DropIndex(
                name: "IX_Workshop_EquipmentId",
                table: "Workshop");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Workshop");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Equipment",
                newName: "NumberEquipment");

            migrationBuilder.AddColumn<string>(
                name: "NameEquipment",
                table: "Equipment",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
