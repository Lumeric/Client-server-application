using Common.Network;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class OpenGroupEvent : PubSubEvent<ObservableCollection<User>>
    {
    }
}
