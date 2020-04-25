using IWorker.Dto;
using IWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class MessagesService
    {
        private readonly IWorkerContext _context;

        public MessagesService(IWorkerContext context)
        {
            _context = context;
        }

        public bool SendToAll(string message)
        {
            List<int> usersList = new List<int>();

            usersList = _context.Users.Where(x => x.UserId != 0).Select(x => x.UserId).ToList();

            foreach (int id in usersList)
            {
                var newMessage = new Message
                {
                    From = 0, //from admin (admin has ID = 0)
                    To = id,
                    MessageText = message,
                    Date = DateTime.Today,
                };

                _context.Messages.Add(newMessage);
                
            }

            _context.SaveChanges();
            return true;
        }

        public bool SendToUser(string message, int from, int to)
        {
            var newMessage = new Message
            {
                From = from,
                To = to,
                MessageText = message,
                Date = DateTime.Today,
            };

            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            return true;
        }

        public List<MessageDto> GetMessageList(int userID)
        {
            List <MessageDto> messageList= new List<MessageDto>();

            var messages = _context.Messages.Where(x => x.To == userID);

            foreach (var item in messages)
            {
                var user = _context.Users.Where(x => x.UserId == item.From).FirstOrDefault();

                messageList.Add(new MessageDto
                {
                    MessageID = item.Id,
                    Date = item.Date.ToLongDateString(),
                    Worker = new UsersListDto
                    {
                        UserID = user.UserId,
                        Name = user.Name,
                        Surname = user.Surname
                    }
                });
            }

            return messageList;
        }

        public string GetMessage(int messageID)
        {
            return _context.Messages.Where(x => x.Id == messageID).FirstOrDefault().MessageText;
        }
    }
}
