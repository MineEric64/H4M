using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H4M
{
    public class GameStats
    {
        public int SolvedCount { get; set; }
        public int WrongCount { get; set; }

        public List<string> WrongKeys { get; set; } = new List<string>();

        public GameStats(int solvedCount, int wrongCount)
        {
            SolvedCount = solvedCount;
            WrongCount = wrongCount;
        }

        public GameStats(int solvedCount, int wrongCount, IEnumerable<string> wrongKeys) : this(solvedCount, wrongCount)
        {
            WrongKeys.Clear();
            WrongKeys.AddRange(wrongKeys);
        }
    }
}
