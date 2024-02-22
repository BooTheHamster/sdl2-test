using Serilog;
using System;
using System.Collections.Generic;
using SDL2;
using System.IO;

namespace Sdl2Test.Services;

/// <summary>
/// Провайдер изображений для текстур.
/// </summary>
internal sealed class SurfaceProvider : IDisposable
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, IntPtr> _textures = new();

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

        _logger.Error("Texture {TextureIdent} not found", imageIdent);
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
        assetsPath = Path.Combine(assetsPath ?? "./", "Assets");

        if (!Directory.Exists(assetsPath))
        {
            _logger.Error("No assets path");
            return;
        }

        foreach (var texturePath in Directory.GetFiles(assetsPath, "*.png", SearchOption.AllDirectories))
        {
            var texture = SDL_image.IMG_Load(texturePath);

            if (texture == IntPtr.Zero)
            {
                _logger.Error("Texture {TexturePath} load error", texturePath);
                continue;
            }

            var textureIndent = Path.GetFileNameWithoutExtension(texturePath);
            _logger.Debug("Loaded texture \"{TextureIndent}\" from file {TexturePath}", textureIndent, texturePath);

            _textures.Add(textureIndent, texture);
        }
    }
}