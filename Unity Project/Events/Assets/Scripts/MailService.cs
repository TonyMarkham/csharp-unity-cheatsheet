using System;

public class MailService
{
    public string Status { get; private set; }

    public MailService(string status)
    {
        Status = status;
    }
    
    public void OnVideoEncoding(object source, EventArgs args)
    {
        Status = string.Empty;
    }

    public void OnVideoEncoded(object source, VideoEventArgs args)
    {
        Status = $"{args.Video.Title} encoded";
    }
}
