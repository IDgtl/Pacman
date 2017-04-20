using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pacman
{
    public class Pacman : CharacterBase
    {
        private float mouthDisplacement = 0.0F;
        private float displacementPerFrame = 250.0F / GameWindow.FPS;
        private float startAngle = 45.0F;
        private bool mouthClosing = true;

        public Pacman(PointF loc) : base(loc, Color.Yellow) { }

        public void ChangeDirection(Direction d)
        {
            this.newDirection = d;
        }

        public override void Draw(Graphics g)
        {
            if (this.location.X < 0)
            {
                if (this.location.X > (-1) * this.size.Width)
                {
                    g.FillPie(this.pen.Brush, this.location.X + BattleField.Background.Width, this.location.Y, this.size.Width, this.size.Height, this.startAngle - this.mouthDisplacement, 270 + 2 * this.mouthDisplacement);
                }
                else
                {
                    this.location = new PointF((BattleField.Background.Width - this.size.Width), location.Y);
                }
            }
            if ((this.location.X + this.size.Width) > BattleField.Background.Width)
            {
                if ((this.location.X + this.size.Width) < (BattleField.Background.Width + this.size.Width))
                {
                    g.FillPie(this.pen.Brush, this.location.X - BattleField.Background.Width, this.location.Y, this.size.Width, this.size.Height, this.startAngle - this.mouthDisplacement, 270 + 2 * this.mouthDisplacement);
                }
                else
                {
                    this.location = new PointF(0.0F, location.Y);
                }
            }
            g.FillPie(this.pen.Brush, this.location.X, this.location.Y, this.size.Width, this.size.Height, this.startAngle - this.mouthDisplacement, 270 + 2 * this.mouthDisplacement);

            this.TryToMove();

            if (this.mouthClosing && this.mouthDisplacement < 45.0F)
            {
                this.mouthDisplacement += this.displacementPerFrame;
                if (this.mouthDisplacement >= 45.0F)
                {
                    this.mouthClosing = false;
                }
            }
            else
            {
                this.mouthDisplacement -= this.displacementPerFrame;
                if (this.mouthDisplacement <= 0.0F)
                {
                    this.mouthClosing = true;
                }
            }
        }

        private void TryToMove()
        {
            if (!hasBarrier(newDirection, speed))
            {
                direction = newDirection;
                this.Move(speed);
            }
            else
            {
                for (int i = speed - 1; i > 0; i--)
                {
                    if (!hasBarrier(newDirection, i))
                    {
                        this.Move(i);
                        direction = newDirection;
                        return;
                    }
                }
                if (!hasBarrier(direction, speed))
                {
                    this.Move(speed);
                }
                else
                {
                    for (int i = speed - 1; i > 0; i--)
                    {
                        if (!hasBarrier(direction, i))
                        {
                            this.Move(i);
                            direction = newDirection;
                            return;
                        }
                    }
                    direction = Direction.Stopped;
                }
            }

            //Log(location.X.ToString() + "  " + location.Y.ToString());
        }

        private void Move(int distance)
        {
            if (this.direction == Direction.Right)
            {
                this.startAngle = 45.0F;
                this.location = new PointF(this.location.X + distance, this.location.Y);
            }
            else if (this.direction == Direction.Left)
            {
                this.startAngle = 225.0F;
                this.location = new PointF(this.location.X - distance, this.location.Y);
            }
            else if (this.direction == Direction.Up)
            {
                this.startAngle = 315.0F;
                this.location = new PointF(this.location.X, this.location.Y - distance);
            }
            else if (this.direction == Direction.Down)
            {
                this.startAngle = 135.0F;
                this.location = new PointF(this.location.X, this.location.Y + distance);
            }
            //if (this.direction == Direction.Right)
            //{
            //    if ((this.location.X + this.size.Width) >= BattleField.Background.Width || hasBarrier(direction, this.location))
            //    {
            //        this.direction = Direction.Stopped;
            //        return;
            //    }
            //    this.startAngle = 45.0F;
            //    this.location = new PointF(this.location.X + this.speed, this.location.Y);
            //}
            //else if (this.direction == Direction.Left)
            //{
            //    if (this.location.X <= 0.0F || hasBarrier(direction, this.location))
            //    {
            //        this.direction = Direction.Stopped;
            //        return;
            //    }
            //    this.startAngle = 225.0F;
            //    this.location = new PointF(this.location.X - this.speed, this.location.Y);
            //}
            //else if (this.direction == Direction.Up)
            //{
            //    if (this.location.Y <= 0.0F || hasBarrier(direction, this.location))
            //    {
            //        this.direction = Direction.Stopped;
            //        return;
            //    }
            //    this.startAngle = 315.0F;
            //    this.location = new PointF(this.location.X, this.location.Y - this.speed);
            //}
            //else if (this.direction == Direction.Down)
            //{
            //    if ((this.location.Y + this.size.Height) >= BattleField.Background.Height || hasBarrier(direction, this.location))
            //    {
            //        this.direction = Direction.Stopped;
            //        return;
            //    }
            //    this.startAngle = 135.0F;
            //    this.location = new PointF(this.location.X, this.location.Y + this.speed);
            //}
        }

        private bool hasBarrier(Direction d, int dist)
        {
            foreach (var wall in BattleField.Walls)
            {
                switch (d)
                {
                    case Direction.Right:
                        if (overlapsWall(wall, new PointF(location.X + dist, location.Y))) return true;
                        break;
                    case Direction.Left:
                        if (overlapsWall(wall, new PointF(location.X - dist, location.Y))) return true;
                        break;
                    case Direction.Up:
                        if (overlapsWall(wall, new PointF(location.X, location.Y - dist))) return true;
                        break;
                    case Direction.Down:
                        if (overlapsWall(wall, new PointF(location.X, location.Y + dist))) return true;
                        break;
                }
            }
            return false;
        }

        private bool overlapsWall(Wall wall, PointF possibleLocation)
        {
            if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if (((possibleLocation.X + size.Width) > wall.Location.X) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if (((possibleLocation.X + size.Width) > wall.Location.X) && ((possibleLocation.Y + size.Height) > wall.Location.Y)) return true;
            }
            else if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && ((possibleLocation.Y + size.Height) > wall.Location.Y)) return true;
            }
            return false;
        }

        private void Log(string line)
        {
            using (System.IO.StreamWriter wr = System.IO.File.AppendText("Log.txt"))
            {
                wr.WriteLine(line);
            }
        }
    }
}
