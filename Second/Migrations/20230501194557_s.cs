using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Second.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vouchers_tourist_infos_tourist_id",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tourist_infos",
                table: "tourist_infos");

            migrationBuilder.DropIndex(
                name: "ix_tourist_infos_tourist_id",
                table: "tourist_infos");

            migrationBuilder.DropColumn(
                name: "tourist_info_id",
                table: "tourist_infos");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tourist_infos",
                table: "tourist_infos",
                column: "tourist_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vouchers_tourist_infos_tourist_id",
                table: "vouchers",
                column: "tourist_id",
                principalTable: "tourist_infos",
                principalColumn: "tourist_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vouchers_tourist_infos_tourist_id",
                table: "vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tourist_infos",
                table: "tourist_infos");

            migrationBuilder.AddColumn<int>(
                name: "tourist_info_id",
                table: "tourist_infos",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_tourist_infos",
                table: "tourist_infos",
                column: "tourist_info_id");

            migrationBuilder.CreateIndex(
                name: "ix_tourist_infos_tourist_id",
                table: "tourist_infos",
                column: "tourist_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vouchers_tourist_infos_tourist_id",
                table: "vouchers",
                column: "tourist_id",
                principalTable: "tourist_infos",
                principalColumn: "tourist_info_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
