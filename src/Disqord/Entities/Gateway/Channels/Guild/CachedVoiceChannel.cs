﻿using System.Collections.Generic;
using Disqord.Collections;
using Disqord.Models;

namespace Disqord
{
    public sealed partial class CachedVoiceChannel : CachedNestedChannel, IVoiceChannel
    {
        public int Bitrate { get; private set; }

        public int UserLimit { get; private set; }

        public IReadOnlyDictionary<Snowflake, CachedMember> Members { get; }

        internal CachedVoiceChannel(CachedGuild guild, ChannelModel model) : base(guild, model)
        {
            Members = new ReadOnlyValuePredicateArgumentDictionary<Snowflake, CachedMember, Snowflake>(
                guild._members, (x, id) => x.VoiceChannel != null && x.VoiceChannel.Id == id, Id);
            Update(model);
        }

        internal override void Update(ChannelModel model)
        {
            if (model.Bitrate.HasValue)
                Bitrate = model.Bitrate.Value;

            if (model.UserLimit.HasValue)
                UserLimit = model.UserLimit.Value;

            base.Update(model);
        }
    }
}
