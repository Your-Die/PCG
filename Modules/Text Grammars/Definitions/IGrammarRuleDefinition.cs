using System.Collections.Generic;

namespace Chinchillada
{
    public interface IGrammarRuleDefinition
    {
        string Symbol { get; }
        List<string> Replacements { get; }
    }
}