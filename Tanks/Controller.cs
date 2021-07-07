using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    
    class Controller
    {
        private static int static_id = 0;

        private Orientier orientier;
        public Orientier Orientier => orientier;
        private int id;
        public int ID => id;

        private Tank tank;
        private Point TLocation;
        private Size TSize;
        public bool alive => tank.Alive;
        public int Health => tank.Health;
        public bool TIsRunning { get; set; }
        private Random rand = new Random();

        public Rectangle Rectangle
        {
            get { return new Rectangle(new Point(TLocation.X, TLocation.Y), TSize); }
        }

        public static void ResetId()
        {
            static_id = 0;
        }

        public Point GetPoint()
        {
            return TLocation;
        }
        public Size GetSize()
        {
            return TSize;
        }

        public Controller(Point point)
        {
            TLocation = point;
            
            TSize = new Size(40, 40);
            tank = new Tank(rand.Next(1, 5));


            id = static_id;
            static_id++;
        }

        public Point Move()
        {
            int speedDelta = tank.Speed; 
            switch (orientier)
            {
                case Orientier.Up:
                    TLocation.Y -= (int)(speedDelta * 0.01);
                    break;

                case Orientier.Left:
                    TLocation.X -= (int)(speedDelta * 0.01);
                    break;

                case Orientier.Down:
                    TLocation.Y += (int)(speedDelta * 0.01);
                    break;

                case Orientier.Right:
                    TLocation.X += (int)(speedDelta * 0.01);
                    break;
                default:
                    break;
            }
            return TLocation;
        }

        public void ChangeOrientier(Orientier or)
        {
            orientier = or;
        }

        public int GetDamage (int damage)
        {
            return tank.GetDamage(damage);
        }

        public Image ImageTank(Image temp)
        {
            switch (orientier)
            {
                case Orientier.Up: break;
                case Orientier.Down: temp.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                case Orientier.Left: temp.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                case Orientier.Right: temp.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
            }

            return temp;
        }

        public void Draw(Graphics graphics, bool player)
        {
            if (player)
            {
                graphics.DrawImage(ImageTank(Tanks.Properties.Resources.heroImage_2), TLocation);
                return;
            }
            graphics.DrawImage(ImageTank(Tanks.Properties.Resources.heroImage), TLocation);
        }

        public Bullet StartShoot()
        {
            return tank.GetBullet();
        }
    }   
}
