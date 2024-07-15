using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace req_tracker_back.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ExecutorId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_ObserverId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ExecutorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ObserverId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ObserverId",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "Executor",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observer",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Executor",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Observer",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "ExecutorId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObserverId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ExecutorId",
                table: "Tickets",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ObserverId",
                table: "Tickets",
                column: "ObserverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ExecutorId",
                table: "Tickets",
                column: "ExecutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_ObserverId",
                table: "Tickets",
                column: "ObserverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
