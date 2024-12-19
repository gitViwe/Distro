namespace gitViwe.Shared.Attribute;

/// <summary>
/// Marks the property to be obfuscated. <seealso cref="Conversion.ToObfuscatedString{TValue}(TValue, IEnumerable{string})"/>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ObfuscateAttribute : System.Attribute;
