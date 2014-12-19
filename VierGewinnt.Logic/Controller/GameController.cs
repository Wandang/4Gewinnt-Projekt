using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using VierGewinnt.Model;
using VierGewinnt.Utils.Logger;

namespace VierGewinnt.Logic.Controller
{
    /// <summary>
    /// Game controller
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// The Game Model used by this Game Controller
        /// </summary>
        Game _game;

        /// <summary>
        /// The Row that the "User" wants to Click
        /// </summary>
        int _pressedRow = -1;

        /// <summary>
        /// Lock for Rounds
        /// </summary>
        public readonly object _roundLock = new object();

        public bool IsRunning { get; set;}

        /// <summary>
        /// Winner of the Game
        /// </summary>
        public IPlayer Winner { get; set; }

        /// <summary>
        /// New Game Controller
        /// </summary>
        public GameController(IPlayer p1, IPlayer p2)
        {
            _game = new Game()
            {
                Player1 = p1,
                Player2 = p2
            };
        }

        public void SetRow(int row)
        {
            lock (_roundLock)
            {
                _pressedRow = row;
                Monitor.Pulse(_roundLock);
            }
        }

        public async void DoNext()
        {
            lock (_roundLock)
            {
                while (_pressedRow == -1)
                {
                    Logger.Debug("GameController", "Task is going into Wait");
                    Monitor.Wait(_roundLock);

                    _pressedRow = -1;
                }
            }
        }

        public void InitGame()
        {
            _game.CreateField(8, 7);
        }
    }
}
