using System.Collections.Generic;

namespace Shared.Test.Data;
internal class Villian
{
    public string StreetName { get; set; } = string.Empty;
    public Affiliation Affiliation { get; set; }
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }

    public static Villian Get()
    {
        return new Villian()
        {
            StreetName = "Corrosive",
            Affiliation = Affiliation.Blockade,
            Elementals = new List<Elemental>()
            {
                Elemental.Poison
            },
            Morality = Morality.Vilian
        };
    }
}
