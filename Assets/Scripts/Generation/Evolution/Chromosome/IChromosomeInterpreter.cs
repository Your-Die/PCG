namespace Chinchillada.Generation.Evolution.Chromosome
{
    public interface IChromosomeInterpreter
    {
        int ChromosomeLength { get; }
        
        void Interpret(Chromosome chromosome);
    }

    public static class ChromosomeInterpreter
    {
        public static Chromosome CreateEmptyChromosome(this IChromosomeInterpreter interpreter)
        {
            return new Chromosome(interpreter.ChromosomeLength);
        }
    }
}