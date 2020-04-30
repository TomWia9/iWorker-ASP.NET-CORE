using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWorker.Dto;
using IWorker.Models;
using IWorker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IWorker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService messagesService;
        private readonly IWorkerContext _context;

        public MessagesController(IWorkerContext context)
        {
            _context = context;
            messagesService = new MessagesService(context);
        }

        [HttpPost("sendToAll")]
        public bool SendToAll([FromBody] MessageItemDto message)
        {
            return messagesService.SendToAll(message.Message);
        }

        [HttpPost("sendToUser/{from}/{to}")]
        public bool SendToUser([FromBody] MessageItemDto message, int from, int to)
        {
            return messagesService.SendToUser(message.Message, from, to);
        }

        [HttpGet("getMessageList/{userID}/{peroid}")]
        public List<MessageDto> GetMessageList(int userID, int peroid)
        {
            return messagesService.GetMessageList(userID, peroid);
        }

        [HttpGet("getMessage/{messageID}")]
        public MessageItemDto GetMessage(int messageID)
        {
            return messagesService.GetMessage(messageID);
        }
    }
}