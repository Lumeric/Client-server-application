﻿using System;

namespace Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public string Message { get; set; }

        public string Target { get; set; }

        public string Groupname { get; set; }

        #endregion // Properties

        #region Constructors

        public MessageRequest(string message, string target, string groupName)
        {
            Message = message;
            Target = target;
            Groupname = groupName;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageRequest),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
