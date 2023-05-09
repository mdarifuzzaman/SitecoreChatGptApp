using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatGptApp
{
    public struct Templates
    {
        public static readonly Sitecore.Data.ID TemplateId = new Sitecore.Data.ID("{70A39C63-AA54-4660-A967-0E28933A8536}");
        public struct Fields
        {
            public static readonly Sitecore.Data.ID ChatUrlId = new Sitecore.Data.ID("{1E7AB36F-27B9-4F84-80BA-0DBE3631BE72}");
            public static readonly Sitecore.Data.ID ImageChatUrl = new Sitecore.Data.ID("{1D5E33AE-13D3-4B42-AA09-11094F7094F2}");
            public static readonly Sitecore.Data.ID NumberOfMessages = new Sitecore.Data.ID("{89A67217-526D-4FEE-9014-826B6FF51505}");
            public static readonly Sitecore.Data.ID NumberOfImages = new Sitecore.Data.ID("{690A7878-5718-4C39-A3CF-9CF40AB98D25}");
            public static readonly Sitecore.Data.ID Token = new Sitecore.Data.ID("{E8C3B4E1-F312-4804-AD63-2C5AF7237C2B}");
            public static readonly Sitecore.Data.ID GptModel = new Sitecore.Data.ID("{7997B1D4-3F15-4A4E-8DC4-EF2A2E87C908}");
            public static readonly Sitecore.Data.ID ImageSize = new Sitecore.Data.ID("{094C699C-4D7A-49B3-ACB5-AB3F9592749B}");
            public static readonly Sitecore.Data.ID MediaPath = new Sitecore.Data.ID("{9772B258-7631-443E-834F-206DCCE8636D}");
            public static readonly Sitecore.Data.ID ItemSavedPath = new Sitecore.Data.ID("{F3792000-4F49-4409-BABC-CD028BECD4BE}");
            public static readonly Sitecore.Data.ID ItemSaveTemplateId = new Sitecore.Data.ID("{3254DFC9-18AE-478C-8F49-55AB5FF819F9}");
        }
    }
}