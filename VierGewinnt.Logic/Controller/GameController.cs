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
        /// Current Round
        /// </summary>
        int _round = 0;

        /// <summary>
        /// The Row that the "User" wants to Click
        /// </summary>
        int _pressedRow = -1;

        /// <summary>
        /// Can be set by the Player when User Input is needed for the next turn
        /// </summary>
        bool _needsInput = false;

        public static readonly object _roundLock = new object();

        /// <summary>
        /// Representing the State of the game
        /// </summary>
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
                Player = new IPlayer[] { p1, p2 }
            };
        }

        public void NeedInput(bool need)
        {
            _needsInput = need;
        }

        public void SetRow(int row)
        {
            Logger.Debug("Trying: " + row);

            if (_needsInput)
            {
                
                lock (_roundLock)
                {
                    _pressedRow = row;
                    Monitor.Pulse(_roundLock);
                }
            }
        }

        public async void DoNext()
        {
            lock (_roundLock)
            {
                while (_pressedRow == -1)
                {
                    Logger.Debug("GameController", "Getting next Players Round");
                    _game.Player[_round % 2].GetNext(ref _needsInput);

                    _needsInput = false; //Reset input so we don't forget this
                }
            }
        }

        public void InitGame()
        {
            _game.CreateField(8, 7);
        }
    }
}
