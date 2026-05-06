using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TigreDoMexico.DesbravaCash.Api.Migrations
{
    /// <inheritdoc />
    public partial class desafios_unidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "desafio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pontuacao = table.Column<int>(type: "integer", nullable: false),
                    data_conclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pode_solicitar = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desafio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "solicitacao_desafio",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DesafioId = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    criado_em = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    transacao_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacao_desafio", x => x.id);
                    table.ForeignKey(
                        name: "FK_solicitacao_desafio_desafio_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "desafio",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solicitacao_desafio_transacao_transacao_id",
                        column: x => x.transacao_id,
                        principalTable: "transacao",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_solicitacao_desafio_unidade_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solicitacao_desafio_usuario_criado_por",
                        column: x => x.criado_por,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_desafio_criado_por",
                table: "solicitacao_desafio",
                column: "criado_por");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_desafio_DesafioId",
                table: "solicitacao_desafio",
                column: "DesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_desafio_transacao_id",
                table: "solicitacao_desafio",
                column: "transacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_desafio_UnidadeId",
                table: "solicitacao_desafio",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitacao_desafio");

            migrationBuilder.DropTable(
                name: "desafio");
        }
    }
}
