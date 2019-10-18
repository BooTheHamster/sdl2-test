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
        private readonly ILogger logger;
        private readonly IDictionary<string, IntPtr> textures = new Dictionary<string, IntPtr>();

        public SurfaceProvider(ILogger logger)
        {
            this.logger = logger;
            LoadImages();
        }

        public bool TryGetSurface(string imageIdent, out IntPtr surface)
        {
            if (textures.TryGetValue(imageIdent, out surface))
            {
                return true;
            }

            logger.Error("Не найдено изображение с идентификатором {textureIdent}", imageIdent);
            surface = IntPtr.Zero;
            return false;
        }

        public void Dispose()
        {
            foreach (var surface in textures.Values)
            {
                SDL.SDL_FreeSurface(surface);
            }

            textures.Clear();
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
                    logger.Error("Ошибка при загруке файла изображения {texturePath}. Текстура не создана.", texturePath);
                    continue;
                }

                var textureIndent = Path.GetFileNameWithoutExtension(texturePath);
                logger.Debug("Загружено изображение \"{textureIndent}\" из {texturePath}", textureIndent, texturePath);

                textures.Add(textureIndent, texture);
            }
        }
    }
}
