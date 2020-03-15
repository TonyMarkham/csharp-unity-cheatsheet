using System;

public class MessageService
{
    public string Status { get; private set; }

    public MessageService(string status)
    {
        Status = status;
    }
    
    public void OnVideoEncoding(object source, EventArgs args)
    {
        Status = string.Empty;
    }
    
    public void OnVideoEncoded(object source, VideoEventArgs args)
    {
        Status = $"{args.Video.Title} Encoded";
    }
}
