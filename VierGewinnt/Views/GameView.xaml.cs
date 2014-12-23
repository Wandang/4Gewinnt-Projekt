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
        /// <summary>
        /// The Task that handles the GameLoop
        /// </summary>
        private Task gameLoop;

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
            gameLoop = new Task(GameLoop);

            _game = new GameController(p1, p2);
            _game.IsRunning = true;

            StartGame();
        }

        public void StartGame()
        {
            gameLoop.Start();
        }

        public async void GameLoop()
        {
            while (_game.IsRunning)
            {
                _game.DoNext();
            }

            if (_game.Winner != null)
            {
                await DisplayWinner();
            }
        }

        public async Task DisplayWinner()
        {
            System.Threading.Thread.Sleep(1000);
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
            _game.SetRow(0);
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    Button btn = new Button
                    {
                        Width = 50, 
                        Height = 50, 
                        CommandParameter = x
                    };


                    btn.Click += btn_Click;

                    Canv.Children.Add(btn);

                    Canvas.SetLeft(btn, x*50);
                    Canvas.SetTop(btn, y*50);
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            _game.SetRow((int)(((Button)sender).CommandParameter));
        }
    }
}
