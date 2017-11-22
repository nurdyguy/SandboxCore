using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SandboxCore.Helpers;


namespace SandboxCore.Controllers
{
    [Authorize("Chat")]
    public class ChatController : Controller
    {
        private readonly ChatSocketManager _socketManager;

        public ChatController(ChatSocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        [HttpGet, Route("chat/room")]
        public IActionResult ChatRoom()
        {
            return View();
        }

        [HttpPost, Route("chat/room")]
        public IActionResult ChatRoom(string username)
        {
            return View("ChatRoom", username);
        }

        [HttpPost, Route("chat")]
        public async Task<IActionResult> PostMessage(string message)
        {
            if (message.Contains("x"))
                return Json(new { success = false, message = "Watch your language!"});

            var newMessage = $"{User.Identity.Name}: {message}";
            await _socketManager.SendMessageToAllAsync(newMessage);
            return Json(new { success = true});
        }

    }
}
