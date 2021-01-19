using Common.Network;
using Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MessageHandler
    {
        #region Fields

        private readonly DatabaseController _dbController;

        #endregion //Fields

        #region Constructors

        public MessageHandler(DatabaseController dbController)
        {
            _dbController = dbController;
        }

        #endregion //Constructors

        #region Methods

        public void AddMessage(string username, string groupname, string text, DateTime date)
        {
            _dbController.AddMessage(username, groupname, text, date);
        }

        public void AddEvent(EventType eventType, string text, DateTime date)
        {
            _dbController.AddEvent(eventType, text, date);
        }

        public void AddGroup(EventType eventType, string text, DateTime date)
        {
            _dbController.AddGroup();
        }

        public Dictionary<string, List<Message>> GetGroupMessages(string username)
        {
            Dictionary<string, List<Message>> groupMessages = new Dictionary<string, List<Message>>()
            { 
                {
                "General", new List<Message>()
               } 
            };

            List<Message> messages = _dbController.GetMessageLog(username);
            return groupMessages;
        }

        #endregion //Methods
    }
}
