using ChatGptApp.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ChatGptApp.Repository
{
    public class ChatGptRepository
    {

        public void SaveItem(string content)
        {

            var settingsItemQuery = $"fast://sitecore/content/Home/Settings//*[@@templateid='{Templates.TemplateId}']";
            var settingItems = Sitecore.Context.Database.SelectItems(settingsItemQuery);
            if (settingItems == null || !settingItems.Any())
            {
                return;
            }

            var settingItem = settingItems.First();
            var templateId = settingItem.Fields[Templates.Fields.ItemSaveTemplateId].GetValue(true);
            LinkField dataItem = settingItem.Fields[Templates.Fields.ItemSavedPath];
            Item rootItem;
            if (dataItem != null)
            {
                using (new SecurityDisabler())
                {
                    var template = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(templateId));
                    rootItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(dataItem.InnerField.Value));
                    var item = rootItem.Add("ChatGpt Item  - " + DateTime.UtcNow.Millisecond, template, Sitecore.Data.ID.NewID);
                    item.Editing.BeginEdit();
                    item["Data"] = content;
                    item.Editing.EndEdit();
                }
            }

            
        }

        public void SaveImage(string url)
        {

            var settingsItemQuery = $"fast://sitecore/content/Home/Settings//*[@@templateid='{Templates.TemplateId}']";
            var settingItems = Sitecore.Context.Database.SelectItems(settingsItemQuery);
            if (settingItems == null || !settingItems.Any())
            {
                return;
            }

            WebClient client = new WebClient();
            var contents = client.DownloadData(url);
            var stream = new MemoryStream(contents);

            var settingItem = settingItems.First();
            LinkField mediaItem = settingItem.Fields[Templates.Fields.MediaPath];
            Item rootMediaItem;
            if (mediaItem != null)
            {
                rootMediaItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(mediaItem.InnerField.Value));

                var mediaCreator = new MediaCreator();
                var dest = "ChatGpt Media - " + DateTimeOffset.UtcNow.Millisecond;
                var options = new MediaCreatorOptions
                {
                    Versioned = false,
                    IncludeExtensionInItemName = false,
                    Database = Sitecore.Context.Database,
                    Destination = rootMediaItem.Paths.ContentPath + "/" + dest
                };

                using (new SecurityDisabler())
                {
                    mediaCreator.CreateFromStream(stream, dest + ".png", options);
                }
            }


        }
        public ChatGptSettingsModel GetModel()
        {
            var model = new ChatGptSettingsModel();

            var settingsItemQuery = $"fast://sitecore/content/Home/Settings//*[@@templateid='{Templates.TemplateId}']";
            var settingItems = Sitecore.Context.Database.SelectItems(settingsItemQuery);
            if(settingItems == null || !settingItems.Any())
            {
                return model;
            }

            var settingItem = settingItems.First();
            model.ChatUrl = settingItem.Fields[Templates.Fields.ChatUrlId].GetValue(true);
            model.ImageChatUrl = settingItem.Fields[Templates.Fields.ImageChatUrl].GetValue(true);
            model.NumberOfImages = int.Parse(settingItem.Fields[Templates.Fields.NumberOfImages].GetValue(true));
            model.NumberOfMessages = int.Parse(settingItem.Fields[Templates.Fields.NumberOfMessages].GetValue(true));
            model.Token = settingItem.Fields[Templates.Fields.Token].GetValue(true);
            model.Size = settingItem.Fields[Templates.Fields.ImageSize].GetValue(true);
            model.SaveTemplateId = settingItem.Fields[Templates.Fields.ItemSaveTemplateId].GetValue(true);
            model.ChatGptModel = settingItem.Fields[Templates.Fields.GptModel].GetValue(true);

            LinkField mediaItem = settingItem.Fields[Templates.Fields.MediaPath];
            if(mediaItem != null)
            {
                model.MediaRootItem = mediaItem.InnerField.Item;
            }

            LinkField dataItem = settingItem.Fields[Templates.Fields.ItemSavedPath];
            if(dataItem != null)
            {
                model.DataRootItem = dataItem.InnerField.Item;
            }

            return model;
        }
    }
}