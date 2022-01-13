using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class VideoHub :BaseHub
    {
        private Channel<string> channel = Channel.CreateUnbounded<string>();

        public async Task UploadStream(ChannelReader<string> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    channel.Writer.TryWrite(item);
                }
            }
        }

        public ChannelReader<string> DownloadStream()
        {
            return channel.Reader;
        }
    }
}
