using Common.Network;
using Server.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Server.Controllers
{
    public class DatabaseController
    {
        #region Fields

        private readonly string _connectionString;
        private readonly DatabaseContext _dbContext;

        #endregion //Fields

        #region Constructors

        public DatabaseController(ConnectionStringSettings connectionString)
        {
            _connectionString = connectionString.ToString();
            _dbContext = new DatabaseContext(_connectionString);
        }

        #endregion //Constructors

        #region Methods

        public void AddMessage(string username, string groupname, string text, DateTime date)
        {
            MessageList message = new MessageList { Username = username, Groupname = groupname, Text = text, Date = date };

            using (var context = new DatabaseContext(_connectionString))
            {
                context.Messages.Add(message);
                context.SaveChanges();
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
            catch (NullReferenceException)
            {
                string error = "Null reference";
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

        public void AddGroup()
        {

        }

        public List<Message> GetMessagesLog(string username)
        {
            List<Message> messagesLog = new List<Message>();

            return messagesLog;
        }

        #endregion //Methods
    }
}
