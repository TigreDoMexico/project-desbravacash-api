using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TigreDoMexico.DesbravaCash.Api.Migrations
{
    /// <inheritdoc />
    public partial class desafios_solicitacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "transacao");

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
                name: "solicitacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    unidade_id = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_por = table.Column<Guid>(type: "uuid", nullable: false),
                    criado_em = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    valor = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    desafio_id = table.Column<Guid>(type: "uuid", nullable: true),
                    transacao_id = table.Column<Guid>(type: "uuid", nullable: true),
                    UnidadeId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_solicitacao_desafio_desafio_id",
                        column: x => x.desafio_id,
                        principalTable: "desafio",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_solicitacao_transacao_transacao_id",
                        column: x => x.transacao_id,
                        principalTable: "transacao",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_solicitacao_unidade_UnidadeId1",
                        column: x => x.UnidadeId1,
                        principalTable: "unidade",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_solicitacao_unidade_unidade_id",
                        column: x => x.unidade_id,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solicitacao_usuario_criado_por",
                        column: x => x.criado_por,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_criado_por",
                table: "solicitacao",
                column: "criado_por");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_desafio_id",
                table: "solicitacao",
                column: "desafio_id");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_transacao_id",
                table: "solicitacao",
                column: "transacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_unidade_id",
                table: "solicitacao",
                column: "unidade_id");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacao_UnidadeId1",
                table: "solicitacao",
                column: "UnidadeId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitacao");

            migrationBuilder.DropTable(
                name: "desafio");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "transacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
