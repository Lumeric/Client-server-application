using Common.Network;
using Server.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class RequestHandler
    {
        #region Fields

        private readonly DatabaseHandler _dbHandler;

        #endregion //Fields

        #region Constructors

        public RequestHandler(DatabaseHandler dbController)
        {
            _dbHandler = dbController;
        }

        #endregion //Constructors

        #region Methods

        public void AddMessage(string username, string groupname, string text, DateTime date)
        {
            _dbHandler.AddMessage(username, groupname, text, date);
        }

        public void AddEvent(EventType eventType, string text, DateTime date)
        {
            _dbHandler.AddEvent(eventType, text, date);
        }

        public void AddGroup(string username, List<string> users)
        {
            _dbHandler.AddGroup();
        }

        public void LeaveGroup(string username, string groupname)
        {
            _dbHandler.LeaveGroup();
        }

        public Dictionary<string, List<Message>> GetGroupMessages(string username)
        {
            Dictionary<string, List<Message>> groupMessages = new Dictionary<string, List<Message>>()
            { 
                {
                    "General", new List<Message>()
                } 
            };

            List<Message> messages = _dbHandler.GetMessageLog(username);
            return groupMessages;
        }

        public List<Message> GetEventLog(DateTime firstDate, DateTime secondDate, EventType eventType)
        {

        }

        public Dictionary<string, List<string>> GetGroupUsers(string username)
        {
            Dictionary<string, List<string>> users = new Dictionary<string, List<string>>();

            foreach(var group in _dbHandler.GetGroups(username))
            {

            }

            return users;
        }

        #endregion //Methods
    }
}
