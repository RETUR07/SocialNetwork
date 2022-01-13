using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class VideoHub :BaseHub
    {
        public async Task UploadStream(ChannelReader<byte> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    await Clients.All.SendAsync("GetStreamPart", item);
                }
            }
        }
    }
}
