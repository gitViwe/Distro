using System.Collections.Generic;

namespace Shared.Test.Data;
internal class Hero
{
    public string Avatar { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public List<Elemental> Elementals { get; set; } = new();
    public Morality Morality { get; set; }

    public static Hero Get()
    {
        return new Hero()
        {
            Avatar = "006-superhero-5.svg",
            Alias = "Botanica",
            Elementals = new List<Elemental>()
            {
                Elemental.Nature,
                Elemental.Wind
            },
            Morality = Morality.SuperHero
        };
    }
}
