using System.Threading.Tasks;
using Disqord.Gateway.Api;
using Disqord.Gateway.Api.Models;

namespace Disqord.Gateway.Default.Dispatcher
{
    public class GuildMemberAddHandler : Handler<GuildMemberAddJsonModel, MemberJoinedEventArgs>
    {
        public override ValueTask<MemberJoinedEventArgs> HandleDispatchAsync(IGatewayApiClient shard, GuildMemberAddJsonModel model)
        {
            IMember member = null;
            var sharedUser = Dispatcher.GetOrAddSharedUser(model.User.Value);
            if (sharedUser != null && CacheProvider.TryGetMembers(model.GuildId, out var cache))
            {
                member = new CachedMember(sharedUser, model.GuildId, model);
                cache.Add(member.Id, member as CachedMember);
            }

            if (CacheProvider.TryGetGuilds(out var guildCache) && guildCache.TryGetValue(model.GuildId, out var guild))
                guild.UpdateMemberAdded();

            member ??= new TransientMember(Client, model.GuildId, model);

            var e = new MemberJoinedEventArgs(member);
            return new(e);
        }
    }
}
