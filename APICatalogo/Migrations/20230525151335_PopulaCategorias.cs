using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias(Nome, ImagemUrl) Values ('Bebidas', 'Bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome, ImagemUrl) Values ('Lanches', 'Lanches.jpg')");
            mb.Sql("INSERT INTO Categorias(Nome, ImagemUrl) Values ('Sobremesas', 'Sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
