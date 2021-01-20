using Common.Network;
using Server.DBModel;
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
        #region Constants

        private const string GENERAL_GROUP = "General";

        #endregion // Constants
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

        public void AddUser(string username)
        {
            _dbHandler.AddUser(username);
        }

        public void AddGroup(string groupname, List<string> users)
        {
            _dbHandler.AddGroup(groupname, users);
        }

        public void LeaveGroup(string username, string groupname)
        {
            _dbHandler.LeaveGroup(username, groupname);
        }

        public Dictionary<string, List<Message>> GetGroupMessages(string username)
        {
            Dictionary<string, List<Message>> groupMessageList = new Dictionary<string, List<Message>>()
            { 
                {
                    GENERAL_GROUP, new List<Message>()
                } 
            };

            List<MessageList> messageList = _dbHandler.GetMessageLogs(username);

            string message = String.Empty;

            messageList.ForEach(m =>
            {
                message = $"{m.Date} - {m.Username}:{m.Text}\n";

                if(String.IsNullOrEmpty(m.Target))
                {
                    groupMessageList[GENERAL_GROUP].Add(new Message(m.Username, m.Text, username == m.Target, m.Date));
                }
                else
                {
                    if (m.Target == username)
                    {
                        if (!groupMessageList.ContainsKey(m.Username))
                        {
                            groupMessageList.Add(m.Username, new List<Message>());
                        }

                        groupMessageList[m.Username].Add(new Message(m.Username, m.Text, false, m.Date));
                    }
                    else
                    {
                        if ((m.Username == username))
                        {
                            if (!groupMessageList.ContainsKey(m.Target))
                            {
                                groupMessageList.Add(m.Target, new List<Message>());
                            }

                            groupMessageList[m.Target].Add(new Message(m.Username, m.Text, true, m.Date));
                        }
                        else
                        {
                            if (!groupMessageList.ContainsKey(m.Target))
                            {
                                groupMessageList.Add(m.Target, new List<Common.Network.Message>());
                            }

                            groupMessageList[m.Target].Add(new Message(m.Username, m.Text, false, m.Date));
                        }

                    }     
                }
            });

            return groupMessageList;
        }

        public List<Message> GetEventLog(DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            List<Message> eventLog = new List<Message>();

            List<EventLog> eventLogs = _dbHandler.GetEventLog(firstDate, secondDate, eventType);

            foreach (EventLog log in eventLogs)
            {

                string message = $"{log.EventLogType} : {log.Text}";
                eventLog.Add(new Message("Event log", message, false, log.Date));
            }

            return eventLog;
        }

        public List<string> GetUsers()
        {
            List<string> users = new List<string>();

            List<UserList> userList = _dbHandler.GetUserList();

            users = userList.Select(u => u.Username).ToList();

            return users;
        }

        public Dictionary<string, List<string>> GetGroups(string username)
        {
            Dictionary<string, List<string>> groupList = new Dictionary<string, List<string>>();

            foreach(var group in _dbHandler.GetGroups(username))
            {
                groupList.Add(group.Groupname, group.UserList.Select(u => u.Username).ToList());
            }

            return groupList;
        }

        #endregion //Methods
    }
}
