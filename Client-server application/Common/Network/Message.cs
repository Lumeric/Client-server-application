using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class Message
    {
        #region Properties

        public string Username { get; set; }

        public string Text { get; set; }

        public bool IsOwner { get; set; }

        public DateTime DateTime { get; set; }

        #endregion //Properties

        #region Constructors

        public Message(string username, string text, bool isOwner, DateTime dateTime)
        {
            Username = username;
            Text = text;
            IsOwner = isOwner;
            DateTime = dateTime;
        }

        #endregion //Constructors
    }
}
