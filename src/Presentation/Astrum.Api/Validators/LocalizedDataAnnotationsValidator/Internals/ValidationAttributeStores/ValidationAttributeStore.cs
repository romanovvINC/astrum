﻿// This source code is fork from https://github.com/dotnet/corefx/blob/master/src/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/ValidationAttributeStore.cs
// The .NET Foundation licenses the original file of this forked file to you under the MIT license.
// See the LICENSE file for the original file of this forked file: https://github.com/dotnet/corefx/blob/master/LICENSE.TXT

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Astrum.Api.Validators.LocalizedDataAnnotationsValidator.Internals.ValidationAttributeStores;

internal class ValidationAttributeStore
{
    private readonly Dictionary<Type, TypeStoreItem> _typeStoreItems = new();
    static internal ValidationAttributeStore Instance { get; } = new();

    internal IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext validationContext)
    {
        var typeStoreItem = GetTypeStoreItem(validationContext.ObjectType);
        return typeStoreItem.ValidationAttributes;
    }

    private TypeStoreItem GetTypeStoreItem(Type type)
    {
        lock (_typeStoreItems)
        {
            if (!_typeStoreItems.TryGetValue(type, out var value))
            {
                var customAttributes = CustomAttributeExtensions.GetCustomAttributes(type, true);
                value = new TypeStoreItem(type, customAttributes);
                _typeStoreItems[type] = value;
            }

            return value;
        }
    }

    internal IEnumerable<ValidationAttribute> GetPropertyValidationAttributes(ValidationContext validationContext)
    {
        var typeStoreItem = GetTypeStoreItem(validationContext.ObjectType);
        var propertyStoreItem = typeStoreItem.GetPropertyStoreItem(validationContext.MemberName);
        return propertyStoreItem.ValidationAttributes;
    }
}