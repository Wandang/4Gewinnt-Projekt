using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using VierGewinnt.Model;
using VierGewinnt.Utils.Logger;

namespace VierGewinnt.Logic
{
    public class HumanPlayer : IPlayer
    {
        /// <summary>
        /// Name of this Human Player
        /// </summary>
        string _name;

        public HumanPlayer(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// Retourn the next Game
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int GetNext(ref bool input)
        {
            input = true;

            Logger.Debug("Going into Wait");

            lock (Controller.GameController.RoundLock)
            {
                Monitor.Wait(Controller.GameController.RoundLock);
            }
            
            Logger.Debug("Trying the Row the user clicked");

            return 0;
        }
    }
}
