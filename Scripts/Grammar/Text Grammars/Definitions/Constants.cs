namespace Mutiny.Grammar
{
    using Chinchillada.Foundation;
    using UnityEngine;

    [DefaultExecutionOrder(-1)]
    public class Constants : SingletonBehaviour<Constants>
    {
        [SerializeField] private char symbolGuard = '#';

        [SerializeField] private char actionOpen = '[';
        [SerializeField] private char actionClose = ']';

        [SerializeField] private  string originSymbol = "origin";

        [SerializeField] private string parameterRegex = "(\\[)([\\w\\s]+)";
        [SerializeField] private string symbolRegex;
        
        public static char SymbolGuard => Instance.symbolGuard;
        
        public static string OriginSymbol => Instance.originSymbol;

        public static char ActionOpen => Instance.actionOpen;

        public static char ActionClose => Instance.actionClose;

        public static string ParameterRegex => Instance.parameterRegex;
        public static string SymbolRegex => Instance.symbolRegex;
    }
}