using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Arkanoid
{
    public class GameCanvas
    {
        private Canvas _canvasPanel;
        public Canvas CanvasPanel { get { return _canvasPanel; } set { _canvasPanel = value; } }

        private Paddle _paddle;
        public Paddle Paddle { get { return _paddle; } set { _paddle = value; } }
        private Ball _ball;
        public Ball Ball { get { return _ball; } set { _ball = value; } }
        private Brick[] _bricksLevelOne;
        public Brick[] BricksLevelOne { get { return _bricksLevelOne; } set { _bricksLevelOne = value; } }
        private Brick[] _bricksLevelTwo;
        public Brick[] BricksLevelTwo { get { return _bricksLevelTwo; } set { _bricksLevelTwo = value; } }

        private TextBlock _livesLabel;
        private TextBlock _livesLabelNumber;
        public TextBlock LivesLabelNumber { get { return _livesLabelNumber; } set { _livesLabelNumber = value; } }
        private TextBlock _newGameLabel;

        private TextBlock _gameOverLabel;
        private TextBlock _gameVictoryLabel;

        public GameCanvas()
        {
            _canvasPanel = new Canvas();
            _canvasPanel.Visibility = Visibility.Hidden;

            _livesLabel = new TextBlock();
            _livesLabelNumber = new TextBlock();
            _newGameLabel = new TextBlock();

            _gameOverLabel = new TextBlock();
            _gameVictoryLabel = new TextBlock();

            _ball = new Ball();
            _paddle = new Paddle();

            _bricksLevelOne = new Brick[30];
            for (int i = 0; i < 30; ++i)
            {
                _bricksLevelOne[i] = new Brick(Colors.Crimson);
                _bricksLevelOne[i].Unbreakable = false;
            }

            _bricksLevelTwo = new Brick[35];
            for (int i = 0; i < 35; ++i)
            {
                if (i == 2 || i == 5 || i == 9 || i == 31 || i == 32 || i == 33)
                {
                    _bricksLevelTwo[i] = new Brick(Colors.Black);
                    _bricksLevelTwo[i].Unbreakable = true;
                    continue;
                }
                _bricksLevelTwo[i] = new Brick(Colors.Crimson);
                _bricksLevelTwo[i].Unbreakable = false;
            }
        }

        // Create canvas for new level
        public void NewGameCanvas()
        {
            _canvasPanel.Children.Clear();

            _canvasPanel.Visibility = Visibility.Visible;

            _canvasPanel.Width = 500;
            _canvasPanel.Height = 300;
            _canvasPanel.Background = new SolidColorBrush(Colors.Pink);

            _canvasPanel.Cursor = System.Windows.Input.Cursors.None;
            setBallAndPaddle();
        }

        // Set ball and Paddle for level start
        public void setBallAndPaddle()
        {
            // Set paddle position in canvas
            Canvas.SetLeft(_paddle.PaddleRect, _paddle.ControllerPositionX);
            Canvas.SetTop(_paddle.PaddleRect, _paddle.ControllerPositionY);
            _canvasPanel.Children.Add(_paddle.PaddleRect);

            // Set ball position in canvas
            _ball.BallPositionX = 70;
            _ball.BallPositionY = 200;
            _ball.BallMovingPositionX = 2;
            _ball.BallMovingPositionY = 3;
            Canvas.SetLeft(_ball.BallRect, 70);
            Canvas.SetTop(_ball.BallRect, 200);
            _canvasPanel.Children.Add(_ball.BallRect);
        }

        // Bricks for level 1
        public void SetBricksLevelOne()
        {
            for (int i = 0; i < _bricksLevelOne.Count(); ++i)
            {
                _bricksLevelOne[i].BrickRect.Visibility = Visibility.Visible;
                Canvas.SetLeft(_bricksLevelOne[i].BrickRect, 115 + ((i % 5) * 57));
                Canvas.SetTop(_bricksLevelOne[i].BrickRect, 20 + (i / 5) * 23);
                _canvasPanel.Children.Add(_bricksLevelOne[i].BrickRect);
            }
        }

        // Bricks for level 2
        public void SetBricksLevelTwo()
        {
            for (int i = 0; i < _bricksLevelTwo.Count(); ++i)
            {
                _bricksLevelTwo[i].BrickRect.Visibility = Visibility.Visible;
                Canvas.SetLeft(_bricksLevelTwo[i].BrickRect, 115 + ((i % 5) * 57));
                Canvas.SetTop(_bricksLevelTwo[i].BrickRect, 20 + (i / 5) * 23);
                _canvasPanel.Children.Add(_bricksLevelTwo[i].BrickRect);
            }
        }

        // reset ball position after life lose
        public void setBallAfterLifeLose()
        {
            _ball.BallPositionX = 70;
            _ball.BallPositionY = 200;
            Canvas.SetLeft(_ball.BallRect, _ball.BallPositionX);
            Canvas.SetTop(_ball.BallRect, _ball.BallPositionY);
            _ball.BallMovingPositionX = 2;
        }

        // Set labels: lives and new game
        public void SetCanvasLabels(int lives)
        {
            _livesLabel.Text = "Lives:";
            _livesLabel.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(_livesLabel, 0);
            Canvas.SetTop(_livesLabel, 0);
            _canvasPanel.Children.Add(_livesLabel);

            _livesLabelNumber.Text = lives.ToString();
            _livesLabelNumber.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(_livesLabelNumber, 32);
            Canvas.SetTop(_livesLabelNumber, 0);
            _canvasPanel.Children.Add(_livesLabelNumber);

            _newGameLabel.Text = "'N' new game";
            _newGameLabel.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(_newGameLabel, 423);
            Canvas.SetTop(_newGameLabel, 0);
            _canvasPanel.Children.Add(_newGameLabel);
        }

        // Canvas appearance for game over
        public void GameOverCanvas()
        {
            _canvasPanel.Children.Clear();
            _canvasPanel.Cursor = System.Windows.Input.Cursors.Arrow;

            _gameOverLabel.Text = "GAME OVER";
            _gameOverLabel.Foreground = new SolidColorBrush(Colors.Black);
            _gameOverLabel.FontSize = 40;
            Canvas.SetLeft(_gameOverLabel, 140);
            Canvas.SetTop(_gameOverLabel, 100);
            _canvasPanel.Children.Add(_gameOverLabel);

            _newGameLabel.Text = "'N' new game";
            _newGameLabel.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(_newGameLabel, 423);
            Canvas.SetTop(_newGameLabel, 0);
            _canvasPanel.Children.Add(_newGameLabel);
        }

        // Canvas appearance for game victory
        public void GameVictoryCanvas()
        {
            _canvasPanel.Children.Clear();
            _canvasPanel.Cursor = System.Windows.Input.Cursors.Arrow;

            _gameVictoryLabel.Text = "VICTORY!";
            _gameVictoryLabel.Foreground = new SolidColorBrush(Colors.Black);
            _gameVictoryLabel.FontSize = 40;
            Canvas.SetLeft(_gameVictoryLabel, 160);
            Canvas.SetTop(_gameVictoryLabel, 100);
            _canvasPanel.Children.Add(_gameVictoryLabel);

            _newGameLabel.Text = "'N' new game";
            _newGameLabel.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(_newGameLabel, 423);
            Canvas.SetTop(_newGameLabel, 0);
            _canvasPanel.Children.Add(_newGameLabel);
        }
    }
}
