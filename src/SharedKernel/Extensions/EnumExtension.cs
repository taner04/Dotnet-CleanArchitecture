namespace SharedKernel.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null)
        {
            return value.ToString();
        }

        var attributes =
            (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static string GetDescription<T>(this int value)
    {
        var @enum = GetEnumByInt<T>(value);
        return @enum.GetDescription();
    }

    public static Enum GetEnumByInt<T>(int value)
    {
        var enums = Enum.GetValues(typeof(T));
        foreach (var enumValue in enums)
        {
            if ((int)enumValue == value)
            {
                return (Enum)enumValue;
            }
        }

        throw new ArgumentException($"No enum value found for {value} in {typeof(T).Name}");
    }
}