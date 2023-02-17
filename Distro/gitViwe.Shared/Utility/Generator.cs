namespace gitViwe.Shared;

/// <summary>
/// Provides common data generator methods
/// </summary>
public static class Generator
{
    /// <summary>
    /// Creates a random string
    /// </summary>
    /// <param name="combination">The alpha-numeric/none alpha-numeric combination to use</param>
    /// <param name="length">The number of characters the string will have</param>
    /// <returns>A random string of the specified length</returns>
    public static string RandomString(CharacterCombination combination = CharacterCombination.NumberAndUpper, int length = 5)
    {
        var random = new Random();
        string characters = combination switch
        {
            CharacterCombination.Lower => StringCharacter.LOWER,
            CharacterCombination.Upper => StringCharacter.UPPER,
            CharacterCombination.Number => StringCharacter.NUMBER,
            CharacterCombination.Symbol => StringCharacter.SYMBOL,
            CharacterCombination.Alphabet => StringCharacter.ALPHABET,
            CharacterCombination.NumberAndLower => StringCharacter.NUMBERANDLOWER,
            CharacterCombination.NumberAndUpper => StringCharacter.NUMBERANDUPPER,
            CharacterCombination.SymbolAndNumber => StringCharacter.SYMBOLANDNUMBER,
            CharacterCombination.SymbolAndNumberAndLower => StringCharacter.SYMBOLANDNUMBERANDLOWER,
            CharacterCombination.SymbolAndNumberAndUpper => StringCharacter.SYMBOLANDNUMBERANDUPPER,
            CharacterCombination.NumberAndAlphabet => StringCharacter.NUMBERANDALPHABET,
            CharacterCombination.SymbolAndNumberAndAlphabet => StringCharacter.SYMBOLANDNUMBERANDALPHABET,
            _ => throw new ArgumentOutOfRangeException(nameof(combination), $"Not expected combination value: {combination}")
        };
        return new string(Enumerable.Repeat(characters, length).Select(x => x[random.Next(x.Length)]).ToArray());
    }

    /// <summary>
    /// Provides common string types
    /// </summary>
    public static class StringCharacter
    {
        /// <summary>
        /// A lowercase alphabet string
        /// </summary>
        public const string LOWER = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// An uppercase alphabet string
        /// </summary>
        public const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// A numeric string
        /// </summary>
        public const string NUMBER = "0123456789";

        /// <summary>
        /// A none alpha-numeric string
        /// </summary>
        public const string SYMBOL = @"~!@#$%^&*()_-+=/\\|.";

        /// <summary>
        /// A lowercase and uppercase alphabet string
        /// </summary>
        public const string ALPHABET = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

        /// <summary>
        /// A lowercase alphabet and numeric string
        /// </summary>
        public const string NUMBERANDLOWER = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// An uppercase alphabet and numeric string
        /// </summary>
        public const string NUMBERANDUPPER = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// A none alphabet and numeric string
        /// </summary>
        public const string SYMBOLANDNUMBER = @"~!@#$%^&*()_-+=/\\|.0123456789";

        /// <summary>
        /// A lowercase alphabet, symbol and numeric string
        /// </summary>
        public const string SYMBOLANDNUMBERANDLOWER = @"~!@#$%^&*()_-+=/\\|.0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// An uppercase alphabet, symbol and numeric string
        /// </summary>
        public const string SYMBOLANDNUMBERANDUPPER = @"~!@#$%^&*()_-+=/\\|.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// A numeric lowercase and uppercase alphabet string
        /// </summary>
        public const string NUMBERANDALPHABET = "0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

        /// <summary>
        /// An uppercase and lowercase alphabet, symbol and numeric string
        /// </summary>
        public const string SYMBOLANDNUMBERANDALPHABET = @"~!@#$%^&*()_-+=/\\|.0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
    }
}
