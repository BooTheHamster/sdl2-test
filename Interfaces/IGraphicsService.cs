using System;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    interface IGraphicsService : IDisposable
    {
        /// <summary>
        /// Инициализация графической подсистемы.
        /// </summary>
        /// <returns>Истина, если графическая подсистема инициализирована успешно.</returns>
        bool TryInitialize();

        /// <summary>
        /// Отрисовка.
        /// </summary>
        void Draw();
    }
}
