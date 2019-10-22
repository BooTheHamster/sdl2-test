using System;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Спрайт.
    /// </summary>
    public interface ISprite : IDisposable
    {
        /// <summary>
        /// Отрисовывает спрайт в указанных координатах.
        /// </summary>
        /// <param name="x">Координата X в пикселях.</param>
        /// <param name="y">Координата Y в пикселях.</param>
        void DrawAt(int x, int y);
    }
}