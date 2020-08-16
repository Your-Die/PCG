using System;

namespace Mutiny.Grammar
{
    public class GrammarParseException : Exception
    {
        public GrammarParseException(string message) : base(message)
        {
        }
    }
}