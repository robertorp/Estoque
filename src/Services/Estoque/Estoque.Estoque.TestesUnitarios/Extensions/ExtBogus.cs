using Bogus;
using Bogus.DataSets;

namespace Estoque.Estoque.TestesUnitarios.Extensions;

public static class ExtBogus
{
    public static string ProductUnitMeasure(this Commerce comerce)
    {
        return new Randomizer().ArrayElement(new[] { "Kg", "Lt", "Un", "Cx" });
    }
}