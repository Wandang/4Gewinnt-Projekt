using System;
using System.CodeDom;
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
        int _round;

        private Task _gameLoop;

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

        #region Events
        public delegate void NeedUiUpdate(object sender);

        public event NeedUiUpdate OnNeedUiUpdate;

        public delegate void PlayerWon(object sender, PlayerWonEventArgs args);

        public event PlayerWon OnPlayerWon;
        #endregion

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
        
        /// <summary>
        /// Start the GameLoop and so the Game
        /// </summary>
        public void Start()
        {
            _gameLoop.Start();
        }

        /// <summary>
        /// Gameloop representing a game Round
        /// </summary>
        public void GameLoop()
        {
            while (IsRunning)
            {
                DoNext();
                Update();
            }

            if (Winner != null)
            {
                var handler = OnPlayerWon;
                if (handler != null)
                {
                    handler(this, new PlayerWonEventArgs {Round =  _round, Winner = _game.Player[_round%2]});
                }
            }
        }

        /// <summary>
        /// Set if we need input or not
        /// </summary>
        /// <param name="need"></param>
        public void NeedInput(bool need)
        {
            _needsInput = need;
        }

        /// <summary>
        /// If the GameControler needs input we can set our input with this Method
        /// </summary>
        /// <param name="row"></param>
        public void SetColumn(int row)
        {
            Logger.Debug("Trying: " + row);

            if (!_needsInput) return; // Break out if we don't wait for Input
            
            lock (RoundLock)
            {
                _pressedColumn = row;
                _needsInput = false;
                Monitor.Pulse(RoundLock);
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
                    _game.Player[_round%2].GetNext(ref _needsInput, ref _pressedColumn);

                    if (IsValid(_pressedColumn))
                    {
                        var y = _game.DoTurn(_pressedColumn, _round%2);
                        var x = _pressedColumn;

                        if (GotWinner(x, y))
                        {
                            IsRunning = false;
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
            var tmpCount = 0;
            var player = Field.Get(x, y);

            #region Vertical

            for (var i = 0; i < Field.Height; i++)
            {
                if (Field.Get(x, i) == player)
                {
                    tmpCount++;
                    if (tmpCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    tmpCount = 0;
                }
            }

            #endregion

            #region Horizontal

            tmpCount = 0;
            for (var i = 0; i < Field.Width; i++)
            {

                if (Field.Get(i, y) == player)
                {
                    tmpCount++;
                    if (tmpCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    tmpCount = 0;
                }

            }
            #endregion

            #region Top Left -> Bottom Right
            tmpCount = 0;
            int tX, tY;
            if (y - x > 0)
            {
                tX = 0;
                tY = y - x;
            }
            else
            {
                tX = x - y;
                tY = 0;
            }

            for (var i = 0; tX + i < Field.Width && tY + i < Field.Height; i++)
            {

                if (Field.Get(tX +i, tY + i) == player)
                {
                    tmpCount++;
                    if (tmpCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    tmpCount = 0;
                }
            }
            #endregion

            #region Top Right -> Bottom Left
            tmpCount = 0;
            if (x + y < Field.Width)
            {
                tX = x + y;
                tY = 0;
            }
            else
            {
                tX = Field.Width - 1;
                tY = y - (Field.Width - 1 - x);
            }

            for (var i = 0; tY + i < Field.Height && tX - i > 0; i++)
            {
                if (Field.Get(tX - i, tY + i) == player)
                {
                    tmpCount++;
                    if (tmpCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    tmpCount = 0;
                }
            }
            #endregion
            return false;
        }

        public void InitGame()
        {
            Winner = null;
            _gameLoop = new Task(GameLoop);
            IsRunning = true;
            _game.CreateField(6, 7);
            _round = 0;

           Update();
        }

        private bool IsValid(int col)
        {
            return Field.HasEmpty(col);
        }

        private void Update()
        {
            var handler = OnNeedUiUpdate;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}
