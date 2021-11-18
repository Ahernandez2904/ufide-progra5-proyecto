using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoRestaurante.Migrations
{
    public partial class AddRestauranteToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CedulaCliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CedulaCliente);
                });

            migrationBuilder.CreateTable(
                name: "Platillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Costo = table.Column<float>(type: "real", nullable: false),
                    activo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadAsientos = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CedulaCliente = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Clientes_CedulaCliente",
                        column: x => x.CedulaCliente,
                        principalTable: "Clientes",
                        principalColumn: "CedulaCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    CedulaEmpleado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Apellido2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activo = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.CedulaEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReservacion = table.Column<int>(type: "int", nullable: false),
                    CedulaEmpleado = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Empleados_CedulaEmpleado",
                        column: x => x.CedulaEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "CedulaEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Reservaciones_IdReservacion",
                        column: x => x.IdReservacion,
                        principalTable: "Reservaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas_Platillo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    IdPlatillo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Detalle_Platillo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas_Platillo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Platillo_Facturas_IdFactura",
                        column: x => x.IdFactura,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Platillo_Platillos_IdPlatillo",
                        column: x => x.IdPlatillo,
                        principalTable: "Platillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdRol",
                table: "Empleados",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CedulaEmpleado",
                table: "Facturas",
                column: "CedulaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IdReservacion",
                table: "Facturas",
                column: "IdReservacion");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Platillo_IdFactura",
                table: "Facturas_Platillo",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_Platillo_IdPlatillo",
                table: "Facturas_Platillo",
                column: "IdPlatillo");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_CedulaCliente",
                table: "Reservaciones",
                column: "CedulaCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas_Platillo");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Platillos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
