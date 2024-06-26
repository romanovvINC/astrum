﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Astrum.SharedLib.Common.Extensions;

public static class EnumExtensions
{
    public static List<T> ToList<T>(this T value) where T : Enum
    {
        return value.ToString().Split(',').Select(flag => (T)Enum.Parse(typeof(T), flag)).ToList();
    }

    public static string GetDisplayName<T>(this T enumValue) where T : Enum
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? enumValue.ToString();
    }

    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
        var enumType = enumValue.GetType();
        var field = enumType.GetField(enumValue.ToString());
        var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();
        return attributes.Count == 0 ? enumValue.ToString() : ((DescriptionAttribute)attributes[0]).Description;
    }
}