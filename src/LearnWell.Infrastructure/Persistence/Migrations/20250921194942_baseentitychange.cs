using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnWell.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class baseentitychange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""Students"" ALTER COLUMN ""ModifiedBy"" TYPE uuid USING ""ModifiedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Students"" ALTER COLUMN ""CreatedBy"" TYPE uuid USING ""CreatedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Students"" ALTER COLUMN ""CreatedBy"" DROP NOT NULL;");

            migrationBuilder.Sql(@"ALTER TABLE ""staff"" ALTER COLUMN ""ModifiedBy"" TYPE uuid USING ""ModifiedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""staff"" ALTER COLUMN ""CreatedBy"" TYPE uuid USING ""CreatedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""staff"" ALTER COLUMN ""CreatedBy"" DROP NOT NULL;");

            migrationBuilder.Sql(@"ALTER TABLE ""Courses"" ALTER COLUMN ""ModifiedBy"" TYPE uuid USING ""ModifiedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Courses"" ALTER COLUMN ""CreatedBy"" TYPE uuid USING ""CreatedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Courses"" ALTER COLUMN ""CreatedBy"" DROP NOT NULL;");

            migrationBuilder.Sql(@"ALTER TABLE ""Classes"" ALTER COLUMN ""ModifiedBy"" TYPE uuid USING ""ModifiedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Classes"" ALTER COLUMN ""CreatedBy"" TYPE uuid USING ""CreatedBy""::uuid;");
            migrationBuilder.Sql(@"ALTER TABLE ""Classes"" ALTER COLUMN ""CreatedBy"" DROP NOT NULL;");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Students",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "staff",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "staff",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Courses",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Classes",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Classes",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
