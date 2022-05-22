using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205141521_CriarIngresso")]
    public class CriarIngresso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.CreateTable(
                  schema: "sgi",
                  name: "INGRESSO",
                  columns: table => new
                  {
                      ID = table.Column<Guid>(),
                      EVENTO_ID = table.Column<Guid>(),
                      SESSAO_ID = table.Column<Guid>(),
                      COMPRA_ID = table.Column<Guid>(),
                      CODIGO = table.Column<string>(nullable: false, maxLength: 200),
                      PRECO = table.Column<decimal>(nullable: false)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_INGRESSO_ID", k => k.ID);
                      table.ForeignKey(
                        name: "FK_INGRESSO_EVENTO",
                        column: i => i.EVENTO_ID,
                        principalTable: "EVENTO",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.NoAction);
                      table.ForeignKey(
                        name: "FK_INGRESSO_SESSAO",
                        column: ev => ev.SESSAO_ID,
                        principalTable: "SESSAO",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.NoAction);
                      table.ForeignKey(
                        name: "FK_INGRESSO_COMPRA",
                        column: ev => ev.COMPRA_ID,
                        principalTable: "COMPRA",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.NoAction);
                  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.DropTable(name: "INGRESSO", schema: "sgi");
        }
    }
}
