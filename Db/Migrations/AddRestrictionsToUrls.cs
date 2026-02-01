using Db.Models;
using FluentMigrator;

namespace Db.Migrations;

[Migration(202601302000)]
public class AddRestrictionsToUrls : Migration
{
    public override void Up()
    {
        Create.Index("UX_Sources_Url")
            .OnTable(Source.TableName)
            .OnColumn(nameof(Source.Url))
            .Unique();
    }

    public override void Down()
    {
        Delete.Index("UX_Sources_Url").OnTable(Source.TableName);
    }
}