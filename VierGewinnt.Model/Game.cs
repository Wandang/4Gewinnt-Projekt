using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Game
    {
        /// <summary>
        /// Player One
        /// </summary>
        public IPlayer[] Player { get; set; }

        /// <summary>
        /// Field of the Game
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// Create new Field for this Game
        /// </summary>
        /// <param name="height">Height of the Game Field</param>
        /// <param name="width">Width of the Game Field</param>
        public void CreateField(int height, int width)
        {
            Field = new Field(width, height);
        }

        public int DoTurn(int col, int player)
        {
            return Field.Set(col, (State)(player+1));
        }
    }
}
