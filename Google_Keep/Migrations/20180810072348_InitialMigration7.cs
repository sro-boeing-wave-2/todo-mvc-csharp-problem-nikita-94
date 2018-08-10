using Microsoft.EntityFrameworkCore.Migrations;

namespace Google_Keep.Migrations
{
    public partial class InitialMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Notes_Notesid",
                table: "Checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_Notesid",
                table: "Label");

            migrationBuilder.RenameColumn(
                name: "Notesid",
                table: "Label",
                newName: "Noteid");

            migrationBuilder.RenameIndex(
                name: "IX_Label_Notesid",
                table: "Label",
                newName: "IX_Label_Noteid");

            migrationBuilder.RenameColumn(
                name: "Notesid",
                table: "Checklist",
                newName: "Noteid");

            migrationBuilder.RenameIndex(
                name: "IX_Checklist_Notesid",
                table: "Checklist",
                newName: "IX_Checklist_Noteid");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Note_Noteid",
                table: "Checklist",
                column: "Noteid",
                principalTable: "Notes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Note_Noteid",
                table: "Label",
                column: "Noteid",
                principalTable: "Notes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Note_Noteid",
                table: "Checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_Label_Note_Noteid",
                table: "Label");

            migrationBuilder.RenameColumn(
                name: "Noteid",
                table: "Label",
                newName: "Notesid");

            migrationBuilder.RenameIndex(
                name: "IX_Label_Noteid",
                table: "Label",
                newName: "IX_Label_Notesid");

            migrationBuilder.RenameColumn(
                name: "Noteid",
                table: "Checklist",
                newName: "Notesid");

            migrationBuilder.RenameIndex(
                name: "IX_Checklist_Noteid",
                table: "Checklist",
                newName: "IX_Checklist_Notesid");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Note_Notesid",
                table: "Checklist",
                column: "Notesid",
                principalTable: "Note",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Note_Notesid",
                table: "Label",
                column: "Notesid",
                principalTable: "Note",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
