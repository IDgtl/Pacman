using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Ghost : CharacterBase
    {
        System.Drawing.Drawing2D.GraphicsPath gp;

        public Ghost(PointF loc, Color col) : base(loc, col)
        {
            this.CreateBody();
        }

        public override void Draw(Graphics g)
        {
            this.DrawBody(g);
        }

        private void CreateBody()
        {
            gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(location.X, location.Y, 30.0F, 30.0F, 180, 180);
            gp.AddLine(new PointF(location.X + 30.0F, location.Y + 15.0F), new PointF(location.X + 30.0F, location.Y + 30.0F));
            gp.AddLine(new PointF(location.X + 30.0F, location.Y + 30.0F), new PointF(location.X + 28.5F, location.Y + 30.0F));
            gp.AddArc(location.X + 16.5F, location.Y + 17.0F, 12.0F, 26.0F, 180, 180);
            gp.AddLine(new PointF(location.X + 16.5F, location.Y + 30.0F), new PointF(location.X + 13.5F, location.Y + 30.0F));
            gp.AddArc(location.X + 1.5F, location.Y + 17.0F, 12.0F, 26.0F, 180, 180);
            gp.AddLine(new PointF(location.X + 1.5F, location.Y + 30.0F), new PointF(location.X, location.Y + 30.0F));
            //gp.AddLine(new PointF(location.X, location.Y + 30.0F), new PointF(location.X, location.Y + 15.0F));
        }

        private void DrawBody(Graphics g)
        {
            g.FillPath(pen.Brush, gp);
            g.FillEllipse(Brushes.White, location.X + 6, location.Y + 3, 6, 10);
            g.FillEllipse(Brushes.Red, location.X + 7, location.Y + 6, 4, 4);
            g.FillEllipse(Brushes.White, location.X + 18, location.Y + 3, 6, 10);
            g.FillEllipse(Brushes.Red, location.X + 19, location.Y + 6, 4, 4);
        }
    }
}
