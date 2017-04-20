using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Wall : IDrawable
    {
        public SizeF Size { get; private set; }
        public PointF Location { get; private set; }
        private Color color;
        private Pen pen;

        public Wall(float X, float Y, float width, float height)
        {
            Location = new PointF(X, Y);
            Size = new SizeF(width, height);
            color = Color.Blue;
            pen = new Pen(color);
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(pen.Brush, Location.X, Location.Y, Size.Width, Size.Height);
        }
    }
}
