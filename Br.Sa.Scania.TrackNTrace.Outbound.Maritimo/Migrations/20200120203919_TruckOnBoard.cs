using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Migrations
{
    public partial class TruckOnBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "TruckOnBoards",
                columns: table => new
                {
                    TruckOnBoardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LicensePlate = table.Column<int>(nullable: false),
                    TrackNumber = table.Column<string>(nullable: true),
                    DataDeGravacao = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    DataDaLocalizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckOnBoards", x => x.TruckOnBoardId);
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortLocation");

            migrationBuilder.DropTable(
                name: "TruckOnBoards");

            migrationBuilder.DropTable(
                name: "VesselLocation");
        }
    }
}
