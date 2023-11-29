namespace gitViwe.Shared.Attribute;

/// <summary>
/// Marks the property to be obfuscated when used in <seealso cref="Conversion.ToObfuscatedString{TValue}(TValue)"/>
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class ObfuscateAttribute : System.Attribute { }
