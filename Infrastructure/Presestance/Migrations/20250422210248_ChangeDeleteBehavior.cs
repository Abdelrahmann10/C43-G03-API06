using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderEntities_OrderEntityId",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderEntities_OrderEntityId",
                table: "OrderItems",
                column: "OrderEntityId",
                principalTable: "OrderEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderEntities_OrderEntityId",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderEntities_OrderEntityId",
                table: "OrderItems",
                column: "OrderEntityId",
                principalTable: "OrderEntities",
                principalColumn: "Id");
        }
    }
}
