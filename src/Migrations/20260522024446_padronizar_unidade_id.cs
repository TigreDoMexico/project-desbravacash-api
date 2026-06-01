using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TigreDoMexico.DesbravaCash.Api.Migrations
{
    /// <inheritdoc />
    public partial class padronizar_unidade_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_solicitacao_unidade_UnidadeId1",
                table: "solicitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_transacao_unidade_UnidadeId",
                table: "transacao");

            migrationBuilder.DropIndex(
                name: "IX_solicitacao_UnidadeId1",
                table: "solicitacao");

            migrationBuilder.DropColumn(
                name: "UnidadeId1",
                table: "solicitacao");

            migrationBuilder.RenameColumn(
                name: "UnidadeId",
                table: "transacao",
                newName: "unidade_id");

            migrationBuilder.RenameIndex(
                name: "IX_transacao_UnidadeId",
                table: "transacao",
                newName: "IX_transacao_unidade_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transacao_unidade_unidade_id",
                table: "transacao",
                column: "unidade_id",
                principalTable: "unidade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transacao_unidade_unidade_id",
                table: "transacao");

            migrationBuilder.RenameColumn(
                name: "unidade_id",
                table: "transacao",
                newName: "UnidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_transacao_unidade_id",
                table: "transacao",
                newName: "IX_transacao_UnidadeId");

            migrationBuilder.AddColumn<Guid>(
                name: "UnidadeId1",
                table: "solicitacao",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_UnidadeId1",
                table: "solicitacao",
                column: "UnidadeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_solicitacao_unidade_UnidadeId1",
                table: "solicitacao",
                column: "UnidadeId1",
                principalTable: "unidade",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_transacao_unidade_UnidadeId",
                table: "transacao",
                column: "UnidadeId",
                principalTable: "unidade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
