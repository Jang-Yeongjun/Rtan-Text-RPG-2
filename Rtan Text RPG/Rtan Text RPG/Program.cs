using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;
using System.Security.Cryptography.X509Certificates;

namespace Rtan_Text_RPG
{
    internal class Program
    {
        private static Character player;
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
                        if (answerCount <= 5)
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
            player = new Character($"{playerName}", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅
        }
        static void DisplayMyInfo(string playerName)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.ResetColor();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = GetLevelColor(player.Level);
            Console.WriteLine($"Lv.{player.Level}");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
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
                    player = new Character($"{playerName}", "전사", 1, 10, 5, 100, 1500);
                    DisplayMyInfo(playerName);
                    break;
            }
        }
        static ConsoleColor GetLevelColor(int level)
        {
            return (level <= 10) ? ConsoleColor.Yellow : (level <= 50) ? ConsoleColor.Green : (level <= 100) ? ConsoleColor.Red : ConsoleColor.Black;
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
            Item itemInstance = new Item();
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
            Item itemInstance = new Item();
            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("입력해 주세요:");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayInventory(playerName);
                    break;
                case 1:
                    break;
                case 2:
                    
                    break;
            }
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
        public Item()
        {
            int num = 1;
            string[] inventory = {"무쇠갑옷(방어력 +5)", "낡은 검(공격력 +2)"};
            foreach (string item in inventory)
            {
                Console.WriteLine("-("+$"{num}"+")"+item);
                    num++;
            }
        }
    }
}