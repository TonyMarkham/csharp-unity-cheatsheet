using System;
using System.Threading.Tasks;

public class VideoEventArgs : EventArgs
{
    public Video Video { get; set; }
}

public class VideoEncoder
{
    // Define an Event
    public event EventHandler<VideoEventArgs> VideoEncoded;

    public async void Encode(Video video)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        OnEncoded(video);
    }

    private void OnEncoded(Video video)
    {
        VideoEncoded?.Invoke(this, new VideoEventArgs{Video = video});
    }
}
