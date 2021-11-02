using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using H4M.Properties;

namespace H4M
{
    class Program
    {
        public static string TEXT_DIRECTORY { get; } = $@"{AppContext.BaseDirectory}Texts";
        public static Dictionary<string, string> Dict { get; private set; } = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Initialize();

                string[] paths = Directory.GetFiles(TEXT_DIRECTORY, "*.txt", SearchOption.TopDirectoryOnly);

                for (var i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(path)}");
                }

                if (paths.Length < 0)
                {
                    Console.WriteLine("Couldn't find any text file.");
                    return;
                }

                Console.Write("Please select the number which you want to open the text: ");
                string num = Console.ReadLine();

                if (int.TryParse(num, out var n))
                {
                    if (n >= 1 && n <= paths.Length)
                    {
                        string text = File.ReadAllText(paths[n - 1]);
                        Dict = DictionaryManager.GetDictionaryFromText(text, true);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("it's not number.");
                    return;
                }

                var stats = PlayGame(Dict);

                Console.Clear();
                PrintGameStats(stats);

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadLine(); //waiting for continue

                Console.Clear();
            }
        }

        static void Initialize()
        {
            Console.Title = "H4M v21.2.2";
            Console.WriteLine(Resources.H4MA);
            Console.WriteLine("by MineEric64, 2021");
            Console.WriteLine("only supports txt extension for searching dictionary data file.");
            Console.WriteLine();
        }

        static void PrintGameStats(GameStats stats)
        {
            Console.WriteLine($"Solved Count: {stats.SolvedCount}");
            Console.WriteLine($"Wrong Count: {stats.WrongCount}");
            Console.WriteLine($"Wrong Answers: [ {string.Join(", ", stats.WrongKeys)} ]");
        }

        static GameStats PlayGame(Dictionary<string, string> dict)
        {
            var random = new Random();

            GameStats stats = new GameStats(0, 0);
            List<string> keys = dict.Keys.OrderBy(x => random.Next()).ToList();
            List<string> values = dict.Values.ToList();

            List<int> indicies = new List<int>();

            for (var i = 0; i < keys.Count; i++)
            {
                string key = keys[i];

                Console.Clear();

                Console.WriteLine($"{i + 1}] {key}");

                int max_count = Math.Min(5, dict.Values.Count);
                int real = random.Next(max_count);

                for (var j = 0; j < max_count; j++)
                {
                    if (j == real)
                    {
                        Console.WriteLine($"{real + 1}. {dict[key]}");
                        continue;
                    }
                    
                    int index = random.Next(dict.Values.Count);

                    while (indicies.Contains(index) || values[index] == dict[key])
                    {
                        index = random.Next(dict.Values.Count);
                        if (values[index] == dict[key] && !indicies.Contains(index)) indicies.Add(index); //정답 예외 처리
                    }

                    indicies.Add(index);
                    Console.WriteLine($"{j + 1}. {values[index]}");
                }

                indicies.Clear();

                Console.WriteLine();
                string num = Console.ReadLine();

                if (int.TryParse(num, out var n))
                {
                    if (n - 1 == real)
                    {
                        stats.SolvedCount++;

                        Console.WriteLine($"Correct! [+{stats.SolvedCount}]");
                        Console.ReadLine();
                    }
                    else
                    {
                        stats.WrongCount++;
                        stats.WrongKeys.Add(key);

                        Console.WriteLine($"Wrong. [+{stats.WrongCount}]");
                        Console.ReadLine();
                    }
                }
            }

            return stats;
        }
    }
}
