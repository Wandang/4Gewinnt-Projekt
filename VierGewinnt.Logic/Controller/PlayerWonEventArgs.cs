using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VierGewinnt.Model;

namespace VierGewinnt.Logic.Controller
{
    public class PlayerWonEventArgs : EventArgs
    {
        public IPlayer Winner;

        public int Round;
    }
}
