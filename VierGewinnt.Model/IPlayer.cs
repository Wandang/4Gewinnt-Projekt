using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public interface IPlayer
    {
        /// <summary>
        /// Get Name of the Player
        /// </summary>
        /// <returns>Player</returns>
        string GetName();

        /// <summary>
        /// Get the Next Row of the Player
        /// </summary>
        /// <param name="input">can be set if the Turn needs user input</param>
        /// <returns>Row</returns>
        void GetNext(ref bool input, ref int column);
    }
}
