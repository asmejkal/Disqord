﻿using System;
using Disqord.Models;
using Qommon;

namespace Disqord.AuditLogs;

public class TransientIntegrationAuditLogData : IIntegrationAuditLogData
{
    /// <inheritdoc/>
    public Optional<bool> EnablesEmojis { get; }

    /// <inheritdoc/>
    public Optional<IntegrationExpirationBehavior> ExpirationBehavior { get; }

    /// <inheritdoc/>
    public Optional<TimeSpan> ExpirationGracePeriod { get; }

    public TransientIntegrationAuditLogData(IClient client, AuditLogEntryJsonModel model, bool isCreated)
    {
        var changes = new TransientIntegrationAuditLogChanges(client, model);
        if (isCreated)
        {
            EnablesEmojis = changes.EnablesEmojis.NewValue;
            ExpirationBehavior = changes.ExpireBehavior.NewValue;
            ExpirationGracePeriod = changes.ExpireGracePeriod.NewValue;
        }
        else
        {
            EnablesEmojis = changes.EnablesEmojis.OldValue;
            ExpirationBehavior = changes.ExpireBehavior.OldValue;
            ExpirationGracePeriod = changes.ExpireGracePeriod.OldValue;
        }
    }
}
