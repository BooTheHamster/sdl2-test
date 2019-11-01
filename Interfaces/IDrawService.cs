using System.Drawing;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Сервис отрисовки сущностей.
    /// </summary>
    public interface IDrawService
    {
        /// <summary>
        /// Отрисовка спрайта в указанных координатах.
        /// </summary>
        /// <param name="drawRect">Область отрисовки спрайта.</param>
        /// <param name="sprite">Спрайт.</param>
        void Draw(Rectangle drawRect, ISprite sprite);
    }
}