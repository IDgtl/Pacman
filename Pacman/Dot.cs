using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Dot : IDrawable
    {
        public SizeF Size { get; private set; }
        public PointF Location { get; private set; }
        public bool isEaten;
        private Color color;
        private Pen pen;
        private List<Direction> directions;

        public Dot(PointF loc)
        {
            Size = new SizeF(10, 10);
            Location = loc;
            isEaten = false;
            color = Color.White;
            pen = new Pen(color);
            directions = new List<Direction>();
            this.checkDirections();
        }

        public void Draw(Graphics g)
        {
            if (!isEaten)
            {
                g.FillEllipse(pen.Brush, Location.X + 10, Location.Y + 10, 10, 10);
            }
        }

        private void checkDirections()
        {
            if (canMove(new PointF(Location.X + 30, Location.Y)))
            {
                directions.Add(Direction.Right);
            }
            if (canMove(new PointF(Location.X - 30, Location.Y)))
            {
                directions.Add(Direction.Left);
            }
            if (canMove(new PointF(Location.X, Location.Y + 30)))
            {
                directions.Add(Direction.Down);
            }
            if (canMove(new PointF(Location.X, Location.Y - 30)))
            {
                directions.Add(Direction.Up);
            }
        }

        private bool canMove(PointF newLocation)
        {
            foreach (var wall in BattleField.Walls)
            {
                if (overlapsWall(wall, newLocation))
                {
                    return false;
                }
            }
            return true;
        }

        private bool overlapsWall(Wall wall, PointF possibleLocation)
        {
            if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if (((possibleLocation.X + Size.Width) > wall.Location.X) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if (((possibleLocation.X + Size.Width) > wall.Location.X) && ((possibleLocation.Y + Size.Height) > wall.Location.Y)) return true;
            }
            else if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && ((possibleLocation.Y + Size.Height) > wall.Location.Y)) return true;
            }
            return false;
        }
    }
}
