using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649

public class ApplicationManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField videoToEncode;
    [SerializeField]
    private TextMeshProUGUI emailMessage;
    [SerializeField]
    private TextMeshProUGUI textMessage;
    [SerializeField]
    private Button startButton;

    private VideoEncoder _videoEncoder;
    private MailService _mailService;
    private MessageService _messageService;
    
    // Define an Event
    public event EventHandler BeginEncoding;

    private void Start()
    {
        _videoEncoder = new VideoEncoder();
        _mailService = new MailService("");
        _messageService = new MessageService("");

        BeginEncoding += _mailService.OnVideoEncoding;
        BeginEncoding += _messageService.OnVideoEncoding;
        
        _videoEncoder.VideoEncoded += _mailService.OnVideoEncoded;
        _videoEncoder.VideoEncoded += _messageService.OnVideoEncoded;
        _videoEncoder.VideoEncoded += OnVideoEncoded;

        FocusInput();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            Encode();
            return;
        }

        if (_mailService.Status.Equals(emailMessage.text) && _messageService.Status.Equals(textMessage.text)) return;

        emailMessage.text = _mailService.Status;
        textMessage.text = _messageService.Status;
    }

    private void OnVideoEncoded(object sender, VideoEventArgs videoEventArgs)
    {
        videoToEncode.text = string.Empty;
        startButton.interactable = true;

        FocusInput();
    }

    private void FocusInput()
    {
        videoToEncode.interactable = true;
        videoToEncode.Select();
        videoToEncode.ActivateInputField();
    }
    
    public void Encode()
    {
        if (videoToEncode.text.Equals(string.Empty)) return;
        videoToEncode.interactable = false;
        startButton.interactable = false;
        _videoEncoder.Encode(new Video{Title = videoToEncode.text});
        videoToEncode.text = $"Encoding: {videoToEncode.text}";
        BeginEncoding?.Invoke(this, EventArgs.Empty);
    }
}
