using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Application.Contracts;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class VideoHub : BaseHub
    {
        public async Task UploadStream(ChannelReader<string> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    string groupname = "video-" + UserId;
                    await Clients.Group(groupname).SendAsync("VideoPart", item);
                }
            }
        }

        public async Task SubscribeStream(int userId)
        {
            string groupname = "video-" + userId;
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Caller.SendAsync("Notify", "Subscribed");
        }
    }
}
