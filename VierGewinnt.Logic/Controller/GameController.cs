using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int _pressedColumn = -1;

        /// <summary>
        /// Can be set by the Player when User Input is needed for the next turn
        /// </summary>
        bool _needsInput = false;

        public Field Field { get { return _game.Field; } }

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

        public void SetColumn(int row)
        {
            Logger.Debug("Trying: " + row);

            if (_needsInput)
            {
                
                lock (RoundLock)
                {
                    _pressedColumn = row;
                    Monitor.Pulse(RoundLock);
                }
            }
        }

        public async void DoNext()
        {
            _pressedColumn = -1;

            lock (RoundLock)
            {
                while (_pressedColumn == -1)
                {
                    Logger.Debug("GameController", "Getting next Players Round");
                    _game.Player[_round % 2].GetNext(ref _needsInput, ref _pressedColumn);

                    Logger.Log("Trying: " + _pressedColumn);

                    _needsInput = false; //Reset input so we don't forget this

                    if (IsValid(_pressedColumn))
                    {
                        int y = _game.DoTurn(_pressedColumn, _round%2);
                        int x = _pressedColumn;

                        if (GotWinner(x, y))
                        {
                            Winner = _game.Player[_round%2];
                            Logger.Log("WE GOT A WINNER!");
                        }
                    }
                    else
                    {
                        _pressedColumn = -1;
                    }
                }

                Logger.Debug("Leaving Lock Loop");
            }

            _round++;
        }

        /// <summary>
        /// Checks if the last Turn made one player win
        /// </summary>
        /// <param name="x">X Coord of the last Turn</param>
        /// <param name="y">Y Coord of the last Turn</param>
        /// <returns></returns>
        private bool GotWinner(int x, int y)
        {
            Logger.Log("X:" + x + " --- Y:" + y);

            bool winner = false;
            int tmpCount = 0;
            State player = Field.Get(x, y);
            
            for (int i = 0; i < Field.Height; i++)
            {
                if(Field.Get(i, x) == player) {
                    tmpCount++;
                    if(tmpCount == 4) {
                        winner = true;
                        break;
                    }
                } else {
                    tmpCount = 0;
                }
            }

            return winner;
        }

        public void InitGame()
        {
            _game.CreateField(6, 7);
        }

        private bool IsValid(int col)
        {
            return Field.HasEmpty(col);
        }
    }
}
