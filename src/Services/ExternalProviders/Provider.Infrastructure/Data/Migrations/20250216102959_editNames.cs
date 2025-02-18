using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class editNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
              name: "MasaryServiceslists", 
              newName: "MasaryService"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
              name: "MasaryServiceslists",  
              newName: "MasaryService");  
        }
    }
}
