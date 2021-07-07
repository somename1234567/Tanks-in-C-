using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    class BulletTask
    {
        private int senderid;
        public int SenderID => senderid;
        private Bullet bullet;
        public Bullet Bullet => bullet;
        private Point Location;
        public Point LOC => Location;
        private Point Destination;
        private Point BeginL;
        public BulletTask(int asenderid, Bullet bullet, Point bdest, Point beg)
        {
            senderid = asenderid;
            this.bullet = bullet;
            Destination = bdest;
            Location = beg;
            BeginL = beg;
        }

        public bool Turn()
        {
            bool finished = false;
            float ax = Math.Abs(Destination.X - Location.X);
            float ay = Math.Abs(Destination.Y - Location.Y);

            int sx = Math.Sign(Destination.X - Location.X);
            int sy = Math.Sign(Destination.Y - Location.Y);

            int dy = (int)(ay * bullet.Speed / (ax + ay));
            int dx = bullet.Speed - dy;

            Location.X += dx * sx;
            Location.Y += dy * sy;

            if (Math.Sqrt(Math.Pow((double)Location.X-BeginL.X, 2) + Math.Pow((double)Location.Y-BeginL.Y, 2)) >= bullet.Far) finished = true;
            return finished;
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(Tanks.Properties.Resources.bulletImage, Location);
        }
    }
}
