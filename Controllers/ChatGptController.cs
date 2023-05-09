using ChatGptApp.Helper;
using ChatGptApp.Models;
using ChatGptApp.Repository;
using ChatGptApp.ViewModel;
using Newtonsoft.Json;
using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatGptApp.Controllers
{
    public class ChatGptController : SitecoreController
    {
        private readonly ChatGptRepository _chatGptRepository;
        public ChatGptController(ChatGptRepository chatGptRepository)
        {
            _chatGptRepository = chatGptRepository;
        }

        // GET: ChatGpt
        public ActionResult DoChat()
        {
            var chatModel = _chatGptRepository.GetModel();
            var model = new ChatGptModel();
            if(string.IsNullOrEmpty(chatModel.Token) || string.IsNullOrEmpty(chatModel.ChatGptModel))
            {
                model.IsError = true;
                model.ErrorMsg = "ChatGpt setting is not defined. Please check";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveItem()
        {
            var content = this.ControllerContext.HttpContext.Request.Form[0];
            _chatGptRepository.SaveItem(content);
            return Json(content);
        }

        [HttpPost]
        public ActionResult SaveImage(ImageViewModel viewModel)
        {
            var url = this.ControllerContext.HttpContext.Request.Form[0];
            foreach (var img in viewModel.ImageUrls)
            {
                _chatGptRepository.SaveImage(img.Replace("'", ""));
            }
            return Json(url);
        }

        [ValidateFormHandler]
        public ActionResult Post()
        {
            var model = _chatGptRepository.GetModel();
            var input = this.ControllerContext.HttpContext.Request.Form["Input"];
            var imageChecked =this.ControllerContext.HttpContext.Request.Form["ProcessImage"].Contains("true");

            

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + model.Token },
                { "Content-Type", "application/json" }
            };

            if (!imageChecked)
            {
                var chatDto = new ChatGptApp.Models.ChatGptDto()
                {
                    messages = new List<Models.ChatMessage>(),
                    n = model.NumberOfMessages,
                    model = model.ChatGptModel
                };

                chatDto.messages.Add(new Models.ChatMessage
                {
                    content = input,
                    role = "user"
                });

                var msg = JsonConvert.SerializeObject(chatDto);

                var response = new HttpHelper().ExecuteRequest(RequestType.Post, model.ChatUrl, msg, "application/json", headers);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var chatOutput = JsonConvert.DeserializeObject<ChatOutput>(content);
                    return View("DoChat", new ChatGptModel() { IsError = false, Input = input, Response = chatOutput.choices.First().message.content });
                }
                var errorContent = response.Content.ReadAsStringAsync().Result;
                return View("DoChat", new ChatGptModel() { IsError = true, ErrorMsg = errorContent });
            }
            else
            {
                var imageDto = new ChatImageDto()
                {
                    n = model.NumberOfImages,
                    prompt = input,
                    size = model.Size
                };
                var msg = JsonConvert.SerializeObject(imageDto);
                var response = new HttpHelper().ExecuteRequest(RequestType.Post, model.ImageChatUrl, msg, "application/json", headers);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var imageOutput = JsonConvert.DeserializeObject<ImageChatOutput>(content);
                    return View("DoChat", new ChatGptModel() { IsError = false, Input = input, ProcessImage = true,  Response = content, ImageUrls = imageOutput.data });
                }
                var errorContent = response.Content.ReadAsStringAsync().Result;
                return View("DoChat", new ChatGptModel() { IsError = true, ErrorMsg = errorContent });
            }
            
        }
    }
}