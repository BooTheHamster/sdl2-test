using Serilog;
using System;
using System.Collections.Generic;
using SDL2;
using System.IO;

namespace Sdl2Test.Services
{
    /// <summary>
    /// Провайдер изображений для текстур.
    /// </summary>
    sealed public class SurfaceProvider : ISurfaceProvider
    {
        private readonly ILogger _logger;
        private readonly IDictionary<string, IntPtr> _textures = new Dictionary<string, IntPtr>();

        public SurfaceProvider(ILogger logger)
        {
            _logger = logger;
            LoadImages();
        }

        public bool TryGetSurface(string imageIdent, out IntPtr surface)
        {
            if (_textures.TryGetValue(imageIdent, out surface))
            {
                return true;
            }

            _logger.Error("Не найдено изображение с идентификатором {textureIdent}", imageIdent);
            surface = IntPtr.Zero;
            return false;
        }

        public void Dispose()
        {
            foreach (var surface in _textures.Values)
            {
                SDL.SDL_FreeSurface(surface);
            }

            _textures.Clear();
        }

        private void LoadImages()
        {
            // Пока загружаем все файлы с расширением PNG из каталога Assets.
            // Идентификатором  будет являться наименование файла.
            var assetsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            assetsPath = Path.Combine(assetsPath, "Assets");

            foreach (var texturePath in Directory.GetFiles(assetsPath, "*.png", SearchOption.AllDirectories))
            {
                var texture = SDL_image.IMG_Load(texturePath);

                if (texture == IntPtr.Zero)
                {
                    _logger.Error("Ошибка при загруке файла изображения {texturePath}. Текстура не создана.", texturePath);
                    continue;
                }

                var textureIndent = Path.GetFileNameWithoutExtension(texturePath);
                _logger.Debug("Загружено изображение \"{textureIndent}\" из {texturePath}", textureIndent, texturePath);

                _textures.Add(textureIndent, texture);
            }
        }
    }
}
