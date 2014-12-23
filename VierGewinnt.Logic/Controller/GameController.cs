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

        /// <summary>
        /// Lock Object (yes this is allowed)
        /// </summary>
        public static readonly object RoundLock = new object();

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
                Player = new[] { p1, p2 }
            };

            _game.CreateField(6,7);
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
                
                lock (RoundLock)
                {
                    _pressedRow = row;
                    Monitor.Pulse(RoundLock);
                }
            }
        }

        public async void DoNext()
        {
            _pressedRow = -1;

            lock (RoundLock)
            {
                while (_pressedRow == -1)
                {
                    Logger.Debug("GameController", "Getting next Players Round");
                    _pressedRow = _game.Player[_round % 2].GetNext(ref _needsInput);

                    _needsInput = false; //Reset input so we don't forget this

                    if (!IsValid(_pressedRow))
                    {
                        _pressedRow = -1;
                    }
                    else
                    {
                        _game.DoTurn(_pressedRow, _round % 2);
                    }
                }

                Logger.Debug("Leaving Lock Loop");
            }

            _round++;
        }

        public void InitGame()
        {
            _game.CreateField(6, 7);
        }

        private bool IsValid(int row)
        {
            return _game.Field.HasEmpty(row);
        }
    }
}
