using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Rtan_Text_RPG
{
    internal class Program
    {
        static Character _player;
        static Item[] _items;

        static void Main(string[] args)
        {
            string playerName = WhenGameStart();
            Thread.Sleep(1000);
            Console.WriteLine("갑자기 앞이 흐려진다...");
            Thread.Sleep(2000);
            GameDataSetting(playerName);
            Village(playerName);

        }

        static string WhenGameStart()
        {
            string playerName = "";
            int answerCount = 0;
            int nameAnswer;
            int readyAnswer;
            bool answerCheck = false;
            bool answerCheck2 = false;

            Console.WriteLine("르탄 던전에 오신 것을 환영합니다.");
            Thread.Sleep(1000);
            Console.WriteLine("\n");
            Console.WriteLine("모험가님의 이름을 알려주실 수 있나요?");
            Thread.Sleep(1000);
            while (!answerCheck)
            {
                Console.WriteLine("\n");
                Console.WriteLine("1.네, 알려릴게요.");
                Console.WriteLine("2.아니요, 알려드리기 싫습니다.");
                Console.WriteLine("\n");
                Console.Write("1, 2중에 선택:");
                if (int.TryParse(Console.ReadLine(), out nameAnswer))
                {
                    if (nameAnswer == 1)
                    {
                        Console.Write("이름을 입력해 주세요:");
                        playerName = Console.ReadLine();
                        answerCheck = true;
                    }
                    else if (nameAnswer == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("그럼 이름을 \"모험가\"로 저장할게요. ");
                        playerName = "모험가";
                        answerCheck = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("다시 입력해 주세요");
                        answerCheck = false;
                    }
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("모험가 정보 등록중");
            Console.WriteLine("......");
            Thread.Sleep(1500);
            Console.WriteLine("새로운 캐릭터 생성 중");
            Console.WriteLine("......");
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("자~!! 이제 모든 등록이 끝났습니다.");
            Thread.Sleep(1000);
            Console.WriteLine("모험을 떠날 준비가 되셨나요?");
            while (!answerCheck2)
            {
                Console.WriteLine("1.네 2.아니오");
                Console.Write("답변:");
                if (int.TryParse(Console.ReadLine(), out readyAnswer))
                {
                    if (readyAnswer == 1)
                    {
                        answerCheck2 = true;
                    }
                    else if (readyAnswer == 2)
                    {
                        ++answerCount;
                        if (answerCount <= 3)
                        {
                            Console.Clear();
                            Console.WriteLine("......");
                            Thread.Sleep(1000);
                            Console.WriteLine("......");
                            Thread.Sleep(1000);
                            Console.WriteLine("이제는 되셨나요?");
                            Thread.Sleep(1000);
                            answerCheck2 = false;
                        }
                        else if (answerCount > 4)
                        {
                            Console.WriteLine("이제 더이상 시간이 없습니다!!");
                            Console.WriteLine("지금 바로 이동하겠습니다.");
                            answerCheck2 = true;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("다시 입력해 주세요");
                        answerCheck2 = false;
                    }
                }
            }

            return playerName;
        }

        static void Village(string playerName)
        {
            bool answerCheck3 = false;
            int villageChoice;
            Console.Clear();
            Console.WriteLine($"촌장:스파르타 마을에 온 것을 환영하네!! {playerName}(이)여");
            Console.WriteLine("무엇을 하러 왔는가?");
            Console.WriteLine("\n");
            while (!answerCheck3)
            {
                Console.WriteLine("1. 상태창 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine();
                Console.Write("입력해주세요:");
                if (int.TryParse(Console.ReadLine(), out villageChoice))
                {
                    if (villageChoice == 1)
                    {
                        DisplayMyInfo(playerName);
                        answerCheck3 = true;
                    }
                    else if (villageChoice == 2)
                    {
                        DisplayInventory(playerName);
                        answerCheck3 = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        answerCheck3 = false;
                    }
                }
            }
        }

        static void GameDataSetting(string playerName)
        {
            // 캐릭터 정보 세팅
            _player = new Character($"{playerName}", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
            _items = new Item[10];
            AddItem(new Item("무쇠 갑옷", "무쇠로 만든 갑옷", 0, 0, 3, 0));
            AddItem(new Item("낡은 철검", "오래써서 낡은 철 검", 1, 2, 0, 0));
            AddItem(new Item("철 갑옷", "흔한 철 갑옷", 0, 0, 5, 0));
            AddItem(new Item("철 검", "흔한 철 검", 1, 5, 0, 0));
            AddItem(new Item("활력의 갑옷", "약간의 HP를 올려주는 갑옷", 0, 0, 3, 5));
            AddItem(new Item("게임 끝", "게임을 끝내고 싶을 때 쓰는거", 3, 100, 100, 100));
        }
        static void DisplayMyInfo(string playerName)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();

            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetLevelColor(_player.Level);
            Console.WriteLine($"Lv.{_player.Level}");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"{_player.Name}({_player.Job})");
            int bonusAtk = getSumBonusAtk();
            Console.WriteLine($"공격력 : {_player.Atk + bonusAtk}");
            int bonusDef = getSumBonusDef();
            Console.WriteLine($"방어력 : {_player.Def + bonusDef}");
            int bonusHp = getSumBonusHp();
            Console.WriteLine($"체력 : {_player.Hp + bonusHp}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Gold : {_player.Gold} G");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0.나가기");
            Console.WriteLine("1.이름 바꾸기");

            Console.Write("입력해 주세요:");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    Village(playerName);
                    break;
                
                case 1:
                    Console.Write("이름을 입력해 주세요:");
                    playerName = Console.ReadLine();
                    _player = new Character($"{playerName}", "전사", 1, 10, 5, 100, 1500);
                    DisplayMyInfo(playerName);
                    break;
            }
        }
        private static int getSumBonusAtk()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Atk;
            }
            return sum;
        }

        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Def;
            }
            return sum;
        }

        private static int getSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Hp;
            }
            return sum;
        }
        static ConsoleColor GetLevelColor(int level)
        {
            return (level <= 10) ? ConsoleColor.DarkGray : (level <= 20) ? ConsoleColor.Yellow : (level <= 30) ? ConsoleColor.DarkRed : (level <= 40) ? ConsoleColor.Green : (level <= 50) ? ConsoleColor.Blue : (level <= 60) ? ConsoleColor.Magenta : (level <= 70) ? ConsoleColor.DarkYellow : (level <= 80) ? ConsoleColor.Red : (level <= 90) ? ConsoleColor.Black : ConsoleColor.White;
        }

        static void DisplayInventory(string playerName)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 볼 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("\n");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription();
            }
            Console.WriteLine("\n");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("입력해 주세요:");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    Village(playerName);
                    break;

                case 1:
                    Console.Clear();
                    ManagementInventory(playerName);
                    break;
            }
        }
        static void ManagementInventory(string playerName)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("장착관리");
            Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 장착 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("\n");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1); // 1, 2, 3에 매핑하기 위해 +1
            }
            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("입력해 주세요:");
            int keyInput = CheckValidInput(0, Item.ItemCnt);

            switch (keyInput)
            {
                case 0:
                    DisplayInventory(playerName);                    
                    break;
                default:
                    ToggleEquipStatus(keyInput - 1); // 유저가 입력하는건 1, 2, 3 : 실제 배열에는 0, 1, 2...
                    ManagementInventory(playerName);                   
                    break;
            }
        }
        static void AddItem(Item item)
        {
            if (Item.ItemCnt == 10) return;
            _items[Item.ItemCnt] = item;
            Item.ItemCnt++;
        }
        static void ToggleEquipStatus(int idx)
        {
            _items[idx].IsEquiped = !_items[idx].IsEquiped;
        }
        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine();
                Console.Write("다시 입력해 주세요:");
            }
        }
    }


    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        // 개선포인트 : Enum 활용
        public int Type { get; }

        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }


        public bool IsEquiped { get; set; }

        public static int ItemCnt = 0;

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquiped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquiped = isEquiped;
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            // 장착관리 전용
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquiped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));

            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"Hp {(Hp >= 0 ? "+" : "")}{Hp}");

            Console.Write(" | ");

            Console.WriteLine(Description);
        }

        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

    }
}