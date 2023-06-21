using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Raider
{
    internal class Boss
    {
        private int bossHp;
        private int bossAtk1;
        private int bossAtk2;
        private int bossBurst;
        private string name;
        ConsoleColor color;
        Random random = new Random();
        public Boss (int _BossHp, int _BossAtk1, int _BossAtk2, int _BossBurst, string _Name,ConsoleColor _Color)
        {
            bossHp = _BossHp;
            bossAtk1 = _BossAtk1;
            bossAtk2 = _BossAtk2;
            bossBurst = _BossBurst;
            name = _Name;
            color = _Color;
        }

        public int Hp ()
        {
            return bossHp;
        }

        public int Atk1()
        {
            return random.Next((int)(bossAtk1 * 0.8), (int)(bossAtk1 + 1 * 1.2));
        }

        public int Atk2()
        {
            return random.Next((int)(bossAtk2 * 0.8), (int)(bossAtk2 + 1 * 1.2));
        }

        public int Burst()
        {
            return random.Next((int)(bossBurst * 0.8), (int)(bossBurst + 1 * 1.2));
        }

        public string BossName()
        {
            return name;
        }

        public ConsoleColor bossColor()
        {
            return color;
        }
        
    }


}
