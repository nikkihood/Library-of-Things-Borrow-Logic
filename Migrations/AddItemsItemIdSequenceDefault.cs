using LibraryOfThingsM4.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryOfThingsM4.Migrations;

[DbContext(typeof(LibraryDbContext))]
[Migration("20260715000000_AddItemsItemIdSequenceDefault")]
public sealed class AddItemsItemIdSequenceDefault : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            DECLARE @startValue bigint = ISNULL(
                (SELECT MAX(CONVERT(bigint, [ItemID])) FROM [dbo].[Items]), 0) + 1;
            DECLARE @createSequenceSql nvarchar(max) =
                N'CREATE SEQUENCE [dbo].[Items_ItemID_Sequence] AS int START WITH '
                + CONVERT(nvarchar(20), @startValue) + N' INCREMENT BY 1;';
            EXEC sys.sp_executesql @createSequenceSql;

            ALTER TABLE [dbo].[Items]
                ADD CONSTRAINT [DF_Items_ItemID]
                DEFAULT (NEXT VALUE FOR [dbo].[Items_ItemID_Sequence]) FOR [ItemID];
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            ALTER TABLE [dbo].[Items] DROP CONSTRAINT [DF_Items_ItemID];
            DROP SEQUENCE [dbo].[Items_ItemID_Sequence];
            """);
    }
}
