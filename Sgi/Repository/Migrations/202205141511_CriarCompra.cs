using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205141511_CriarCompra")]
    public class CriarCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.CreateTable(
                  schema: "sgi",
                  name: "COMPRA",
                  columns: table => new
                  {
                      ID = table.Column<Guid>(),
                      USUARIO_ID = table.Column<Guid>(),
                      DATA = table.Column<DateTime>(nullable: false),
                      FORMA_PAGAMENTO = table.Column<string>(nullable: false, maxLength: 50),
                      TOTAL = table.Column<decimal>(nullable: false)                      
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_COMPRA_ID", k => k.ID);
                      table.ForeignKey(
                        name: "FK_COMPRA_USUARIO",
                        column: ev => ev.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.Cascade);
                  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.DropTable(name: "COMPRA", schema: "sgi");
        }
    }
}
