using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimateHours = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskJob_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("216a7728-0057-447f-a064-3111937c6cc5"), new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(953), "Em desenvolvimento" },
                    { new Guid("5eec74b8-b815-49b3-8eab-2b58f5569c64"), new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(954), "Em homologação" },
                    { new Guid("bba97714-da40-45dc-ad32-a55efb5e6895"), new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(966), "Concluído" },
                    { new Guid("fde2cc85-645d-49d6-8b04-60173f99e2f2"), new DateTime(2023, 3, 3, 16, 50, 26, 279, DateTimeKind.Utc).AddTicks(950), "Na fila" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskJob_StatusId",
                table: "TaskJob",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskJob");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}