namespace Chinchillada.Generation.Grid
{
    public static class HexMath
    {
        public static int Distance(Hex x, Hex y)
        {
            var difference = x - y;
            return difference.Length;
        }
    }
}