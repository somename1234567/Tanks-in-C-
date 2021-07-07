using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    struct Armor
    {
        private int strength;
        private int weight;
        private string type;

        public int Strength => strength;
        public int Weight => weight;
        public string Type => type;

        public Armor(string type)
        {
            this.type = type;
            strength = 0;
            weight = 0;
            switch (type)
            {

                case "Light":
                    strength = 10;
                    weight = 10;
                    break;

                case "Medium":
                    strength = 50;
                    weight = 50;
                    break;

                case "Hard":
                    strength = 100;
                    weight = 100;
                    break;

                default:
                    break;
            }
        }

        public int GetDamageForArmor(int damage)
        {
            if (strength > (int)(damage * 0.6))
            {
                strength -= (int)(damage * 0.6);
                weight -= (int)(damage * 0.2);
                damage = (int)(damage * 0.4);
            }
            else
            {
                strength = 0;
                weight = 0;
            }
            return damage;
        }

    }
}
