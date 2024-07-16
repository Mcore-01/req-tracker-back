using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace req_tracker_back.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableTicketAddFieldNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tickets");
        }
    }
}
