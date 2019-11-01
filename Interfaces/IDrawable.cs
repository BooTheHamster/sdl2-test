namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Сущность поддерживающая отрисовку.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Отрисовка сущности.
        /// </summary>
        /// <param name="drawService">Сервис отрисовки сущностей.</param>
        void Draw(IDrawService drawService);
    }
}