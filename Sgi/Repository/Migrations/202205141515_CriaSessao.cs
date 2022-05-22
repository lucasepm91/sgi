using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205141515_CriaSessao")]
    public class CriaSessao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.CreateTable(
                  schema: "sgi",
                  name: "SESSAO",
                  columns: table => new
                  {
                      ID = table.Column<Guid>(),
                      EVENTO_ID = table.Column<Guid>(),
                      DATA = table.Column<DateTime>(nullable: false),
                      LOTACAO = table.Column<int>(nullable: false),
                      INGRESSOS_VENDIDOS = table.Column<int>(nullable: false),
                      CODIGO_LOCAL = table.Column<string>(nullable: false, maxLength: 100),
                      ESQUEMA = table.Column<string>(nullable: false),
                      RESERVADOS = table.Column<string>(nullable: false),
                      LIVRES = table.Column<string>(nullable: false)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_SESSAO_ID", k => k.ID);
                      table.ForeignKey(
                        name: "FK_SESSAO_EVENTO",
                        column: i => i.EVENTO_ID,
                        principalTable: "EVENTO",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.Cascade);                      
                  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.DropTable(name: "SESSAO", schema: "sgi");
        }
    }
}
