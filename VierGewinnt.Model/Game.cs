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
        /// <param name="Height">Height of the Game Field</param>
        /// <param name="Width">Width of the Game Field</param>
        public void CreateField(int Height, int Width)
        {
            Field = new Field(Width, Height);
        }

        public int DoTurn(int col, int player)
        {
            return Field.Set(col, (State)(player+1));
        }
    }
}
