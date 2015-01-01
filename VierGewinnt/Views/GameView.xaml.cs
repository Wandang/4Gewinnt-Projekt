using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
            _game = new GameController(p1, p2) {IsRunning = true};
            _game.OnNeedUiUpdate += Update;
            _game.OnPlayerWon += _game_OnPlayerWon;

            _game.Start();
        }

        async void _game_OnPlayerWon(object sender, PlayerWonEventArgs args)
        {
            await DisplayWinner(args.Winner, args.Round);
        }

        /// <summary>
        /// Uptdate our GameView
        /// </summary>
        public void Update(object sender)
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
                            SetBackground(_btns[x, y], Colors.Yellow);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Change background Color of the Given Buttons
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="clr"></param>
        public void SetBackground(Button btn, Color clr)
        {
            btn.Dispatcher.Invoke(() =>
            {
                btn.Background = new SolidColorBrush(clr);
            });
        }

        /// <summary>
        /// Display the winner
        /// </summary>
        /// <returns></returns>
        public async Task DisplayWinner(IPlayer player, int round)
        {
            Debug.WriteLine("WINNER WINNER CHICKEN DINNER!");
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

        /// <summary>
        /// Create the Buttons we need for the Game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handle Button clicks and give it to our game Controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            _game.SetColumn((int)(((Button)sender).CommandParameter));
        }

        /// <summary>
        /// Handle Escape so we an open our Game Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Escape) return;
            
            System.Diagnostics.Debug.WriteLine("GOGO!!");

            e.Handled = true;
            ViewHost.Overlay(new MenuView(this.ViewHost, true, this));
        }
    }
}
