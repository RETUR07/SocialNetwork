using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class VideoHub :BaseHub
    {
        private static Channel<byte[]> channel = Channel.CreateUnbounded<byte[]>();

        public async Task UploadStream(ChannelReader<byte[]> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    channel.Writer.TryWrite(item);
                }
            }
        }

        public ChannelReader<byte[]> DownloadStream()
        {
            return channel.Reader;
        }
    }
}
