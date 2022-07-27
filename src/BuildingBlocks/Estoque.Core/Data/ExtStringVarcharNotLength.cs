using Microsoft.EntityFrameworkCore;

namespace Estoque.Core.Data;

public static class ExtStringVarcharNotLength
{
    public static void StringVarchar(this ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(
                         p => p.ClrType == typeof(string) && p.GetColumnType() == null
                     )))
            property.SetColumnType("varchar");
    }
}