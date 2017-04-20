using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pacman
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Stopped
    }

    public class BattleField
    {
        public static Bitmap Background { get; private set; }
        private Pacman pacman;
        private Ghost pinky;
        public static List<Wall> Walls { get; private set; }
        public static List<Dot> Dots { get; private set; }

        public BattleField(Bitmap background)
        {
            Background = background;
            pacman = new Pacman(new PointF(210, 210));
            pinky = new Ghost(new PointF(30, 30), Color.Pink);
            Walls = new List<Wall>();
            Dots = new List<Dot>();
            CreateInterior();
            CreateDots();
            //System.Windows.Forms.MessageBox.Show(Dots.Count.ToString());
        }

        public void DrawGraphic()
        {
            using (var g = Graphics.FromImage(Background))
            {
                this.DrawBackGround(g);
                this.DrawInterior(g);
                this.DrawDots(g);
                pacman.Draw(g);
                pinky.Draw(g);
            }
        }

        private void DrawBackGround(Graphics g)
        {
            g.Clear(Color.Black);
        }

        public void ChangePacmanDirection(Direction d)
        {
            pacman.ChangeDirection(d);
        }

        private void DrawInterior(Graphics g)
        {
            foreach(var wall in Walls)
            {
                wall.Draw(g);
            }
        }
        
        private void CreateInterior()
        {
            Walls.Add(new Wall(30, 0, 390, 30));
            Walls.Add(new Wall(90, 60, 60, 30));
            Walls.Add(new Wall(180, 60, 90, 30));
            Walls.Add(new Wall(300, 60, 60, 30));
            Walls.Add(new Wall(120, 120, 90, 30));
            Walls.Add(new Wall(240, 120, 90, 30));
            Walls.Add(new Wall(30, 180, 60, 30));
            Walls.Add(new Wall(150, 180, 60, 30));
            Walls.Add(new Wall(240, 180, 60, 30));
            Walls.Add(new Wall(360, 180, 60, 30));
            Walls.Add(new Wall(30, 240, 60, 30));
            Walls.Add(new Wall(150, 240, 150, 30));
            Walls.Add(new Wall(360, 240, 60, 30));
            Walls.Add(new Wall(120, 300, 90, 30));
            Walls.Add(new Wall(240, 300, 90, 30));
            Walls.Add(new Wall(90, 360, 60, 30));
            Walls.Add(new Wall(180, 360, 90, 30));
            Walls.Add(new Wall(300, 360, 60, 30));
            Walls.Add(new Wall(30, 420, 390, 30));

            Walls.Add(new Wall(0, 0, 30, 210));
            Walls.Add(new Wall(0, 240, 30, 210));
            Walls.Add(new Wall(60, 60, 30, 90));
            Walls.Add(new Wall(60, 300, 30, 90));
            Walls.Add(new Wall(120, 180, 30, 90));
            Walls.Add(new Wall(300, 180, 30, 90));
            Walls.Add(new Wall(360, 60, 30, 90));
            Walls.Add(new Wall(360, 300, 30, 90));
            Walls.Add(new Wall(420, 0, 30, 210));
            Walls.Add(new Wall(420, 240, 30, 210));
        }

        private void DrawDots(Graphics g)
        {
            foreach(var dot in Dots)
            {
                dot.Draw(g);
            }
        }

        private void CreateDots()
        {
            for (int i = 0; i <= Background.Width - 30; )
            {
                for (int j = 0; j <= Background.Height - 30; )
                {
                    PointF newLocation = new PointF(i, j);
                    bool canCreate = true;

                    foreach (var wall in Walls)
                    {
                        if (overlapsWall(wall, newLocation))
                        {
                            canCreate = false;
                            break;
                        }
                    }

                    if (canCreate)
                    {
                        Dots.Add(new Dot(newLocation));
                    }
                    j += 30;
                }
                i += 30;
            }
        }

        private bool overlapsWall(Wall wall, PointF possibleLocation)
        {
            if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y >= wall.Location.Y))
            {
                if (((possibleLocation.X + 30) > wall.Location.X) && (possibleLocation.Y < (wall.Location.Y + wall.Size.Height))) return true;
            }
            else if ((possibleLocation.X <= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if (((possibleLocation.X + 30) > wall.Location.X) && ((possibleLocation.Y + 30) > wall.Location.Y)) return true;
            }
            else if ((possibleLocation.X >= wall.Location.X) && (possibleLocation.Y <= wall.Location.Y))
            {
                if ((possibleLocation.X < (wall.Location.X + wall.Size.Width)) && ((possibleLocation.Y + 30) > wall.Location.Y)) return true;
            }
            return false;
        }
    }
}
