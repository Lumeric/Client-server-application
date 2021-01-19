namespace Common.Network
{
    using System;
    using WebSocketSharp;
    using WebSocketSharp.Server;
    using Newtonsoft.Json;
    using System.Threading;
    using System.Collections.Concurrent;
    using Messages;

    public class WsConnection : WebSocketBehavior
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        public WsServer _wsServer;

        private int _sending;

        #endregion //Fields

        #region Properties

        public Guid Id { get; set; }

        public string Username { get; set; }

        public bool IsConnected => Context.WebSocket?.ReadyState == WebSocketState.Open;

        #endregion //Properties

        #region Constructors

        public WsConnection()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
            Id = Guid.NewGuid();
        }

        #endregion //Constructors

        #region Methods

        public void AddServer(WsServer server)
        {
            _wsServer = server;
        }

        public void Send(MessageContainer container)
        {
            if (!IsConnected)
                return;

            _sendQueue.Enqueue(container);

            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                SendImpl();
        }

        public void Close()
        {
            Context.WebSocket.Close();
        }

        protected override void OnOpen()
        {
            _wsServer.AddConnection(this);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _wsServer.RemoveConnection(Id);
            string serializedMessages = JsonConvert.SerializeObject(Container.GetContainer(nameof(DisconnectRequest), 
                                                                                        new DisconnectRequest(Username)));
            var message = JsonConvert.DeserializeObject<MessageContainer>(serializedMessages);

        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsText)
            {
                var message = JsonConvert.DeserializeObject<MessageContainer>(e.Data);
                _wsServer.HandleMessage(Id, message);
            }
        }

        private void SendCompleted(bool completed)
        {
            // При отправке произошла ошибка.
            if (!completed)
            {
                _wsServer.RemoveConnection(Id);
                Context.WebSocket.CloseAsync();
                return;
            }

            SendImpl();
        }

        private void SendImpl()
        {
            if (!IsConnected)
                return;

            if (!_sendQueue.TryDequeue(out var message) && Interlocked.CompareExchange(ref _sending, 1, 0) == 1)
                return;

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(message, settings);
            SendAsync(serializedMessages, SendCompleted);
        }

        #endregion //Methods
    }
}
