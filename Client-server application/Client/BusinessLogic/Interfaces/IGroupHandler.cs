using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface IGroupHandler
    {
        #region Methods

        void CreateGroupRequest(string groupname, List<string> users);

        #endregion // Methods
    }
}
