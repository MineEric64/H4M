using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H4M
{
    public class DictionaryManager
    {
        /// <summary>
        /// Identifier [;]
        /// </summary>
        public const string IDENTIFIER = ";";

        public static Dictionary<string, string> GetDictionaryFromText(string text)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string[] texts = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string x in texts)
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    continue;
                }

                string[] pair = x.Split(new[] { IDENTIFIER }, StringSplitOptions.None);

                if (pair.Length >= 2)
                {
                    string key = pair[0].Trim();
                    string value = string.Join(";", pair.ToList().GetRange(1, pair.Length - 1)).Trim();

                    dict.Add(key, value);
                    //dict.Add(value, key);
                }
            }

            return dict;
        }
    }
}
