using System;
using System.Drawing;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    public interface IGraphicsService : IDrawable, IDisposable
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

        /// <summary>
        /// Добавление отрисовываемого объекта.
        /// </summary>
        /// <param name="drawable">Объект.</param>
        void Add(IDrawable drawable);

        /// <summary>
        /// Удаление отрисовываемого объекта.
        /// </summary>
        /// <param name="drawable">Объект.</param>
        void Remove(IDrawable drawable);

        /// <summary>
        /// Возвращает текущие размеры окна приложения.
        /// </summary>
        /// <returns>Размеры окна.</returns>
        Size GetWindowDimensions();
    }
}
