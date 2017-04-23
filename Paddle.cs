using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    public class Paddle
    {
        private int _controllerPositionX, _controllerPositionY;
        public int ControllerPositionX { get { return _controllerPositionX; } set { _controllerPositionX = value; } }
        public int ControllerPositionY { get { return _controllerPositionY; } set { _controllerPositionY = value; } }

        private Rectangle _paddleRect;
        public Rectangle PaddleRect { get { return _paddleRect; } set { _paddleRect = value; } }
        

        public Paddle()
        {
            _controllerPositionX = 120;
            _controllerPositionY = 260;

            // Create ball controller
            _paddleRect = new Rectangle();
            _paddleRect.Width = 85;
            _paddleRect.Height = 12;

            _paddleRect.RadiusX = 5;
            _paddleRect.RadiusY = 5;

            _paddleRect.Stroke = new SolidColorBrush(Colors.Black);
            _paddleRect.StrokeThickness = 1;
            _paddleRect.Fill = new SolidColorBrush(Colors.HotPink);
        }
    }
}
