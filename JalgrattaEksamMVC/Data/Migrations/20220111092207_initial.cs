﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace JalgrattaEksamMVC.Data.Migrations
{
#pragma warning disable IDE1006 // Naming Styles
    public partial class initial : Migration
#pragma warning restore IDE1006 // Naming Styles
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eksam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Perenimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Teooria = table.Column<int>(type: "int", nullable: false),
                    Slaalom = table.Column<int>(type: "int", nullable: false),
                    Ringtee = table.Column<int>(type: "int", nullable: false),
                    Tänav = table.Column<int>(type: "int", nullable: false),
                    Luba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eksam", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eksam_Eesnimi_Perenimi",
                table: "Eksam",
                columns: new[] { "Eesnimi", "Perenimi" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eksam");
        }
    }
}
