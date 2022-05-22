using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205141442_CriarEvento")]
    public class CriarEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.CreateTable(
                  schema: "sgi",
                  name: "EVENTO",
                  columns: table => new
                  {
                      ID = table.Column<Guid>(),
                      USUARIO_ID = table.Column<Guid>(),
                      NOME = table.Column<string>(nullable: false, maxLength: 200),
                      TIPO_EVENTO = table.Column<string>(nullable: false, maxLength: 80),
                      MODALIDADE = table.Column<string>(nullable: false, maxLength: 20),
                      DESCRICAO = table.Column<string>(nullable: false),                      
                      LINK_STREAM = table.Column<string>(nullable: true, maxLength: 500),
                      LINK_REDIRECIONAMENTO = table.Column<string>(nullable: true, maxLength: 500),
                      PRECO = table.Column<decimal>(nullable: false),
                      PAIS = table.Column<string>(nullable: false, maxLength: 100),
                      ESTADO = table.Column<string>(nullable: false, maxLength: 100),
                      CIDADE = table.Column<string>(nullable: false, maxLength: 100),
                      BAIRRO = table.Column<string>(nullable: false, maxLength: 100),
                      ENDERECO_COMPLETO = table.Column<string>(nullable: false, maxLength: 250),
                      COMPLEMENTO = table.Column<string>(nullable: true, maxLength: 250)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_EVENTO_ID", k => k.ID);
                      table.ForeignKey(
                        name: "FK_EVENTO_USUARIO",
                        column: ev => ev.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        principalSchema: "sgi",
                        onDelete: ReferentialAction.Cascade);
                  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.DropTable(name: "EVENTO", schema: "sgi");
        }
    }
}
