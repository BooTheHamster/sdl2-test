using System;

namespace Sdl2Test.Interfaces
{
    /// <summary>
    /// Провайдер изображений для текстур.
    /// </summary>
    public interface ISurfaceProvider : IDisposable
    {
        /// <summary>
        /// Возвращает изображение для текстуры по идентификатору.
        /// </summary>
        /// <param name="imageIdent">Идентификатор изображения.</param>
        /// <param name="surface">Изображение для текстуры.</param>
        /// <returns>Истину изображение для текстуры получено.</returns>
        bool TryGetSurface(string imageIdent, out IntPtr surface);
    }
}