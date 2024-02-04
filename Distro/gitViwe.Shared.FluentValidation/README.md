## gitViwe.Shared.FluentValidation

### Nuget package:
```
dotnet add package gitViwe.Shared.FluentValidation 
```

### Extensions:

Some custom FluentValidation methods
```csharp
class FluentValidatorExtension {
    static IRuleBuilderOptions<T, string> MustBeValidUri<T>(this IRuleBuilder<T, string> ruleBuilder);
    static IRuleBuilderOptions<T, string> NotContain<T>(this IRuleBuilder<T, string> ruleBuilder, string searchString);
}
```