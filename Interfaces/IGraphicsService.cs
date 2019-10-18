using System;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    interface IGraphicsService : IDrawable, IDisposable
    {
        /// <summary>
        /// Инициализация графической подсистемы.
        /// </summary>
        /// <returns>Истина, если графическая подсистема инициализирована успешно.</returns>
        bool TryInitialize();

        /// <summary>
        /// Создает спрайт.
        /// </summary>
        /// <param name="width">Ширина спрайта в пикселях.</param>
        /// <param name="height">Высота спрайта в пикселях.</param>
        /// <param name="imageIdent">Идентификатор изображения спрайта.</param>
        /// <returns>Спрайт.</returns>
        ISprite CreateSprite(int width, int height, string imageIdent);
    }
}
