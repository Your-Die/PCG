namespace Chinchillada.Generation.Grid
{
    public interface IRenderer<in T>
    {
        void Render(T content);
    }
}