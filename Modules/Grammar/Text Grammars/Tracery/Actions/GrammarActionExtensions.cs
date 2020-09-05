using System.Collections.Generic;
using System.Text;

namespace Mutiny.Grammar
{
    public static class GrammarActionExtensions
    {
        public static string ToJSON(this IEnumerable<TraceryAction> actions)
        {
            var stringBuilder = new StringBuilder();

            foreach (var action in actions) 
                stringBuilder.Append(action);

            return stringBuilder.ToString();
        }
    }
}