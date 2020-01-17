using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FirstMVCMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpeakerProfiles",
                columns: table => new
                {
                    SpeakerId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Bio = table.Column<string>(maxLength: 100, nullable: true),
                    PhotoPath = table.Column<string>(maxLength: 200, nullable: true),
                    Company = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerProfiles", x => x.SpeakerId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    PresentationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 200, nullable: false),
                    LongDescription = table.Column<string>(maxLength: 800, nullable: false),
                    PresentationOwnerSpeakerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => x.PresentationId);
                    table.ForeignKey(
                        name: "FK_Presentations_SpeakerProfiles_PresentationOwnerSpeakerId",
                        column: x => x.PresentationOwnerSpeakerId,
                        principalTable: "SpeakerProfiles",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PresentationSpeakers",
                columns: table => new
                {
                    PresentationId = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentationSpeakers", x => new { x.PresentationId, x.SpeakerId });
                    table.ForeignKey(
                        name: "FK_PresentationSpeakers_Presentations_PresentationId",
                        column: x => x.PresentationId,
                        principalTable: "Presentations",
                        principalColumn: "PresentationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresentationSpeakers_SpeakerProfiles_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "SpeakerProfiles",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PresentationTags",
                columns: table => new
                {
                    PresentationId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentationTags", x => new { x.PresentationId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PresentationTags_Presentations_PresentationId",
                        column: x => x.PresentationId,
                        principalTable: "Presentations",
                        principalColumn: "PresentationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresentationTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Presentations_PresentationOwnerSpeakerId",
                table: "Presentations",
                column: "PresentationOwnerSpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentationSpeakers_SpeakerId",
                table: "PresentationSpeakers",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentationTags_TagId",
                table: "PresentationTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PresentationSpeakers");

            migrationBuilder.DropTable(
                name: "PresentationTags");

            migrationBuilder.DropTable(
                name: "Presentations");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SpeakerProfiles");
        }
    }
}