using Common.Network;
using System;
using System.ComponentModel.DataAnnotations;

namespace Server.DBModel
{
    public class EventLog
    {
        #region Properties

        [Key]
        public int ID { get; set; }

        public EventType EventLogType { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        #endregion //Properties
    }
}
