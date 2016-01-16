using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetrunnerDb.Net;

namespace NetrunnerdbToJinteki
{
    public class Program
    {
        [STAThread()]
        public static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Input id of decklist");
                Environment.Exit(1);
            }
            var stringBuilder = new StringBuilder();
            new Repository()
                .GetDecklist(args[0])
                .First()
                .Cards
                .Select(a => new { Card = new Repository().GetCard(a.Key).First(), Count = a.Value })
                .Where(a => !string.Equals(a.Card.Type, "Identity", StringComparison.OrdinalIgnoreCase))
                .ForEach(a => stringBuilder.AppendLine($"{a.Count} {a.Card.Title}"));

            Clipboard.SetText(stringBuilder.ToString());
        }
    }
}