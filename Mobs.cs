using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Raider
{
    internal class Mobs
    {
        private int hp = 20;
        private int normalAtk = 3;
        private string name = "";
        Random random = new Random();
       public Mobs(int _Hp, int _NormalAtk, string _Name)
        {
            hp = _Hp;
            normalAtk = _NormalAtk;
            name = _Name;
        }

        public int Atk1()
        {
            return random.Next((int)(normalAtk * 0.8), (int)(normalAtk + 1 * 1.2));
        }

        public int MobHp ()
        {
            return hp;
        }

        public string MobName()
        {
            return name;
        }

    }
}
