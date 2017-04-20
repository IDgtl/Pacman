using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class CharacterBase : IDrawable
    {
        protected SizeF size;
        protected PointF location;
        protected Color color;
        protected Pen pen;
        protected Direction direction;
        protected Direction newDirection;
        protected int speed;

        public CharacterBase()
        {
            size = new SizeF(30.0F, 30.0F);
            direction = Direction.Stopped;
            newDirection = Direction.Stopped;
            speed = (int)200.0F / GameWindow.FPS;
        }

        public CharacterBase(PointF loc, Color col) : this()
        {
            location = loc;
            color = col;
            pen = new Pen(color);
        }

        public abstract void Draw(Graphics g);
    }
}
