using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiTiendaWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CervezaUrlImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cerveza",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alcohol = table.Column<double>(type: "float", nullable: false),
                    idEstilo = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<double>(type: "float", nullable: false),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cerveza", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cerveza_Estilo_idEstilo",
                        column: x => x.idEstilo,
                        principalTable: "Estilo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cerveza_idEstilo",
                table: "Cerveza",
                column: "idEstilo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cerveza");
        }
    }
}
