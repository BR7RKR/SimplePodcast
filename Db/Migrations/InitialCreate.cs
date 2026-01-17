using Db.Models;
using FluentMigrator;

namespace Db.Migrations;

[Migration(202601202012)]
public class InitialCreate : Migration
{
    public override void Up()
    {
        Create.Table(Source.TableName)
            .WithColumn(nameof(Source.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(Source.Url)).AsFixedLengthString(300).NotNullable()
            .WithColumn(nameof(Source.Type)).AsInt32().NotNullable();

        Create.Table(Podcast.TableName)
            .WithColumn(nameof(Podcast.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(Podcast.Title)).AsFixedLengthString(200).Nullable()
            .WithColumn(nameof(Podcast.Description)).AsString().Nullable()
            .WithColumn(nameof(Podcast.SourceId)).AsInt32().ForeignKey(Source.TableName, nameof(Source.Id));
    }

    public override void Down()
    {
        Delete.Table(Podcast.TableName);
        Delete.Table(Source.TableName);
    }
}