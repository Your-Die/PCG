namespace Chinchillada.Generation
{
    public interface IGenerator<out T>
    {
        T Generate();
    }
}