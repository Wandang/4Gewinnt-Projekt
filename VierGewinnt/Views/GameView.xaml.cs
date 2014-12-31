using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VierGewinnt.Model;
using VierGewinnt.Logic.Controller;
using VierGewinnt.Utils.Logger;

namespace VierGewinnt.Views
{
    /// <summary>
    /// Interaktionslogik für GameView.xaml
    /// </summary>
    public partial class GameView : IView
    {
        readonly Button[,] _btns = new Button[7, 6];

        /// <summary>
        /// The Task that handles the GameLoop
        /// </summary>
        private Task _gameLoop;

        /// <summary>
        /// The Game Controller that runs the Game
        /// </summary>
        private GameController _game;

        /// <summary>
        /// Create a new Game View
        /// </summary>
        /// <param name="mw">MainWindow to attach to</param>
        /// <param name="p">Parent View</param>
        /// <param name="p1">Player 1</param>
        /// <param name="p2">Player 2</param>
        public GameView(MainWindow mw, IView p, IPlayer p1, IPlayer p2) :
            base(mw, false, p)
        {
            InitializeComponent();
            _gameLoop = new Task(GameLoop);

            _game = new GameController(p1, p2) {IsRunning = true};

            StartGame();
        }

        public void StartGame()
        {
            _gameLoop.Start();
        }

        public async void GameLoop()
        {
            while (_game.IsRunning)
            {
                _game.DoNext();
                Update();
            }

            if (_game.Winner != null)
            {
                await DisplayWinner();
            }
        }

        /// <summary>
        /// Uptdate our GameView
        /// </summary>
        public void Update()
        {
            for (var x = 0; x < 7; x++)
            {
                for (var y = 0; y < 6; y++)
                {
                    switch (_game.Field.Get(x, y))
                    {
                        case State.Player1:
                            SetBackground(_btns[x, y], Color.FromRgb(255, 0, 0));
                            break;
                        case State.Player2:
                            SetBackground(_btns[x, y], Color.FromRgb(0, 0, 255));
                            break;
                    }
                }
            }
        }

        public void SetBackground(Button btn, Color clr)
        {
            btn.Dispatcher.Invoke(() =>
            {
                btn.Background = new SolidColorBrush(clr);
            });
        }

        public async Task DisplayWinner()
        {
            await Task.Delay(1000);
        }

        /// <summary>
        /// Close this View
        /// </summary>
        /// <returns></returns>
        public override bool Close()
        {
            if (_game.IsRunning)
            {
                return
                    (MessageBox.Show("Wirklich Beenden (Spiel gillt als Verloren) ?", "Beenden", MessageBoxButton.YesNo) ==
                     MessageBoxResult.Yes);
            }
            else
            {
                return true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _game.SetColumn((int)((Button)sender).CommandParameter);
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (var x = 0; x < 7; x++)
            {
                for (var y = 0; y < 6; y++)
                {
                    _btns[x,y] = new Button
                    {
                        Width = 50, 
                        Height = 50, 
                        CommandParameter = x
                    };


                    _btns[x, y].Click += btn_Click;



                    Canv.Children.Add(_btns[x, y]);

                    Canvas.SetLeft(_btns[x, y], x * 50);
                    Canvas.SetTop(_btns[x, y], y * 50);
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked Col: " + (int)((Button)sender).CommandParameter);

            _game.SetColumn((int)(((Button)sender).CommandParameter));
        }
    }
}
