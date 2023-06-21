using System.Drawing;
using System.Linq;
using System.Security.Cryptography;

namespace Dungeon_Raider
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.Start();
            //Hier zorg ik ervoor dat de spel pas stopt als de spelers gezondheid lager is dan 0
            while (p.playerHp > 0)
            {
                
                //Hier roep ik de methode's op die ik heb gemaakt
                p.StartScreen();
                p.WeaponInventory();
                p.IndexMobs();
                p.ArmorInventory();
                p.Potoo();
                p.DualWield();
                p.Guide();
                p.MobFloorOne();
                p.MobFloorTwo();
                p.MobFloorThree();
                p.BossFloorOne();
                p.BossFloorTwo();
                p.BossFloorThree();
                p.Fontain();
                p.Status();
                p.End();
            }
        }


        // dit zijn allerlei nieuwe objecten die ik heb aangemaakt.
        //Swords) atk1,atk2, burst, naam, rarity color, value
        Sword dullBlade = new Sword(5, 8, 13, "Dull Blade", ConsoleColor.Gray);
        Sword steelSword = new Sword(9, 14, 20, "Steel Sword", ConsoleColor.Green);
        Sword forgedDwarfBlade = new Sword(15, 22, 36, "Forged Dwarf Blade", ConsoleColor.Blue);
        Sword soulEater = new Sword(50, 80, 150, "Soul Eater", ConsoleColor.DarkMagenta);
        Sword angelicSword = new Sword(100, 230, 630, "Angelic Sword", ConsoleColor.DarkYellow);
        Sword hellsBlade = new Sword(300, 523, 900, "Hells Blade", ConsoleColor.DarkRed);

        Sword elucidator = new Sword(250, 452, 772, "The Elucidator", ConsoleColor.Magenta);
        Sword darkRepulser = new Sword(250, 452, 772, "Dark Repulser", ConsoleColor.Magenta);
        Sword theSticks = new Sword(700, 1300, 2000, "THE sticks", ConsoleColor.Magenta);
        Sword playerSword = new Sword(2, 4, 8, "Rusty Blade", ConsoleColor.White);

        //Mobs) hp,atk1,naam
        Mobs slime = new Mobs(30, 2, "Slime");
        Mobs goblin = new Mobs(40, 3, "Goblin");
        Mobs desertDwellers = new Mobs(73, 15, "Desert Dwellers");
        Mobs orc = new Mobs(120, 23, "Orc");
        Mobs lavaMinion = new Mobs(300, 30, "Lava Minion");
        Mobs demon = new Mobs(523, 52, "Demon");
        Mobs fallenAngel = new Mobs(800, 30, "Fallen Angel");
        Mobs currentMob = new Mobs(1, 1, "Error");

        //Boss) hp, atk1,atk2,burst,naam,kleur
        Boss goblinWarrior = new Boss(220, 10, 16, 25, "Goblin Warrior", ConsoleColor.DarkGreen);
        Boss sandworm = new Boss(750, 90, 150, 300, "Worm Of The Desert", ConsoleColor.DarkYellow);
        Boss demonKing = new Boss(5000, 250, 400, 600, "Demon King Satan", ConsoleColor.DarkRed);
        Boss potoo = new Boss(6666, 333, 555, 800, "Potoo!?", ConsoleColor.DarkRed);
        Boss currentBoss = new Boss(0, 0, 0, 0, "error", ConsoleColor.White);

        //Armors) hp, naam,rarity colors
        Armor farmerArmor = new Armor(150, "Farmer Clothes", ConsoleColor.DarkGray);
        Armor hunterArmor = new Armor(250, "Hunter Armor", ConsoleColor.DarkGreen);
        Armor elvesRobe = new Armor(400, "Elves Robe", ConsoleColor.Green);
        Armor knightArmor = new Armor(800, "Knight Armor", ConsoleColor.Blue);
        Armor kiritoCoat = new Armor(1400, "Kiritos Coat", ConsoleColor.DarkMagenta);
        Armor angelicArmor = new Armor(5500, "Angelic Armor", ConsoleColor.DarkYellow);
        Armor zeroCloak = new Armor(8000, "Zero's Cloak", ConsoleColor.DarkRed);
        Armor playerArmor = new Armor(100, "No Armor", ConsoleColor.White);

        // array van mobs 
        string[] mobs = { "Slime", "Goblin", "DireBoar",
                "Orc", "Lava Minions", "Demons",  "Angels" };

        //Een random generator
        Random random = new Random();

        //Dit is een lijst waarvan ik later dingen toevoeg/weg haal
        List<Sword> swords = new List<Sword>();
        List<Armor> armors = new List<Armor>();


        //Hier stop ik alles in een variabel op.
        string playerInput = "";
        int mana = 0;
        int mobhp;
        int bossHp;
        int playerHp;
        int floor = 1;
        int keyOne = 0;
        int keyTwo = 0;
        int keyThree = 0;
        int fontain = 0;
        int playerAtk;
        int mobAtk;
        int bossAtk;
        string dungeonRaider = File.ReadAllText("GameTitle.txt");
        string startUp = "Welcome to Dungeon Raider!";
        string enteringBossFloor = "Upon entering this floor, you will not be able to come back here...\nThe mobs will become stronger to face..\nAre you sure you want to enter?";
        string secretBossFloor = "???????..";
        string bossChest = File.ReadAllText("Chest.txt");
        string floorTwo = File.ReadAllText("Floor2.txt");
        string floorThree = File.ReadAllText("Floor3.txt");
        string potooImage = File.ReadAllText("Potoo.txt");
        string enteringBossFloorTwo = "??????";


        /// <summart>
        /// Mob/Boss gevecht mechanisme. Je mept en de mob/boss mept je terug.
        /// </summart>
        void BossBattle()
        {
            Console.WriteLine("Choose your type of attack!");
            Console.WriteLine("Skill = 5 Mana. Bursy = 8 Mana");
            Console.Write("Normal Attack (E)  ");
            Console.Write("Skill Attack (Q)  ");
            Console.WriteLine("Burst(X)  ");
            Console.Write("PlayerInput: ");
            playerInput = Console.ReadLine().ToUpper();
            switch (playerInput)
            {
                case "E":
                    mana++;
                    playerAtk = playerSword.Atk1();
                    bossHp = bossHp - playerAtk;
                    Console.Write("Your damage: ");
                    Console.WriteLine(playerAtk);


                    break;

                case "Q":
                    if (mana >= 3)
                    {
                        playerAtk = playerSword.Atk2();
                        bossHp = bossHp - playerAtk;
                        Console.WriteLine("You did a skill attack!");
                        Console.Write("Your damage: ");
                        Console.WriteLine(playerAtk);
                        mana = mana - 3;
                    }
                    else
                    {
                        Console.WriteLine("You do not have the required amount of Mana");
                    }


                    break;

                case "X":
                    if (mana >= 8)
                    {
                        playerAtk = playerSword.Atk3();
                        bossHp = bossHp - playerAtk;
                        Console.WriteLine("BURST!");
                        Console.Write("Your damage: ");
                        Console.WriteLine(playerAtk);
                        mana = mana - 8;
                    }
                    else
                    {
                        Console.WriteLine("You do not have the required amount of Mana");
                    }
                    break;

                default:
                    Console.WriteLine("Error");
                    break;
            }

        }
        void MobBattle()
        {
            Console.WriteLine("Choose your options:");
            Console.WriteLine("Skill = 5 Mana. Burst = 8 Mana");
            Console.Write("Normal Attack (E)  ");
            Console.Write("Skill Attack (Q)  ");
            Console.WriteLine("Burst(X)  ");



            Console.Write("PlayerInput: ");
            playerInput = Console.ReadLine().ToUpper();
            //switch hier reageert alleen op de input van player
            switch (playerInput)
            {
                //mana wordt hier bij 1 erbij geteld bij elke slag. 
                case "E":
                    mana++;
                    playerAtk = playerSword.Atk1();
                    //Hier trek ik de mobhp van af.
                    mobhp = mobhp - playerAtk;
                    Console.Write("Your damage: ");
                    Console.WriteLine(playerAtk);


                    break;

                case "Q":
                    //als mana gelijk of groter is dan 3, dan voert het dit stuk code uit.
                    if (mana >= 3)
                    {
                        playerAtk = playerSword.Atk2();
                        mobhp = mobhp - playerAtk;
                        Console.WriteLine("You used your skill!");
                        Console.Write("Your damage: ");
                        Console.WriteLine(playerAtk);
                        mana = mana - 3;
                    }
                    //anders komt dit bericht 
                    else
                    {
                        Console.WriteLine("You do not have the required amount of Mana");
                    }


                    break;

                case "X":
                    if (mana >= 8)
                    {
                        playerAtk = playerSword.Atk3();
                        mobhp = mobhp - playerSword.Atk3();
                        Console.WriteLine("BURST!");
                        Console.Write("Your damage: ");
                        Console.WriteLine(playerAtk);
                        mana = mana - 8;
                    }
                    else
                    {
                        Console.WriteLine("You do not have the required amount of Mana");
                    }

                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
        }


        /// <summary>
        /// Dit is de intro van het spel
        /// </summary>
        void Start()
        {

            PrintLetter(startUp);
            //Je print tekst in de console
            Console.WriteLine("");
            Console.WriteLine(dungeonRaider);
            Console.WriteLine("Press any key to continue!");
            //Na een key, gaat het verder
            Console.ReadKey();
            //Ik klaar de hele console
            Console.Clear();
            //Hier maak ik de playerHP aan. Deze methode gebruik ik ook voor om mensen tot maximale hp te brengen
            playerHp = playerArmor.AHp();
        }

        /// <summary>
        /// Hier laat ik door alle mobs lopen
        /// </summary>
        void IndexMobs()
        {
            //als player input gelijk is aan mob dex. Dan activeert dit stuk code
            if (playerInput == "mob dex")
            {
                Console.Clear();

                int i = 0;
                // Do while laat ik het door de mobs loopen. Als ik bij while < 0 heb,
                //dan loopt het al sowieso 1x. Er komt dus minimaal een woord.
                do
                {
                    Console.WriteLine(mobs[i]);
                    i++;
                } while (i < mobs.Length);
            }
        }

        /// <summary>
        /// Als de Input niks of iets waarvan je niet moet invullen, dan komt dit stuk code.
        /// </summary>
        void End()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Dit is het begin van waar je start
        /// </summary>
        void StartScreen()
        {
            // \t is gewoon heel veel spaties.
            Console.WriteLine("\t\t\t\t\t\t\t\tCurrent floor: " + floor);
            Console.WriteLine("Current Player HP: " + playerHp);
            Console.WriteLine("Key 1: " + keyOne);
            Console.WriteLine("Key 2: " + keyTwo);
            Console.WriteLine("Key 3: " + keyThree);
            Console.WriteLine("What do you want to do?\n \n");
            Console.WriteLine("Left Door\tBoss Floor\tRight Door\t(LD,BF,RD)");
            Console.WriteLine("Fontain of eternal life (" + fontain + "/3)(f)");
            // Hier geef ik de tekst een kleur
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("???");
            Console.ResetColor();
            Console.WriteLine("inventory weapon (iw)");
            Console.WriteLine("inventory armor (ia)");
            Console.WriteLine("Status");
            Console.WriteLine("Guide");
            Console.WriteLine("Mob Dex");


            Console.Write("PlayerInput: ");
            playerInput = Console.ReadLine().ToLower();
        }

        /// <summary>
        /// Deze methode loopt door armor/weapon uit. Je kijkt naar je inventory en gebruik daarna een armor/wapen.
        /// </summary>
        void WeaponInventory()
        {
            if (playerInput == "inventory weapon")
            {
                Console.Clear();
                Console.WriteLine("Inventory")
;           
                // Hier laat ik de console door alle zwaarden heen loopen/laten zien in de console.
                foreach (Sword sword in swords)
                {
                    Console.ForegroundColor = sword.colors();
                    Console.WriteLine(sword.Name());
                    Console.ResetColor();

                }
                Console.WriteLine("Do you wish to equip a weapon? y/n");
                Console.Write("Player Input: ");
                // player input wordt hier allemaal in kleine letters bewaard.
                playerInput = Console.ReadLine().ToLower();
                if (playerInput == "y")
                {

                    Console.WriteLine("Pick a weapon");
                    Console.Write("Player Input: ");
                    playerInput = Console.ReadLine().ToLower();

                    //&& staat voor OOK ALS. Dus beide condities moeten hierbij voldoen
                    //swords.Contains bedoel ik ermee  als de zwaard er uberhaupt in de list zit.
                    if (playerInput == "dull blade" && swords.Contains(dullBlade))
                    {
                        //swords.add voeg ik een zwaard toe in de list
                        swords.Add(playerSword);
                        Console.Write("You've succesfully eqquiped: ");
                        //Spelers die in gebruik is wordt hier dus nu deze zwaard/
                        playerSword = dullBlade;
                        //Hier haal ik de zwaard weg
                        swords.Remove(dullBlade);
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();

                    }

                    else if (playerInput == "steel sword" && swords.Contains(steelSword))
                    {
                        swords.Add(playerSword);
                        Console.Write("You've succesfully eqquiped: ");
                        playerSword = steelSword;
                        swords.Remove(steelSword);
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "forged dwarf blade" && swords.Contains(forgedDwarfBlade))
                    {
                        swords.Add(playerSword);
                        playerSword = forgedDwarfBlade;
                        swords.Remove(forgedDwarfBlade);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }
                    else if (playerInput == "soul eater" && swords.Contains(soulEater))
                    {
                        swords.Add(playerSword);
                        playerSword = soulEater;
                        swords.Remove(soulEater);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "angelic sword" && swords.Contains(angelicSword))
                    {
                        swords.Add(playerSword);
                        playerSword = angelicSword;
                        swords.Remove(angelicSword);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "demon blade" && swords.Contains(hellsBlade))
                    {
                        swords.Add(playerSword);
                        playerSword = hellsBlade;
                        swords.Remove(hellsBlade);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "dark repulsor" && swords.Contains(darkRepulser))
                    {
                        swords.Add(playerSword);
                        playerSword = darkRepulser;
                        swords.Remove(darkRepulser);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "the elucidator" && swords.Contains(elucidator))
                    {
                        swords.Add(playerSword);
                        playerSword = elucidator;
                        swords.Remove(elucidator);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }

                    else if (playerInput == "the sticks" && swords.Contains(theSticks))
                    {
                        swords.Add(playerSword);
                        playerSword = theSticks;
                        swords.Remove(theSticks);
                        Console.Write("You've succesfully eqquiped: ");
                        Console.ForegroundColor = playerSword.colors();
                        Console.WriteLine(playerSword.Name());
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Wrong input/No sword has been found");
                    }

                }
                else
                {
                    Console.Clear();
                }
            }
        }

        void ArmorInventory()
        {
            if (playerInput == "inventory armor")
            {
                Console.Clear();
                Console.WriteLine("Inventory");
                foreach (Armor armor in armors)
                {
                    Console.ForegroundColor = armor.ArmorColor();
                    Console.WriteLine(armor.AName());
                    Console.ResetColor();
                }
                Console.WriteLine("Do you wish to equip a armor? y/n");
                Console.Write("Player Input: ");
                playerInput = Console.ReadLine().ToLower();
                if (playerInput == "y")
                {
                    Console.WriteLine("Pick a armor");
                    Console.Write("Player Input: ");
                    playerInput = Console.ReadLine().ToLower();
                    if (playerInput == "farmer clothes" && armors.Contains(farmerArmor))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = farmerArmor;
                        armors.Remove(farmerArmor);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else if (playerInput == "hunter armor" && armors.Contains(hunterArmor))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = hunterArmor;
                        armors.Remove(hunterArmor);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else if (playerInput == "elves robe" && armors.Contains(elvesRobe))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = elvesRobe;
                        armors.Remove(elvesRobe);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }
                    else if (playerInput == "knight armor" && armors.Contains(knightArmor))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = knightArmor;
                        armors.Remove(knightArmor);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else if (playerInput == "kiritos coat" && armors.Contains(kiritoCoat))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = kiritoCoat;
                        armors.Remove(kiritoCoat);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else if (playerInput == "angelic armor" && armors.Contains(angelicArmor))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = angelicArmor;
                        armors.Remove(angelicArmor);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else if (playerInput == "zero cloak" && armors.Contains(zeroCloak))
                    {
                        armors.Add(playerArmor);
                        Console.Write("You've succesfully eqquiped: ");
                        playerArmor = zeroCloak;
                        armors.Remove(zeroCloak);
                        Console.ForegroundColor = playerArmor.ArmorColor();
                        Console.WriteLine(playerArmor.AName());
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.WriteLine("wrong input");
                    }

                }
                else
                {
                    Console.Clear();
                }

            }
        }
        /// <summary>
        /// Kleine geheime wapen. Als je die 2 wapens in je inventory hebt, dan kan je de geheime wapen krijgen.
        /// </summary>
        void DualWield()
        {
            if (playerInput == "???" && swords.Contains(darkRepulser) && swords.Contains(elucidator))
            {
                swords.Remove(darkRepulser);
                swords.Remove(elucidator);
                swords.Add(theSticks);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Dark Repulser and Elucidator has been removed from your inventory.");
                Thread.Sleep(500);
                Console.WriteLine("But a different sword has been added to your inventory");
                Console.ResetColor();
            }
            //Als de zwaarden niet gelijk is aan true ( als de zwaarden niet in de list zitten ) dan voert het dit stuk code uit
            else if (playerInput == "???" && true != swords.Contains(darkRepulser) && true != swords.Contains(elucidator))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("???");
                Console.ResetColor();
            }

            else if (playerInput == "???" && false == swords.Contains(darkRepulser)&& true != swords.Contains(elucidator))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("???");
                Console.ResetColor();
            }


            else if (playerInput == "???" && true != swords.Contains(darkRepulser) && false == swords.Contains(elucidator))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("???");
                Console.ResetColor();
            }

        }

        /// <summary>
        /// mob floor/boss floor is het heel gevecht. Je verslaat een mob/boss en krijgt buit. 
        /// </summary>
        void MobFloorOne()
        {
            // || betekent dat er 1 van de condities waar moet zijn.
            if (floor == 1 && playerInput == "ld" || floor == 1 && playerInput == "rd")
            {
                //Hier heb ik allerlei random number generators gemaakt. Natuurlijk hebben ze allemaal hun eigen rol!
                int SpawnMobs = random.Next(1, 11);
                int LootDrop = random.Next(1, 3);
                int WeaponDrop = random.Next(1, 11);
                int ArmorDrop = random.Next(1, 11);
                int KeyDrop = random.Next(1, 7);
                Console.WriteLine("Entering the room...");
                //Als spawnmobs kleiner is dan 7, voert het dit code uit
                if (SpawnMobs < 7)
                {
                    currentMob = slime;
                }

                else if (SpawnMobs >= 7)
                {
                    currentMob = goblin;
                }

                else if (SpawnMobs == 7)
                {
                    Console.WriteLine("Number does not exist");
                }

                //Hier deklaar ik de mobhp naar de mob die is gespawnt. Je vecht eigenlijk technisch gezien zijn hp
                mobhp = currentMob.MobHp();

                //Hier laat ik de console pauze nemen voordat het verder gaat.
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("A Monster Has Appeard!");
                //Hier print ik de naam van de mob die ik heb aangemaakt in een class.
                Console.WriteLine(currentMob.MobName());


                while (mobhp > 0 && playerHp > 0)
                {

                    Console.Write("Player Hp:");
                    Console.WriteLine(playerHp);
                    Console.Write(currentMob.MobName());
                    Console.Write(" Hp:");
                    Console.WriteLine(mobhp);
                    Console.Write("Mana: ");
                    Console.WriteLine(mana);
                    MobBattle();
                    if (mobhp > 0)
                    {
                        mobAtk = currentMob.Atk1();
                        playerHp = playerHp - mobAtk;
                        Console.Write("Mob damage: ");
                        Console.WriteLine(mobAtk);

                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                if (mobhp < 1)
                {


                    fontain++;
                    Console.WriteLine("You have killed the mob!");

                    if (LootDrop == 1)
                    {

                        if (KeyDrop >= 4 && keyOne == 0)
                        {
                            Console.WriteLine("The mob has dropped a key...");
                            keyOne = 1;
                        }

                        if (WeaponDrop == 10)
                        {
                            swords.Add(forgedDwarfBlade);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = forgedDwarfBlade.colors();
                            Console.WriteLine(forgedDwarfBlade.Name());
                            Console.ResetColor();
                        }

                        else if (WeaponDrop < 7)
                        {
                            swords.Add(dullBlade);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = dullBlade.colors();
                            Console.WriteLine(dullBlade.Name());
                            Console.ResetColor();
                        }
                        else if (WeaponDrop >= 7)
                        {
                            swords.Add(steelSword);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = steelSword.colors();
                            Console.WriteLine(steelSword.Name());
                            Console.ResetColor();
                        }
                    }
                    else if (LootDrop == 2)
                    {
                        if (ArmorDrop == 10)
                        {
                            armors.Add(elvesRobe);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = elvesRobe.ArmorColor();
                            Console.WriteLine(elvesRobe.AName());
                            Console.ResetColor();
                        }

                        else if (ArmorDrop < 7)
                        {
                            armors.Add(farmerArmor);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = farmerArmor.ArmorColor();
                            Console.WriteLine(farmerArmor.AName());
                            Console.ResetColor();
                        }
                        else if (ArmorDrop >= 7)
                        {
                            armors.Add(hunterArmor);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = hunterArmor.ArmorColor();
                            Console.WriteLine(hunterArmor.AName());
                            Console.ResetColor();
                        }
                    }

                    else if (LootDrop == 3)
                    {
                        Console.WriteLine("Error");
                    }


                }
            }
        }

        void MobFloorTwo()
        {
            if (floor == 2 && playerInput == "ld" || floor == 2 && playerInput == "rd")
            {
                int SpawnMobs = random.Next(1, 11);
                int LootDrop = random.Next(1, 3);
                int WeaponDrop = random.Next(1, 51);
                int ArmorDrop = random.Next(1, 11);
                int KeyDrop = random.Next(1, 7);
                Console.WriteLine("Entering the room...");

                if (SpawnMobs < 7)
                {
                    currentMob = desertDwellers;
                }

                else if (SpawnMobs >= 7)
                {
                    currentMob = orc;
                }

                else if (SpawnMobs == 7)
                {
                    Console.WriteLine("Error");
                }


                mobhp = currentMob.MobHp();


                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("A Monster Has Appeard!");
                Console.WriteLine(currentMob.MobName());


                while (mobhp > 0 && playerHp > 0)
                {

                    Console.Write("Player Hp:");
                    Console.WriteLine(playerHp);
                    Console.Write(currentMob.MobName());
                    Console.Write(" Hp:");
                    Console.WriteLine(mobhp);
                    Console.Write("Mana: ");
                    Console.WriteLine(mana);
                    MobBattle();
                    if (mobhp > 0)
                    {
                        mobAtk = currentMob.Atk1();
                        playerHp = playerHp - mobAtk;
                        Console.Write("Mob damage: ");
                        Console.WriteLine(mobAtk);

                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                if (mobhp < 1)
                {


                    fontain++;
                    Console.WriteLine("You have killed the mob!");

                    if (LootDrop == 1)
                    {

                        if (KeyDrop >= 4 && keyTwo == 0)
                        {
                            Console.WriteLine("The mob has dropped a key...");
                            keyTwo = 1;
                        }


                        if (WeaponDrop == 50)
                        {
                            swords.Add(darkRepulser);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = darkRepulser.colors();
                            Console.WriteLine(darkRepulser.Name());
                            Console.ResetColor();
                        }
                        else if (WeaponDrop >= 35)
                        {
                            swords.Add(soulEater);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = soulEater.colors();
                            Console.WriteLine(soulEater.Name());
                            Console.ResetColor();
                        }

                        else if (WeaponDrop < 35)
                        {
                            swords.Add(forgedDwarfBlade);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = forgedDwarfBlade.colors();
                            Console.WriteLine(forgedDwarfBlade.Name());
                            Console.ResetColor();
                        }


                    }
                    else if (LootDrop == 2)
                    {
                        if (ArmorDrop == 10)
                        {
                            armors.Add(angelicArmor);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = angelicArmor.ArmorColor();
                            Console.WriteLine(angelicArmor.AName());
                            Console.ResetColor();
                        }

                        else if (ArmorDrop < 7)
                        {
                            armors.Add(knightArmor);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = knightArmor.ArmorColor();
                            Console.WriteLine(knightArmor.AName());
                            Console.ResetColor();
                        }
                        else if (ArmorDrop >= 7)
                        {
                            armors.Add(kiritoCoat);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = kiritoCoat.ArmorColor();
                            Console.WriteLine(kiritoCoat.AName());
                            Console.ResetColor();
                        }
                    }

                    else if (LootDrop == 3)
                    {
                        Console.WriteLine("Error");
                    }


                }
            }
        }

        void MobFloorThree()
        {
            if (floor == 3 && playerInput == "ld" || floor == 3 && playerInput == "rd")
            {
                int SpawnMobs = random.Next(1, 51);
                int LootDrop = random.Next(1, 3);
                int WeaponDrop = random.Next(1, 51);
                int ArmorDrop = random.Next(1, 51);
                int KeyDrop = random.Next(1, 7);
                Console.WriteLine("Entering the room...");


                if (SpawnMobs == 50)
                {
                    currentMob = fallenAngel;
                }

                else if (SpawnMobs < 35)
                {
                    currentMob = lavaMinion;
                }

                else if (SpawnMobs >= 35)
                {
                    currentMob = demon;
                }




                mobhp = currentMob.MobHp();


                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine("A Monster Has Appeard!");
                Console.WriteLine(currentMob.MobName());


                while (mobhp > 0 && playerHp > 0)
                {

                    Console.Write("Player Hp:");
                    Console.WriteLine(playerHp);
                    Console.Write(currentMob.MobName());
                    Console.Write(" Hp:");
                    Console.WriteLine(mobhp);
                    Console.Write("Mana: ");
                    Console.WriteLine(mana);
                    MobBattle();
                   
                    if (mobhp > 0)
                    {
                        mobAtk = currentMob.Atk1();
                        playerHp = playerHp - mobAtk;
                        Console.Write("Mob damage: ");
                        Console.WriteLine(mobAtk);

                    }
                    Console.ReadKey();
                    Console.Clear();
                }
                if (mobhp < 1)
                {


                    fontain++;
                    Console.WriteLine("You have killed the mob!");

                    if (LootDrop == 1)
                    {

                        if (KeyDrop >= 4 && keyThree == 0)
                        {
                            Console.WriteLine("The mob has dropped a key...");
                            keyThree = 1;
                        }
                        if (WeaponDrop == 50)
                        {
                            swords.Add(hellsBlade);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = hellsBlade.colors();
                            Console.WriteLine(hellsBlade.Name());
                            Console.ResetColor();
                        }


                        else if (WeaponDrop >= 35)
                        {
                            swords.Add(angelicSword);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = angelicSword.colors();
                            Console.WriteLine(angelicSword.Name());
                            Console.ResetColor();
                        }

                        else if (WeaponDrop < 35)
                        {
                            swords.Add(soulEater);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = soulEater.colors();
                            Console.WriteLine(soulEater.Name());
                            Console.ResetColor();
                        }

                    }
                    else if (LootDrop == 2)
                    {
                        if (ArmorDrop == 50)
                        {
                            armors.Add(zeroCloak);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = zeroCloak.ArmorColor();
                            Console.WriteLine(zeroCloak.AName());
                            Console.ResetColor();
                        }

                        else if (ArmorDrop < 7)
                        {
                            armors.Add(kiritoCoat);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = kiritoCoat.ArmorColor();
                            Console.WriteLine(kiritoCoat.AName());
                            Console.ResetColor();
                        }
                        else if (ArmorDrop >= 7)
                        {
                            armors.Add(angelicArmor);
                            Console.Write("De mob has dropped a: ");
                            Console.ForegroundColor = angelicArmor.ArmorColor();
                            Console.WriteLine(angelicArmor.AName());
                            Console.ResetColor();
                        }
                    }

                    else if (LootDrop == 3)
                    {
                        Console.WriteLine("Error");
                    }


                }
            }
        }
        void BossFloorOne()
        {
            if (playerInput == "bf" && keyOne == 1 && floor == 1)
            {
                currentBoss = goblinWarrior;
                bossHp = currentBoss.Hp();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                PrintLetter(enteringBossFloor);
                Console.ResetColor();
                Console.WriteLine("yes/no");
                Console.Write("PlayerInput: ");
                playerInput = Console.ReadLine().ToLower();


                if (playerInput == "no")
                {
                    Console.WriteLine("You safely walked away!");
                }
                else if (playerInput == "yes")
                {
                    mana = 0;
                    Console.WriteLine("Entering the boss floor...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Boss: ");
                    Console.ForegroundColor = currentBoss.bossColor();
                    Console.WriteLine(currentBoss.BossName());
                    Console.ResetColor();

                    while (bossHp > 0 && playerHp > 0)
                    {
                        int bossmove = random.Next(1, 11);
                        Console.ForegroundColor = currentBoss.bossColor();
                        Console.Write(currentBoss.BossName());
                        Console.ResetColor();
                        Console.Write(" Hp: ");
                        Console.WriteLine(bossHp);
                        Console.Write("Player Hp: ");
                        Console.WriteLine(playerHp);
                        Console.Write("Mana: ");
                        Console.WriteLine(mana);

                        BossBattle();


                        if (bossmove < 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk1();
                            playerHp = playerHp - bossAtk;
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }
                        else if (bossmove >= 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk2();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.WriteLine(" Used axe swing!");
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }

                        else if (bossmove == 10 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Burst();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.WriteLine(" Has used ground slam!");
                            Console.WriteLine("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }

                        else
                        {

                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (playerHp < 1)
                    {
                        Console.WriteLine("Game over");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }


                    else if (bossHp < 1)
                    {
                        Console.WriteLine("You've beaten the boss!");
                        Console.WriteLine("Pick your juicy rewards!.");
                        Console.WriteLine(bossChest + "\n\n\n\n" + bossChest);
                        Console.WriteLine("Q = Weapon chest\n\n\n E = Armor chest");
                        playerInput = "";
                        playerHp = playerArmor.AHp();
                        while (playerInput != "q" && playerInput != "e")
                        {
                            playerInput = Console.ReadLine().ToLower();
                            if (playerInput == "q")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = forgedDwarfBlade.colors();
                                Console.WriteLine(forgedDwarfBlade.Name());
                                Console.Write(forgedDwarfBlade.Name());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                swords.Add(forgedDwarfBlade);
                            }

                            else if (playerInput == "e")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = elvesRobe.ArmorColor();
                                Console.WriteLine(elvesRobe.AName());
                                Console.Write(elvesRobe.AName());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                armors.Add(elvesRobe);
                            }

                            else
                            {
                                Console.WriteLine("Error");
                            }

                            Console.WriteLine("Congratulations beating floor 1!");
                            Console.WriteLine("You will now proceed to");
                            Console.WriteLine(floorTwo);
                            floor = 2;
                        }


                    }

                    else
                    {
                        Console.Clear();
                    }
                }
            }

            else if (playerInput == "boss floor" && keyOne != 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Search for the key!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        void BossFloorTwo()
        {
            if (playerInput == "bf" && keyTwo == 1 && floor == 2)
            {
                currentBoss = sandworm;
                bossHp = currentBoss.Hp();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                PrintLetter(enteringBossFloor);
                Console.ResetColor();
                Console.WriteLine("yes/no");
                Console.Write("PlayerInput: ");
                playerInput = Console.ReadLine().ToLower();


                if (playerInput == "no")
                {
                    Console.WriteLine("You safely walked away!");
                }
                else if (playerInput == "yes")
                {
                    mana = 0;
                    Console.WriteLine("Entering the boss floor...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Boss: ");
                    Console.ForegroundColor = currentBoss.bossColor();
                    Console.WriteLine(currentBoss.BossName());
                    Console.ResetColor();
                    while (bossHp > 0 && playerHp > 0)
                    {
                        int bossmove = random.Next(1, 11);
                        Console.ForegroundColor = currentBoss.bossColor();
                        Console.Write(currentBoss.BossName());
                        Console.ResetColor();
                        Console.Write(" Hp: ");
                        Console.WriteLine(bossHp);
                        Console.Write("Player Hp: ");
                        Console.WriteLine(playerHp);
                        Console.Write("Mana: ");
                        Console.WriteLine(mana);

                        BossBattle();


                        if (bossmove < 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk1();
                            playerHp = playerHp - bossAtk;
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }
                        else if (bossmove >= 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk2();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.WriteLine(" Tail swipe!");
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }

                        else if (bossmove == 10 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Burst();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.Write(" Went underground and ate you! ");
                            Console.WriteLine(bossAtk);
                        }

                        else
                        {

                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (playerHp < 1)
                    {
                        Console.WriteLine("Game over");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }


                    else if (bossHp < 1)
                    {
                        Console.WriteLine("You've beaten the boss!");
                        Console.WriteLine("Pick your juicy rewards!.");
                        Console.WriteLine(bossChest + "\n\n\n\n" + bossChest);
                        Console.WriteLine("Q = Weapon chest\n\n\n E = Armor chest");
                        playerInput = "";
                        playerHp = playerArmor.AHp();
                        while (playerInput != "q" && playerInput != "e")
                        {
                            playerInput = Console.ReadLine().ToLower();
                            if (playerInput == "q")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = darkRepulser.colors();
                                Console.WriteLine(elucidator.Name());
                                Console.Write(elucidator.Name());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                swords.Add(elucidator);
                            }

                            else if (playerInput == "e")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = kiritoCoat.ArmorColor();
                                Console.WriteLine(kiritoCoat.AName());
                                Console.Write(kiritoCoat.AName());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                armors.Add(kiritoCoat);
                            }

                            else
                            {
                                Console.WriteLine("Error");
                            }

                            Console.WriteLine("Congratulations beating floor 2!");
                            Console.WriteLine("You will now proceed to");
                            Console.WriteLine(floorThree);
                            floor = 3;
                        }


                    }

                    else
                    {
                        Console.Clear();
                    }
                }
            }

            else if (playerInput == "boss floor" && keyTwo != 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Search for the key!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        void BossFloorThree()
        {
            if (playerInput == "bf" && keyThree == 1 && floor == 3)
            {
                currentBoss = demonKing;
                bossHp = currentBoss.Hp();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                PrintLetter(enteringBossFloor);
                Console.ResetColor();
                Console.WriteLine("yes/no");
                Console.Write("PlayerInput: ");
                playerInput = Console.ReadLine().ToLower();


                if (playerInput == "no")
                {
                    Console.WriteLine("You safely walked away!");
                }
                else if (playerInput == "yes")
                {
                    mana = 0;
                    Console.WriteLine("Entering the boss floor...");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.Write("Boss: ");
                    Console.ForegroundColor = currentBoss.bossColor();
                    Console.WriteLine(currentBoss.BossName());
                    Console.ResetColor();
                    while (bossHp > 0 && playerHp > 0)
                    {
                        int bossmove = random.Next(1, 11);
                        Console.ForegroundColor = currentBoss.bossColor();
                        Console.Write(currentBoss.BossName());
                        Console.ResetColor();
                        Console.Write(" Hp: ");
                        Console.WriteLine(bossHp);
                        Console.Write("Player Hp: ");
                        Console.WriteLine(playerHp);
                        Console.Write("Mana: ");
                        Console.WriteLine(mana);

                        BossBattle();


                        if (bossmove < 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk1();
                            playerHp = playerHp - bossAtk;
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }
                        else if (bossmove >= 8 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Atk2();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.WriteLine(" Used Astor Inqerad");
                            Console.Write("Boss damage: ");
                            Console.WriteLine(bossAtk);
                        }

                        else if (bossmove == 10 && bossHp > 0)
                        {
                            bossAtk = currentBoss.Burst();
                            playerHp = playerHp - bossAtk;
                            Console.ForegroundColor = currentBoss.bossColor();
                            Console.Write(currentBoss.BossName());
                            Console.ResetColor();
                            Console.Write(" Has used Amol Dherrsaiqa! ");
                            Console.WriteLine(bossAtk);
                        }

                        else
                        {

                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (playerHp < 1)
                    {
                        Console.WriteLine("Game over");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }


                    else if (bossHp < 1)
                    {
                        Console.WriteLine("You've beaten the boss!");
                        Console.WriteLine("Pick your juicy rewards!.");
                        Console.WriteLine(bossChest + "\n\n\n\n" + bossChest);
                        Console.WriteLine("Q = Weapon chest\n\n\n E = Armor chest");
                        playerInput = "";
                        playerHp = playerArmor.AHp();
                        while (playerInput != "q" && playerInput != "e")
                        {
                            playerInput = Console.ReadLine().ToLower();
                            if (playerInput == "q")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = hellsBlade.colors();
                                Console.WriteLine(hellsBlade.Name());
                                Console.Write(hellsBlade.Name());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                swords.Add(hellsBlade);
                            }

                            else if (playerInput == "e")
                            {
                                Console.Write("You found a ");
                                Console.ForegroundColor = angelicArmor.ArmorColor();
                                Console.WriteLine(angelicArmor.AName());
                                Console.Write(angelicArmor.AName());
                                Console.ResetColor();
                                Console.WriteLine(" Got added to your inventory");
                                armors.Add(angelicArmor);
                            }

                            else
                            {
                                Console.WriteLine("Error");
                            }

                            Console.WriteLine("Congratulations beating floor 3!");
                            Console.WriteLine("Code: potoo!");
                
                         
                            floor = 4;
                        }


                    }

                    else
                    {
                        Console.Clear();
                    }
                }
            }

            else if (playerInput == "boss floor" && keyOne != 3)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Search for the key!");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Hier kijk je naar je status. Hp, wapen, base atk van de wapen en etc 
        /// </summary>
        void Status()
        {
            if (playerInput == "status")
            {
                Console.Write("Armor: ");
                Console.ForegroundColor = playerArmor.ArmorColor();
                Console.WriteLine(playerArmor.AName());
                Console.ResetColor();
                Console.WriteLine("Max armor hp: " + playerArmor.AHp());



                Console.Write("Weapon: ");
                Console.ForegroundColor = playerSword.colors();
                Console.WriteLine(playerSword.Name());
                Console.ResetColor();
                Console.WriteLine("Base ATK: " + playerSword.Atk1());



            }
        }

        /// <summary>
        /// Print een kleine uitleg over de game uit
        /// </summary>
        void Guide()
        {
            if (playerInput == "guide")
            {
                // \n betekent enter
                Console.WriteLine("What is the goal in the game?\n" +
                    "You simply kill the mobs by going through the dungeon. Get their loot and reach higher floors!" +
                    "\nYou will need to beat the end boss of this game to win!\n" +
                    " Good luck out there Player!");
            }

        }

        /// <summary>
        /// Fontain waarvan de speler tot max hp wordt hersteld
        /// </summary>
        void Fontain()
        {
            if (playerInput == "f" && fontain >= 3)
            {
                fontain = 0;
                playerHp = playerArmor.AHp();
                Console.WriteLine("Fontain of eternal life was used. Your on max hp!");
            }

            else if (playerInput == "fontain of eternal life" && fontain < 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Prove your worthness to the Fontain of Eternal Life.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Hier laat ik de letters langzaam uitprinten
        /// </summary>
      
        void PrintLetter(string _printletter)
        {
            // voor als i kleiner is dan de text, dan komt er telkens 1 erbij. 
            for (int i = 0; i < _printletter.Length; i++)
            {
                Thread.Sleep(70);
                Console.Write(_printletter[i]);
            }
            Console.WriteLine();
        }


        /// <summary>
        /// Geheime potoo baas met ongeveer hetzelfde mechanisme van boss floors
        /// </summary>
        void Potoo()
        {
            if (playerInput == "potoo")
            {
                currentBoss = potoo;
                bossHp = currentBoss.Hp();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                //ik geef de achtergrond een mooi kleurtje.
                Console.BackgroundColor = ConsoleColor.White;
                PrintLetter(secretBossFloor);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                PrintLetter(enteringBossFloorTwo);
                Thread.Sleep(2000);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(potooImage);
                Thread.Sleep(2000);
                Console.Clear();
                
                mana = 0;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("Entering the ?????...");
                Thread.Sleep(2000);
                Console.Clear();
                Console.Write("Boss: ");
                Console.ForegroundColor = currentBoss.bossColor();
                Console.WriteLine(currentBoss.BossName());
                //De game blijft hier de heletijd loopen omdat ze beide boven 0 hp hebben
                while (bossHp > 0 && playerHp > 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    int bossmove = random.Next(1, 11);
                    Console.ForegroundColor = currentBoss.bossColor();
                    Console.Write(currentBoss.BossName());
                    Console.ResetColor();
                    Console.Write(" Hp: ");
                    Console.WriteLine(bossHp);
                    Console.WriteLine("Player Hp: "+ playerHp);
                    Console.Write("Mana: ");
                    Console.WriteLine(mana);

                    BossBattle();

                    if (bossmove < 8 && bossHp > 0)
                    {
                        bossAtk = currentBoss.Atk1();
                        playerHp = playerHp - bossAtk;
                        Console.WriteLine("Potoo swing");
                        Console.Write("Boss damage: ");
                        Console.WriteLine(bossAtk);
                    }
                    else if (bossmove >= 8 && bossHp > 0)
                    {
                        bossAtk = currentBoss.Atk2();
                        playerHp = playerHp - bossAtk;
                        Console.ForegroundColor = currentBoss.bossColor();
                        Console.Write(currentBoss.BossName());
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine(" Used Peck!");
                        Console.Write("Boss damage: ");
                        Console.WriteLine(bossAtk);
                    }

                    else if (bossmove == 10 && bossHp > 0)
                    {
                        bossAtk = currentBoss.Burst();
                        playerHp = playerHp - bossAtk;
                        Console.ForegroundColor = currentBoss.bossColor();
                        Console.Write(currentBoss.BossName());
                        Console.ResetColor();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" Has used Butterfly effect! ");
                        Console.WriteLine(bossAtk);
                    }

                    else
                    {

                    }
                    Console.ReadKey();
                    Console.Clear();
                }


                if (playerHp < 1)
                {
                    Console.WriteLine("Game over");
                    Console.ReadKey();
                    //Hier verlaat je de applicatie.
                    Environment.Exit(0);
                }


                else if (bossHp < 1)
                {
                    Console.WriteLine("Congratulations beating the potoo!");
                    Console.WriteLine("You've beaten the game!");
                    Console.WriteLine("Here is your participation trophy!");
                    Console.ReadKey();
                    Environment.Exit(0);

                }


            }

        }



    }
}