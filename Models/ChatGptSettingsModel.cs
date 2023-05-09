using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatGptApp.Models
{
    public class ChatGptSettingsModel
    {
        public string ChatUrl { get; set; }
        public string ImageChatUrl { get; set; }
        public string Token { get; set; }
        public string ChatGptModel { get; set; }
        public Item MediaRootItem { get; set; }
        public Item DataRootItem { get; set; }
        public string SaveTemplateId { get; set; }
        public string Size { get; set; }
        public int NumberOfMessages { get; set; }
        public int NumberOfImages { get; set; }
    }
}