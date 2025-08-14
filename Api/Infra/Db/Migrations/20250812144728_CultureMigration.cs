using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minimal_api.Migrations
{
    /// <inheritdoc />
    public partial class CultureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Vehicles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Vehicles",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "Ano",
                table: "Vehicles",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Administrators",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Perfil",
                table: "Administrators",
                newName: "Profile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Vehicles",
                newName: "Ano");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vehicles",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Vehicles",
                newName: "Marca");

            migrationBuilder.RenameColumn(
                name: "Profile",
                table: "Administrators",
                newName: "Perfil");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Administrators",
                newName: "Senha");
        }
    }
}
