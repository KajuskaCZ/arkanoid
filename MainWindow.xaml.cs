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

namespace Arkanoid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml   `
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameController _game;
        private GameTimer _gameTimer;
        private bool _inGame;

        public MainWindow()
        {
            InitializeComponent();

            _game = new GameController();

            _gameTimer = new GameTimer();
            _gameTimer.Timer.Tick += new EventHandler(DispatcherTimer_Tick);

            _inGame = false;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            int stopTimer = _game.PlayingTimer();

            switch (stopTimer)
            {
                case 0:
                    {
                        Content = _game.GameCanvasPanel.CanvasPanel;
                        break;
                    }
                case 1:
                    {
                        _gameTimer.Timer.Stop();
                        break;
                    }
                case 2:
                    {
                        _gameTimer.Timer.Stop();
                        _inGame = false;
                        break;
                    }
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            _game.NewGame();
            Content = _game.GameCanvasPanel.CanvasPanel;
            _inGame = true;
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MyMouseMove(object sender, MouseEventArgs e)
        {
            if (_inGame)
            {
                double x = e.GetPosition(_game.GameCanvasPanel.CanvasPanel).X;
                _game.GameMouseMove(x);
            }
        }

        private void MyMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_inGame && !_gameTimer.Timer.IsEnabled)
            {
                // Start timer;
                _gameTimer.Timer.Start();
            }
        }

        private void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N)
            {
                _gameTimer.Timer.Stop();

                _game.NewGame();
                Content = _game.GameCanvasPanel.CanvasPanel;
                _inGame = true;
            }
        }

    }
}
