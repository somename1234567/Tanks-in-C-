using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    
    class Tank
    {
        private int speed;
        public int Speed => speed;
        private int health;
        public int Health => health;
        private List<Bullet> bullets;
        private Armor armor;
        public static readonly int baseSpeed = 300;

        private bool alive = true;
        public bool Alive => alive;

        private Random random;
        
        public Tank(int type)
        {
            
            health = 200;
            bullets = new List<Bullet>();

            switch (type)
            {
                case 1:
                    armor = new Armor("Light");
                    break;

                case 2:
                    armor = new Armor("Medium");
                    break;

                case 3:
                    armor = new Armor("Hard");
                    break;
                default:
                    armor = new Armor("Medium");
                    break;
            }
            
            speed = baseSpeed - armor.Weight;

            random = new Random();
        }

        public int GetDamage(int damage)
        { 
            if (!alive) return 0;
            damage = armor.GetDamageForArmor(damage);
            health -= damage;

            if (health <= 0)
            {
                health = 0;
                alive = false;
            }

            CheckSpeed();

            return health;
        }

        private void CheckSpeed()
        {
            speed = baseSpeed - armor.Weight;
        }

        public Bullet GetBullet()
        {
            Bullet tmp;
            if (bullets.Count == 0)
            {
                ReCharge();
            }

            tmp = bullets[0];
            bullets.RemoveAt(0);

            return tmp;
        }

        private void ReCharge()
        {
            bullets.Clear();

            for (int i = 1; i < 10; i++)
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        bullets.Add(new Bullet(BulletType.FastLong));
                        break;

                    case 2:
                        bullets.Add(new Bullet(BulletType.SlowLong));
                        break;

                    case 3:
                        bullets.Add(new Bullet(BulletType.FastShort));
                        break;

                    case 4:
                        bullets.Add(new Bullet(BulletType.SlowShort));
                        break;
                }
            }
        }
    }
}
