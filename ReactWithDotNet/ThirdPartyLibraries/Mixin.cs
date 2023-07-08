using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

namespace ReactWithDotNet.ThirdPartyLibraries;

static class Mixin
{
    public const string Core__CalculateSyntheticChangeEventArguments = "ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments";
    public const string Core__ReplaceNullWhenEmpty = "ReactWithDotNet::Core::ReplaceNullWhenEmpty";
    
    
    public static void ApplyAll<T>(this T[] items, Action<T> action  )
    {
        if (action is null)
        {
            return;
        }
        
        if (items is null)
        {
            return;
        }
        
        foreach (var item in items)
        {
            action(item);
        }
        
    }
}