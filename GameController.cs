using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    public class GameController
    {
        private GameCanvas _gameCanvasPanel;
        public GameCanvas GameCanvasPanel { get { return _gameCanvasPanel; } set { _gameCanvasPanel = value; } }

        private int _lives;
        private int _score;
        private int _level; 

        public GameController()
        {
            _gameCanvasPanel = new GameCanvas();
        }

        // New game start
        public void NewGame()
        {
            _gameCanvasPanel.NewGameCanvas();
            _lives = 3;
            _score = 0;
            _level = 1;
            _gameCanvasPanel.SetBricksLevelOne();
            _gameCanvasPanel.SetCanvasLabels(_lives);
        }

        // If two rectangles intersect
        public static bool RectanglesIntersect(Rectangle shape1, Rectangle shape2)
        {
            Rect rect1 = new Rect(Canvas.GetLeft(shape1), Canvas.GetTop(shape1),
                shape1.Width, shape1.Height);
            Rect rect2 = new Rect(Canvas.GetLeft(shape2), Canvas.GetTop(shape2),
                shape2.Width, shape2.Height);

            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }

            return false;
        }

        // Timer for playing
        public int PlayingTimer()
        {
            // Intersect ball and paddle
            if (RectanglesIntersect(_gameCanvasPanel.Ball.BallRect, _gameCanvasPanel.Paddle.PaddleRect)) 
            {
                _gameCanvasPanel.Ball.BallMovingPositionY *= -1;
            }


            switch (_level)
            {
                case 1:
                    {
                        // Intersect ball and brick in level 1
                        for (int i = 0; i < 30; ++i)
                        {
                            if (RectanglesIntersect(_gameCanvasPanel.BricksLevelOne[i].BrickRect, _gameCanvasPanel.Ball.BallRect)
                                && _gameCanvasPanel.BricksLevelOne[i].BrickRect.Visibility != Visibility.Hidden)
                            {
                                // If ball hit side od brick
                                if (((Canvas.GetLeft(_gameCanvasPanel.BricksLevelOne[i].BrickRect) > Canvas.GetLeft(_gameCanvasPanel.Ball.BallRect))
                                    && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) < (Canvas.GetTop(_gameCanvasPanel.BricksLevelOne[i].BrickRect) + 10))
                                    && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) > (Canvas.GetTop(_gameCanvasPanel.BricksLevelOne[i].BrickRect) - 13)))
                                    || ((Canvas.GetLeft(_gameCanvasPanel.BricksLevelOne[i].BrickRect) + 40 <= Canvas.GetLeft(_gameCanvasPanel.Ball.BallRect))
                                     && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) < (Canvas.GetTop(_gameCanvasPanel.BricksLevelOne[i].BrickRect) + 10))
                                    && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) > (Canvas.GetTop(_gameCanvasPanel.BricksLevelOne[i].BrickRect) - 13))))
                                {
                                    _gameCanvasPanel.Ball.BallMovingPositionX *= -1;
                                } 
                                else 
                                {
                                    _gameCanvasPanel.Ball.BallMovingPositionY *= -1;
                                }

                                // Hide hit brick
                                _gameCanvasPanel.BricksLevelOne[i].BrickRect.Visibility = Visibility.Hidden;
                                ++_score;

                                // If max score, level end
                                if (_score == 30)
                                {
                                    _score = 0;
                                    ++_level;
                                    _gameCanvasPanel.NewGameCanvas();
                                    _gameCanvasPanel.SetCanvasLabels(_lives);
                                    _gameCanvasPanel.SetBricksLevelTwo();
                                    return 1;
                                }
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        // Intersect ball and brick in level 2
                        for (int i = 0; i < 35; ++i)
                        {
                            if (RectanglesIntersect(_gameCanvasPanel.BricksLevelTwo[i].BrickRect, _gameCanvasPanel.Ball.BallRect)
                                && _gameCanvasPanel.BricksLevelTwo[i].BrickRect.Visibility != Visibility.Hidden)
                            {
                                // If ball hit side od brick
                                if (((Canvas.GetLeft(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) > Canvas.GetLeft(_gameCanvasPanel.Ball.BallRect))
                                && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) < (Canvas.GetTop(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) + 10))
                                && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) > (Canvas.GetTop(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) - 13)))
                                || ((Canvas.GetLeft(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) + 40 < Canvas.GetLeft(_gameCanvasPanel.Ball.BallRect))
                                && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) < (Canvas.GetTop(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) + 10))
                                && (Canvas.GetTop(_gameCanvasPanel.Ball.BallRect) > (Canvas.GetTop(_gameCanvasPanel.BricksLevelTwo[i].BrickRect) - 13))))
                                {
                                    _gameCanvasPanel.Ball.BallMovingPositionX *= -1;
                                }
                                else
                                {
                                    _gameCanvasPanel.Ball.BallMovingPositionY *= -1;
                                }

                                if (_gameCanvasPanel.BricksLevelTwo[i].Unbreakable == false)
                                {
                                    // Hide hit brick
                                    _gameCanvasPanel.BricksLevelTwo[i].BrickRect.Visibility = Visibility.Hidden;
                                    ++_score;

                                    // If max score, level end
                                    if (_score == 29)
                                    {
                                        _gameCanvasPanel.GameVictoryCanvas();
                                        return 2;
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
            

            // Intersect ball and canvas borders
            if (_gameCanvasPanel.Ball.BallPositionY <= 0)
            {
                _gameCanvasPanel.Ball.BallMovingPositionY *= -1;
            }
            if (_gameCanvasPanel.Ball.BallPositionX + 10 >= 500 || _gameCanvasPanel.Ball.BallPositionX <= 0)
            {
                _gameCanvasPanel.Ball.BallMovingPositionX *= -1;
            }

            // Life lose 
            if (_gameCanvasPanel.Ball.BallPositionY + 10 >= 300)
            {
                _gameCanvasPanel.setBallAfterLifeLose();

                if (_lives > 0) 
                {
                    --_lives;
                    _gameCanvasPanel.LivesLabelNumber.Text = _lives.ToString();
                    return 1;
                }
                _gameCanvasPanel.GameOverCanvas();
                return 2;
            }

            _gameCanvasPanel.Ball.BallPositionX += _gameCanvasPanel.Ball.BallMovingPositionX;
            _gameCanvasPanel.Ball.BallPositionY += _gameCanvasPanel.Ball.BallMovingPositionY;
            Canvas.SetLeft(_gameCanvasPanel.Ball.BallRect, _gameCanvasPanel.Ball.BallPositionX);
            Canvas.SetTop(_gameCanvasPanel.Ball.BallRect, _gameCanvasPanel.Ball.BallPositionY);

            return 0;
        }

        // Control paddle by mouse
        public void GameMouseMove(double x)
        {
            if (x > 42 && x < (_gameCanvasPanel.CanvasPanel.Width - 42))
            {
                Canvas.SetLeft(_gameCanvasPanel.Paddle.PaddleRect, x - 42);
            }
        }

    }
}
