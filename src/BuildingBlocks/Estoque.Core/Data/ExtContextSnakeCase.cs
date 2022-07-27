using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Estoque.Core.Data;

public static class ExtContextSnakeCase
{
    public static void ToSnakeNames(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var schema = entity.GetSchema();
            var tableName = entity.GetTableName().ToSnakeCase();
            var tableStore = StoreObjectIdentifier.Table(tableName, schema);

            entity.SetTableName(tableName);

            foreach (var property in entity.GetProperties())
            {
                var newColumnName = property.GetColumnName(tableStore).ToSnakeCase();
                property.SetColumnName(newColumnName);
            }

            foreach (var key in entity.GetKeys())
            {
                var newKeyName = key.GetName(tableStore).ToSnakeCase();
                key.SetName(newKeyName);
            }

            foreach (var key in entity.GetForeignKeys())
            {
                var newKeyName = key.GetConstraintName().ToSnakeCase();
                key.SetConstraintName(newKeyName);
            }

            foreach (var index in entity.GetIndexes())
            {
                var newIndexName = index.GetDatabaseName().ToSnakeCase();
                index.SetDatabaseName(newIndexName);
            }
        }
    }

    private static string ToSnakeCase(this string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? name
            : Regex.Replace(
                name,
                @"([a-z0-9])([A-Z])",
                "$1_$2",
                RegexOptions.Compiled,
                TimeSpan.FromSeconds(1)).ToLower();
    }
}