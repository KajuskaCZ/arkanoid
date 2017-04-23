using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arkanoid
{
    public class Brick
    {
        private Rectangle _brickRect;
        public Rectangle BrickRect { get { return _brickRect; } set { _brickRect = value; } }

        private bool _unbreakable;
        public bool Unbreakable { get { return _unbreakable; } set { _unbreakable = value; } }

        public Brick(Color brickColor)
        {
            _brickRect = new Rectangle();
            _brickRect.Width = 40;
            _brickRect.Height = 13;

            _brickRect.Stroke = new SolidColorBrush(Colors.Black);
            _brickRect.StrokeThickness = 1;
            _brickRect.Fill = new SolidColorBrush(brickColor);
        }
    }
}
