using System;
using System.ComponentModel.DataAnnotations;

namespace Server.DBModel
{
    public class MessageList
    {
        #region Properties

        [Key]
        public int MessageID { get; set; }

        public string Username { get; set; }

        public string Groupname { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        #endregion Properties
    }
}
