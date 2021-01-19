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
        private DatabaseContext _dbContext;

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
                if (_dbContext.Users.Find(username) == null)
                {
                    UserList user = new UserList { Username = username };

                    using (dbContext)
                    {
                        dbContext.Users.Add(user);
                        dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                string error = $"Unexpected error occurred: {ex}";
                Console.WriteLine(error);

                using(dbContext)
                {
                    dbContext.EventLog.Add(new EventLog
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

        public void AddMessage(string username, string groupname, string text, DateTime date)
        {
            MessageList message = new MessageList { Username = username, Groupname = groupname, Text = text, Date = date };
            var dbContext = new DatabaseContext(_connectionString);

            using (dbContext)
            {
                dbContext.Messages.Add(message);
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
                    dbContext.EventLog.Add(log);
                    dbContext.SaveChangesAsync();
                }
            }
            catch (NullReferenceException ex)
            {
                string error = $"Unexpected error occurred: {ex}";
                Console.WriteLine(error);

                using (dbContext)
                {
                    dbContext.EventLog.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = date
                    });
                    dbContext.SaveChangesAsync();
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
                        dbContext.Users.Find(user).GroupList.Add(group);
                    }

                    dbContext.GroupList.Add(group);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                string error = "This group already exists";
                Console.WriteLine(error);

                using (dbContext)
                {
                    dbContext.EventLog.Add(new EventLog
                    {
                        EventLogType = EventType.Error,
                        Text = error,
                        Date = DateTime.Now
                    });

                    dbContext.SaveChanges();
                }
            }
        }

        public void LeaveGroup()
        {

        }

        public List<UserList> GetClients()
        {
            List<UserList> userList = _dbContext.Users.ToList();

            return userList;
        }

        public List<Message> GetMessagesLog(string username)
        {
            List<Message> messagesLog = new List<Message>();

            return messagesLog;
        }

        public List<EventLog> GetEventLog(DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            List<EventLog> eventLog = new List<EventLog>();

            var events = _dbContext.EventLog.Where(e => e.Date >= firstDate && e.Date <= secondDate && eventType.HasFlag(e.EventLogType));

            return (eventLog = events.ToList());
        }
        public List<GroupList> GetGroups(string login)
        {
            List<GroupList> groupList = new List<GroupList>();

            var groups = _dbContext.GroupList.Where(item => item.UserList.Where(client => client.Username == login).Count() != 0);

            return (groupList = groups.ToList());
        }

        #endregion //Methods
    }
}
