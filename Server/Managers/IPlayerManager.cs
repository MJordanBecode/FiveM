using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lostgen.Server.Interfaces
{
    public interface IPlayerManager
    {
        void AddPlayer(int serverId, string license);
        void RemovePlayer(int serverId);
        bool IsPlayerConnected(int serverId);
    }
}
