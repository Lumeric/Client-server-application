using Common.Network;
using Server.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Server.Handlers
{
    public class DatabaseHandler
    {
        #region Fields

        private readonly string _connectionString;
        private readonly DatabaseContext _dbContext;

        #endregion //Fields

        #region Constructors

        public DatabaseHandler(ConnectionStringSettings connectionString)
        {
            _connectionString = connectionString.ToString();
            _dbContext = new DatabaseContext(_connectionString);
        }

        #endregion //Constructors

        #region Methods

        public void AddUser(string username)
        {
            var dbContext = new DatabaseContext(_connectionString);

            try
            {
                if (_dbContext.UserLists.Find(username) == null)
                {
                    UserList user = new UserList { Username = username };

                    using (dbContext)
                    {
                        dbContext.UserLists.Add(user);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                string error = $"Unexpected error occurred: {ex}";
                Console.WriteLine(error);

                using(dbContext)
                {
                    dbContext.EventLogs.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = DateTime.Now
                    });
                }
            }
            catch (NullReferenceException ex)
            {
                string error = $"Unexpected error occurred: {ex}";
                Console.WriteLine(error);

                using (dbContext)
                {
                    dbContext.EventLogs.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddMessage(string username, string target, string text, DateTime date)
        {
            MessageList message = new MessageList { Username = username, Target = target, Text = text, Date = date };
            var dbContext = new DatabaseContext(_connectionString);

            using (dbContext)
            {
                dbContext.MessageLists.Add(message);
                dbContext.SaveChanges();
            }
        }

        public void AddEvent(EventType eventType, string text, DateTime date)
        {
            EventLog log = new EventLog { EventLogType = eventType, Text = text, Date = date };
            var dbContext = new DatabaseContext(_connectionString);

            try
            {
                using (dbContext)
                {
                    dbContext.EventLogs.Add(log);
                    dbContext.SaveChanges();
                }
            }
            catch (NullReferenceException ex)
            {
                string error = $"Unexpected error occurred: {ex}";
                Console.WriteLine(error);

                using (dbContext)
                {
                    dbContext.EventLogs.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = date
                    });
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddGroup(string groupname, List<string> userList)
        {
            GroupList group = new GroupList { Groupname = groupname, UserList = new List<UserList>() };
            var dbContext = new DatabaseContext(_connectionString);
            try
            {
                using (dbContext)
                {
                    foreach (var user in userList)
                    {
                        dbContext.UserLists.Find(user).GroupList.Add(group);
                    }

                    dbContext.GroupLists.Add(group);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string error = $"This group already exists: {ex}";
                Console.WriteLine(error);

                using (dbContext)
                {
                    dbContext.EventLogs.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = DateTime.Now
                    });

                    dbContext.SaveChanges();
                }
            }
        }

        public void LeaveGroup(string username, string groupname)
        {
            var dbContext = new DatabaseContext(_connectionString);

            using (dbContext)
            {
                var user = dbContext.UserLists.Find(username);
                var group = dbContext.GroupLists.Find(groupname);

                group.UserList.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public List<UserList> GetUserList()
        {
            List<UserList> userList = _dbContext.UserLists.ToList();

            return userList;
        }

        public List<MessageList> GetMessageLogs(string username)
        {
            List<MessageList> messageLog = new List<MessageList>();
            List<string> groups = GetGroups(username).Select(g => g.Groupname).ToList();

            var messages = _dbContext.MessageLists.Where(m => m.Username == username || m.Target == username || String.IsNullOrEmpty(m.Target) ||
                                                          groups.Contains(m.Target));

            foreach (MessageList message in messages)
            {
                messageLog.Add(message);
            }

            return messageLog;
        }

        public List<EventLog> GetEventLog(DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            List<EventLog> eventLog = new List<EventLog>();

            var events = _dbContext.EventLogs.Where(e => e.Date >= firstDate && e.Date <= secondDate && eventType == e.EventLogType);

            return eventLog = events.ToList();
        }

        public List<GroupList> GetGroups(string login)
        {
            List<GroupList> groupList = new List<GroupList>();

            var groups = _dbContext.GroupLists.Where(g => g.UserList.Where(u => u.Username == login).Count() != 0);

            return groupList = groups.ToList();
        }

        #endregion //Methods
    }
}
