using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatGptApp.Models
{
    public class ChatGptModel
    {
        public ChatGptModel()
        {
            ImageUrls = new List<ImageData>();
        }
        public string Input { get; set; }
        public string Response { get; set; }

        public bool IsError { get; set; }
        public string ErrorMsg { get; set; }

        public bool ProcessImage { get; set; }

        public List<ImageData> ImageUrls { get; set; }
    }

    public class ChatImageDto
    {
        public string prompt { get; set; }
        public int n { get; set; }
        public string size { get; set; }
    }

    public class ImageChatOutput
    {
        public string created { get; set; }
        public List<ImageData> data { get; set; }
    }

    public class ImageData
    {
        public string url { get; set; }
    }

    public class ChatGptDto
    {
        public ChatGptDto()
        {
            messages = new List<ChatMessage>();
        }
        public string model { get; set; }

        public List<ChatMessage> messages { get; set; }

        public int n { get; set; }
    }

    public class ChatMessage
    {
        public string role { get; set; }

        public string content { get; set; }
    }

    public class ChatOutput
    {
        public string id { get; set; }
        public List<ChoiceObject> choices { get; set; }
    }

    public class ChoiceObject
    {
        public MessageObject message { get; set; }
    }

    public class MessageObject
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}