using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205102001_CriarUsuario")]
    public class CriarUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.CreateTable(
                  schema: "sgi",
                  name: "USUARIO",
                  columns: table => new
                  {
                      ID = table.Column<Guid>(),
                      NOME = table.Column<string>(maxLength: 120),
                      CPF_CNPJ = table.Column<string>(nullable: false, maxLength: 14),
                      EMAIL = table.Column<string>(nullable: false, maxLength: 100),
                      SENHA = table.Column<string>(nullable: false, maxLength: 300),
                      SALDO_CARTEIRA = table.Column<decimal>(nullable: false),
                      NOME_FANTASIA = table.Column<string>(nullable: true, maxLength: 200),
                      NOME_RESPONSAVEL = table.Column<string>(nullable: true, maxLength: 200),
                      TIPO_USUARIO = table.Column<string>(nullable: false, maxLength: 20),
                      PAIS = table.Column<string>(nullable: false, maxLength: 100),                      
                      ESTADO = table.Column<string>(nullable: false, maxLength: 100),
                      CIDADE = table.Column<string>(nullable: false, maxLength: 100),
                      BAIRRO = table.Column<string>(nullable: false, maxLength: 100),
                      ENDERECO_COMPLETO = table.Column<string>(nullable: false, maxLength: 200),
                      COMPLEMENTO = table.Column<string>(nullable: true, maxLength: 150),
                      CODIGO_BANCO = table.Column<string>(nullable: true, maxLength: 10),
                      CODIGO_AGENCIA = table.Column<string>(nullable: true, maxLength: 10),
                      CODIGO_CONTA = table.Column<string>(nullable: true, maxLength: 10)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_USUARIO_ID", k => k.ID);
                  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder?.DropTable(name: "USUARIO", schema: "sgi");
        }
    }
}
