using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Raider
{
    internal class Armor
    {
     
        private int ArmorHp = 0;
        private string ArmorName = "";
        private ConsoleColor Color;
        public Armor(int _ArmorHp, string _ArmorName, ConsoleColor _Color)
        {
            ArmorHp = _ArmorHp;
            ArmorName = _ArmorName;
            Color = _Color;
        }

        public int AHp ()
        {
            return ArmorHp;
        }

        public string AName()
        {
            return ArmorName;
        }

        public ConsoleColor ArmorColor()
        {
            return Color;
        }


    }
}
