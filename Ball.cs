using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    public class Ball
    {
        private int _ballPositionX, _ballPositionY;
        public int BallPositionX { get { return _ballPositionX; } set { _ballPositionX = value; } }
        public int BallPositionY { get { return _ballPositionY; } set { _ballPositionY = value; } }

        private int _ballMovingPositionX, _ballMovingPositionY;
        public int BallMovingPositionX { get { return _ballMovingPositionX; } set { _ballMovingPositionX = value; } }
        public int BallMovingPositionY { get { return _ballMovingPositionY; } set { _ballMovingPositionY = value; } }

        private Rectangle _ballRect;
        public Rectangle BallRect { get { return _ballRect; } set { _ballRect = value; } }

        public Ball()
        {
            _ballPositionX = 70;
            _ballPositionY = 200;
            _ballMovingPositionX = 2;
            _ballMovingPositionY = 3;

            // Create ball
            _ballRect = new Rectangle();
            _ballRect.Width = 10;
            _ballRect.Height = 10;

            _ballRect.Stroke = new SolidColorBrush(Colors.Black);
            _ballRect.StrokeThickness = 1;
            _ballRect.Fill = new SolidColorBrush(Colors.DeepPink);
        }
    }
}
