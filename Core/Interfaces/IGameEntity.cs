using System;

namespace Sdl2Test.Core.Interfaces
{
    /// <summary>
    /// Игровая сущность.
    /// </summary>
    public interface IGameEntity
    {
        /// <summary>
        /// Обновление сущности.
        /// </summary>
        /// <param name="elapsedTime">Интервал времени прошедший с последнего обновления.</param>
        void Update(TimeSpan elapsedTime);
    }
}