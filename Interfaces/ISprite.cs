using System;
using System.Drawing;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Спрайт.
    /// </summary>
    public interface ISprite : IDisposable
    {
        /// <summary>
        /// Отрисовывает спрайт.
        /// </summary>
        /// <param name="targetRectangle">Прямоугольник, задающий координаты и размеры области отрисовки спрайта.</param>
        void Draw(Rectangle targetRectangle);
    }
}