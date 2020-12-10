namespace Common.Network
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Messages;
    using Newtonsoft.Json;

    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class WsClient : ITransport
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private WebSocket _socket;

        private int _sending;
        private string _login;

        #endregion //Fields

        #region Properties
        public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

        #endregion //Properties

        #region Events

        public event EventHandler<GroupCreatedEventArgs> GroupCreated;
        public event EventHandler<GroupRemovedEventArgs> GroupRemoved;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<UserConnectedToGroupEventArgs> UserConnectedToGroup;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;

        #endregion //Events

        #region Constructors

        public WsClient()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
        }

        #endregion //Constructors

        #region Methods

        public void Connect(string address, string port)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Login(string login)
        {
            throw new NotImplementedException();
        }

        public void Send(List<Guid> listClientId, MessageContainer message)
        {
            _sendQueue.Enqueue(message);

            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                SendImpl();
        }

        private void SendCompleted(bool completed)
        {
            // При отправке произошла ошибка.
            if (!completed)
            {
                Disconnect();
                UserDisconnected?.Invoke(this, new UserDisconnectedEventArgs(_login));
                return;
            }

            SendImpl();
        }

        private void SendImpl()
        {
            if (!IsConnected)
                return;

            if (!_sendQueue.TryDequeue(out var message) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
                return;

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(message, settings);
            _socket.SendAsync(serializedMessages, SendCompleted);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {

        }

        private void OnOpen(object sender, System.EventArgs e)
        {

        }

        private void OnClose(Object sender, CloseEventArgs e)
        {

        }

        #endregion //Methods
    }
}
