﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class MessageHistoryReceivedEventArgs
    {
        #region Properties

        public Dictionary<string, List<Message>> UserMessages { get; }

        #endregion // Properties

        #region Constructors

        public MessageHistoryReceivedEventArgs(Dictionary<string, List<Message>> userMessages)
        {
            UserMessages = userMessages;
        }

        #endregion // Constructors
    }
}
