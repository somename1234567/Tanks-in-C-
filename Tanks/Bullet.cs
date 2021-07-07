using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    enum BulletType
    {
        FastLong,
        FastShort,
        SlowLong,
        SlowShort
    }
    class Bullet
    {
        private int speed;
        public int Speed => speed;
        
        private int far;
        public int Far => far;
        private int damage = 1;
        public int Damage => damage;
        
        
        private BulletType type;
        public BulletType Type => type;
        public bool IsRunning { get; set; }

        public Bullet(BulletType type)
        {
            this.type = type;
            switch (type)
            {
                case BulletType.FastLong:
                    speed = 5;
                    far = 700;
                    damage = 10;
                    break;

                case BulletType.FastShort:
                    speed = 6;
                    far = 400;
                    damage = 5;
                    break;

                case BulletType.SlowLong:
                    speed = 3;
                    far = 700;
                    damage = 20;
                    break;

                case BulletType.SlowShort:
                    speed = 2;
                    far = 200;
                    damage = 50;
                    break;
                
                default:
                    break;
                
            }

        }
    }
}