using System;

namespace Common.Network
{
    public class FiltrationReceivedEventArgs
    {
        #region Properties

        public string Username { get; }

        public DateTime FirstDate { get; }
        public DateTime SecondDate { get; }

        public EventType EventType { get; }

        #endregion // Properties

        #region Constructors

        public FiltrationReceivedEventArgs(string username, DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            Username = username;
            FirstDate = firstDate;
            SecondDate = secondDate;
            EventType = eventType;
        }

        #endregion // Constructors
    }
}
