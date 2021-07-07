using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Tanks
{
    enum Orientier
    {
        Up,
        Down,
        Left,
        Right

    }

    enum Intersection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    class GameLogic
    {
        List<BulletTask> bulletTasks = new List<BulletTask>();
        List<Controller> controllers;
        Random random;

        private Controller Player;
        private int playerscore = 0;
        public int PlayerScore => playerscore;
        public int PlayerHP => Player.Health;

        private int timerForNewEnemyOrientation = 0;


        PictureBox panel;

        public GameLogic(PictureBox panel, int cnt = 5)
        {
            this.panel = panel;

            random = new Random();
            controllers = new List<Controller>();

            CreateGame(cnt);
            Player = controllers[0];
        }

        /// <summary> создаёт игру </summary>
        /// <param name="cnt"> число игроков </param>
        public void CreateGame(int cnt)
        {
            controllers.Clear();
            for (int i = 0; i < cnt; i++)
            {
                controllers.Add(new Controller(new Point(random.Next(1, panel.Width - panel.Width / 100), random.Next(1, panel.Height - panel.Height / 100))));
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < controllers.Count; i++)
            {
                controllers[i].Draw(g, i==0);
            }

            for (int i = 0; i < bulletTasks.Count; i++)
            {
                bulletTasks[i].Draw(g);
            }
        }

        public void Update()
        { 
            for (int i = 0; i < bulletTasks.Count; i++)
            {
                BulletTask tbt = bulletTasks[i];
                bulletTasks[i] = tbt;
                if (tbt.Turn())
                {
                    bulletTasks.RemoveAt(i);
                    i--;
                }
                
            }

            for (int i = 0; i < controllers.Count; i++)
            {
                if (timerForNewEnemyOrientation % 50 == 0 && i != 0)
                {
                    ChangeOrientierEnemy(i);
                }
                if (i != 0 && timerForNewEnemyOrientation % 20 == 0) AddBulletTask(i);
                Move(i);

                for (int j = 0; j < bulletTasks.Count; j++)
                {
                    var btask = bulletTasks[j].LOC;
                    if (i >= 0 && controllers[i].Rectangle.Contains(btask) && controllers[i].ID != bulletTasks[j].SenderID) 
                    {
                        controllers[i].GetDamage(bulletTasks[j].Bullet.Damage);
                        
                        
                        if (!controllers[i].alive)
                        {
                            if (bulletTasks[j].SenderID == 0) playerscore++;

                            if (i == 0) GameOver();
                            controllers.RemoveAt(i);
                            i--;
                        }
                        bulletTasks.RemoveAt(j);
                        j--;
                    }
                }            
            }
            timerForNewEnemyOrientation++;
        }

        private void Move(int i)
        {
            switch (controllers[i].Orientier)
            {
                case Orientier.Up:
                    if (controllers[i].GetPoint().Y > 0) controllers[i].Move();
                    break;
                case Orientier.Left:
                    if (controllers[i].GetPoint().X > 0) controllers[i].Move();
                    break;
                case Orientier.Down:
                    if (controllers[i].GetPoint().Y < panel.Height - controllers[i].GetSize().Height) controllers[i].Move();
                    break;
                case Orientier.Right:
                    if (controllers[i].GetPoint().X < panel.Width - controllers[i].GetSize().Width) controllers[i].Move();
                    break;
            }
                
        }

        private void ChangeOrientierEnemy(int i)
        {
            Orientier or_tmp;
            switch (random.Next(5))
            {
                case 1: or_tmp = Orientier.Up; break;
                case 2: or_tmp = Orientier.Down; break;
                case 3: or_tmp = Orientier.Left; break;
                case 4: or_tmp = Orientier.Down; break;
                case 5: or_tmp = Orientier.Right; break;
                default: or_tmp = Orientier.Up; break;

            }
            controllers[i].ChangeOrientier(or_tmp);
        }

        public void ChangePlayerOrientier(Orientier tmp)
        {
            controllers[0].ChangeOrientier(tmp);
        }
        public void StartRun(int i)
        {
            controllers[i].TIsRunning = true;   
        }

        public void StopRun(int i)
        {
            controllers[i].TIsRunning = false;
        }

        public void AddBulletTask(int cont)
        {
            Bullet tmp = controllers[cont].StartShoot();
            Point tpoint = controllers[cont].GetPoint();
            int x, y, zx, zy;

            switch (controllers[cont].Orientier)
            {
                case Orientier.Up:
                    x = controllers[cont].GetPoint().X + controllers[cont].GetSize().Width / 2;
                    y = controllers[cont].GetPoint().Y;

                    zx = x;
                    zy = y - tmp.Far;
                    break;

                case Orientier.Left:
                    x = controllers[cont].GetPoint().X;
                    y = controllers[cont].GetPoint().Y + controllers[cont].GetSize().Height / 2;

                    zx = x - tmp.Far;
                    zy = y;
                    break;

                case Orientier.Down:
                    x = controllers[cont].GetPoint().X + controllers[cont].GetSize().Width / 2;
                    y = controllers[cont].GetPoint().Y + controllers[cont].GetSize().Height;

                    zx = x;
                    zy = y + tmp.Far;
                    break;

                case Orientier.Right:
                    x = controllers[cont].GetPoint().X + controllers[cont].GetSize().Width;
                    y = controllers[cont].GetPoint().Y + controllers[cont].GetSize().Height / 2;

                    zx = x + tmp.Far;
                    zy = y;
                    break;
                default:
                    x = y = zx = zy = 0;
                    break;
            }

            bulletTasks.Add(new BulletTask(controllers[cont].ID, tmp, new Point(zx, zy), new Point(x, y)));   
        }

        private bool RectIntersection(Rectangle rect1, Rectangle rect2)
        {
            return (Rectangle.Intersect(rect1, rect2) != null);
        }

        private void GameOver()
        {
            Application.Exit();
        }
    }
}
