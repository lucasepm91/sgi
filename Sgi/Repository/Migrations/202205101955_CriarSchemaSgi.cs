using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Sgi.Repository.Contexts;

namespace Sgi.Repository.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("202205101955_CriarSchemaSgi")]
    public class CriarSchemaSgi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) =>
           migrationBuilder?.EnsureSchema("sgi");

        protected override void Down(MigrationBuilder migrationBuilder) =>
            migrationBuilder?.DropSchema("sgi");
    }
}
