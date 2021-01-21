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
    using Newtonsoft.Json.Linq;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class WsClient : ITransport
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private WebSocket _socket;

        private int _sending;

        #endregion //Fields

        #region Properties

        public string Username { get; set; }

        public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

        #endregion //Properties

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<ConnectionReceivedEventArgs> ConnectionReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<MessageHistoryReceivedEventArgs> MessageHistoryReceived;
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;
        public event EventHandler<UsersReceivedEventArgs> UsersReceived;
        public event EventHandler<GroupsReceivedEventArgs> GroupsReceived;
        public event EventHandler<FilteredLogsReceivedEventArgs> FilteredLogsReceived;

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
            _socket = new WebSocket($"ws://{address}:{port}");
            _socket.OnOpen += OnOpen;
            _socket.OnClose += OnClose;
            _socket.OnMessage += OnMessage;
            _socket.ConnectAsync();
        }

        public void Send(MessageContainer message)
        {
            _sendQueue.Enqueue(message);

            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                SendImpl();
        }

        public void Disconnect()
        {
            if (_socket == null)
                return;

            if (IsConnected)
                _socket.CloseAsync();

            _socket.OnOpen -= OnOpen;
            _socket.OnClose -= OnClose;
            _socket.OnMessage -= OnMessage;

            Username = String.Empty;
            _socket = null;
        }

        private void SendCompleted(bool completed)
        {
            if (!completed)
            {
                Disconnect();
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(Username, false, DateTime.Now));
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
            if (!e.IsText)
                return;

            var container = JsonConvert.DeserializeObject<MessageContainer>(e.Data);

            switch (container.Identifier)
            {
                case nameof(ConnectionResponse):
                    var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
                    if (connectionResponse.Result == ResultCode.Failure)
                    {
                        Username = string.Empty;
                        string source = string.Empty;
                        ErrorReceived?.Invoke(this, new ErrorReceivedEventArgs(connectionResponse.Reason, connectionResponse.Date));
                    }
                    if (!String.IsNullOrEmpty(Username))
                    {
                        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(Username, true, DateTime.Now,  connectionResponse.ActiveUsers));
                    }
                    break;
                case nameof(ConnectionBroadcast):
                    var connectionBroadcast = ((JObject)container.Payload).ToObject(typeof(ConnectionBroadcast)) as ConnectionBroadcast;
                    if (connectionBroadcast.Username != Username)
                    {
                        ConnectionReceived?.Invoke(this, new ConnectionReceivedEventArgs(connectionBroadcast.Username, connectionBroadcast.IsConnected, connectionBroadcast.Date));
                    }
                    break;
                case nameof(MessageBroadcast):
                    var messageBroadcast = ((JObject)container.Payload).ToObject(typeof(MessageBroadcast)) as MessageBroadcast;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageBroadcast.Source, messageBroadcast.Target, messageBroadcast.Message,messageBroadcast.Groupname, messageBroadcast.Date));
                    break;
                case nameof(UserListResponse):
                    var userListResponse = ((JObject)container.Payload).ToObject(typeof(UserListResponse)) as UserListResponse;
                    UsersReceived?.Invoke(this, new UsersReceivedEventArgs(userListResponse.UserList));
                    break;
                case nameof(MessageHistoryResponse):
                    var messageHistoryResponse = ((JObject)container.Payload).ToObject(typeof(MessageHistoryResponse)) as MessageHistoryResponse;
                    MessageHistoryReceived?.Invoke(this, new MessageHistoryReceivedEventArgs(messageHistoryResponse.GroupMessages));
                    break;
                case nameof(FiltrationResponse):
                    var filterResponse = ((JObject)container.Payload).ToObject(typeof(FiltrationResponse)) as FiltrationResponse;
                    FilteredLogsReceived?.Invoke(this, new FilteredLogsReceivedEventArgs(filterResponse.FilteredLogs));
                    break;
                case nameof(GroupListResponse):
                    var groupsListResponse = ((JObject)container.Payload).ToObject(typeof(GroupListResponse)) as GroupListResponse;
                    GroupsReceived?.Invoke(this, new GroupsReceivedEventArgs(groupsListResponse.Groups));
                    break;
                case nameof(GroupBroadcast):
                    var groupBroadcast = ((JObject)container.Payload).ToObject(typeof(GroupBroadcast)) as GroupBroadcast;
                    GroupsReceived?.Invoke(this, new GroupsReceivedEventArgs(groupBroadcast.Groupname));
                    break;
            }
        }

        private void OnOpen(object sender, System.EventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(Username, true, DateTime.Now));
        }

        private void OnClose(Object sender, CloseEventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(Username, false, DateTime.Now));
        }

        #endregion //Methods
    }
}
