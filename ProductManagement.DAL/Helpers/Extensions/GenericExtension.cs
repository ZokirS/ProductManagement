namespace ProductManagement.DAL.Helpers.Extensions;

public static class GenericExtension
{
    public static List<string> CopyProperties<T>(this T destination, T source) where T : class
    {
        var sourceProperties = source.GetType().GetProperties();
        var destinationProperties = destination.GetType().GetProperties();

        var result = new List<string>();
        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);

            if (destinationProperty != null && sourceProperty.Name != "CreatedAt" && sourceProperty.GetValue(source) != destinationProperty.GetValue(destination))
            {
                result.Add($"property - {destinationProperty.Name}, ex-value - {destinationProperty.GetValue(destination)}, new value - {sourceProperty.GetValue(source)}");
                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
        }
        return result;
    }

    public static void CopyPropertiesFrom<T, T1>(this T destination, T1 source) where T : class where T1 : class
    {
        var sourceProperties = source.GetType().GetProperties();
        var destinationProperties = destination.GetType().GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);

            if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType && sourceProperty.GetValue(source) != destinationProperty.GetValue(destination))
            {
                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
        }
    }
}

