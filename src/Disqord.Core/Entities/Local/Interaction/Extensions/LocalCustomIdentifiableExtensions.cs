﻿using System.ComponentModel;

namespace Disqord;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class LocalCustomIdentifiableExtensions
{
    public static TCustomIdentifiable WithCustomId<TCustomIdentifiable>(this TCustomIdentifiable localCustomIdentifiable, string customId)
        where TCustomIdentifiable : ILocalCustomIdentifiableEntity
    {
        localCustomIdentifiable.CustomId = customId;
        return localCustomIdentifiable;
    }
}
