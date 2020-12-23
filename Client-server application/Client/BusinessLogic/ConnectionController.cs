using Common.Network;
using Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.BusinessLogic
{
    //public class ConnectionController : IConnectionController
    //{
    //    #region Constants



    //    #endregion //Constants

    //    #region Events

    //    public event EventHandler<UserConnectedEventArgs> UserConnected;

    //    #endregion //Events

    //    #region Fields

    //    private ITransport _transport;
    //    private List<TransportType> _sockets;
    //    private TransportType _selectedSocket;
    //    private ObservableCollection<string> _eventLog;

    //    #endregion //Fields

    //    #region Properties



    //    #endregion //Properties

    //    #region Constructors

    //    public ConnectionController()
    //    {
    //        _sockets = new List<TransportType>();
    //        _selectedSocket = new TransportType();
    //        _eventLog = new ObservableCollection<string>();
    //        _sockets.Add(TransportType.WebSocket);
    //        _sockets.Add(TransportType.TcpSocket);
    //        _selectedSocket = TransportType.WebSocket;
    //    }

    //    #endregion //Constructors

    //    #region Methods

    //    public void Connect(string address, string port)
    //    {
    //        try
    //        {
    //            _transport = TransportFactory.Create((TransportType)_selectedSocket);
    //            //_transport.ConnectionStateChanged += OnConnectionStateChanged;
    //            //_transport.MessageReceived += OnMessageReceived;
    //            _transport.Connect(address, port);
    //        }
    //        catch (Exception ex)
    //        {
    //            _eventLog.Add(ex.Message);
    //        }
    //    }

    //    private void OnConnectionStateChanged(ConnectionStateChangedEventArgs e)
    //    {
    //        if (e.IsConnected)
    //        {

    //            if (e.ClientName)
    //            switch ()
    //            _eventLog.Add()
    //        }
    //    }


    //    #endregion //Methods
    //}
}
