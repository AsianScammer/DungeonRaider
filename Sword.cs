using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dungeon_Raider
{
    internal class Sword
    {
        //Hier heb ik de meeste private gemaakt omdat ik niet wil dat dit aantepassen is. Dit blijft mijn standaard template.
        private int normalAtk = 0;
        private int skillAtk = 0;
        private int burst = 0;
        private string swordName = "";
        private ConsoleColor color;
        Random random = new Random();
      
        // Hier geef ik alles mee door middel van een parameter. Dit is hoe je objecten maakt en wat ze allemaal moet hebben
        // om te creeeren
        public Sword (int _NormalAtk, int _SkillAtk, int _Burst, string _SwordName, ConsoleColor _Color)
        {
            normalAtk = _NormalAtk;
            skillAtk = _SkillAtk;
            burst = _Burst;
            swordName = _SwordName;
            color = _Color;
            
            
        }

        //hier geef ik een return type. Als je dit methode aanroept dan voert het dit stuk code uit.
        public int Atk1()
        {
            //(int) betekent dat ik alles wil afronden naar een heel getal. Anders komt er een float wat niet de bedoeling is.
            return random.Next((int)(normalAtk*0.8),(int)(normalAtk+1*1.2));
           
        }

        public int Atk2()
        {
            return random.Next((int)(skillAtk * 0.8), (int)(skillAtk + 1 * 1.2));
        }

       public int Atk3()
        {
            return random.Next((int)(burst * 0.8), (int)(burst + 1 * 1.2));
        }
       
       public string Name()
        {
            return swordName;
        }

        public ConsoleColor colors()
        {
            return color;
        }
        
     
    }
}
