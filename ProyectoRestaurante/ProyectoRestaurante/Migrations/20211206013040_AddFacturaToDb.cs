using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoRestaurante.Migrations
{
    public partial class AddFacturaToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Facturas",
                newName: "IdFactura");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdFactura",
                table: "Facturas",
                newName: "Id");
        }
    }
}
