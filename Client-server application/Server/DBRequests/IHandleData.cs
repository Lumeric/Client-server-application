using Server.RequestsToDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IHandleData
    {
        #region Methods

        ConcurrentDictionary<int, List<MessageInfo>> GetAllMessageFromChats();

        Task<bool> AddNewUser(UserInfo container);

        Task<int> CreateNewGroup(CreateGroupInfo container);

        Task<bool> RemoveChat(int groupNumber);

        #endregion //Methods
    }
}
