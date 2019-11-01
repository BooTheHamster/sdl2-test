using Sdl2Test.Interfaces;
using Serilog;
using System;
using SDL2;
using Sdl2Test.Graphics;
using System.Collections.Generic;
using System.Drawing;

namespace Sdl2Test.Services
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    sealed public class GraphicsService : IGraphicsService
    {
        private static readonly Color BackgroundColor = Color.Black;
        private const double DefaultSizePercent = 0.95;
        private readonly ILogger _logger;
        private ISurfaceProvider _surfaceProvider;
        private IntPtr _window = IntPtr.Zero;
        private IntPtr _renderer = IntPtr.Zero;
        private readonly IList<IDrawable> _drawables = new List<IDrawable>();
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        private Size _windowSize = new Size();
        private Point _cameraPosition;

        private bool HasWindow => _window != IntPtr.Zero;
        private bool HasRender => _renderer != IntPtr.Zero;
        private bool CanDraw => HasWindow && HasRender;

        public Rectangle DrawRectangle => throw new NotSupportedException();

        public GraphicsService(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            if (HasRender)
            {
                SDL.SDL_DestroyRenderer(_renderer);
            }

            if (HasWindow)
            {
                SDL.SDL_DestroyWindow(_window);
            }

            _surfaceProvider.Dispose();

            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }

        public bool TryInitialize()
        {
            var result = InitializeSdl() 
                && CreateWindow()
                && CreateRenderer();

            _surfaceProvider = new SurfaceProvider(_logger);
            _cameraPosition = new Point(0, 0);

            return result;
        }

        public void DrawAll()
        {
            if (!CanDraw)
            {
                return;
            }

            SDL.SDL_SetRenderDrawColor(_renderer, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);
            SDL.SDL_RenderClear(_renderer);

            foreach (var drawable in _drawables)
            {
                drawable.Draw(this);
            }

            SDL.SDL_RenderPresent(_renderer);
        }

        public ISprite CreateSprite(int width, int height, string imageIdent)
        {
            if (!HasRender)
            {
                return null;
            }

            if (!_surfaceProvider.TryGetSurface(imageIdent, out IntPtr surface))
            {
                return null;
            }

            var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);

            if (texture == IntPtr.Zero)
            {
                LogSdlError($"Ошибка при создании текстуры {imageIdent}");
                return null;
            }

            var sprite = new Sprite(width, height, _renderer, texture);
            _disposables.Add(sprite);

            return sprite;
        }

        public void Add(IDrawable drawable)
        {
            _drawables.Add(drawable);
        }

        public void Remove(IDrawable drawable)
        {
            _drawables.Remove(drawable);
        }

        public Size GetWindowDimensions()
        {
            return new Size(_windowSize.Width, _windowSize.Height);
        }

        public void Draw(Rectangle drawRect, ISprite sprite)
        {
            var viewportRect = GetViewportRect();
            if (!viewportRect.IntersectsWith(drawRect))
            {
                return;
            }

            drawRect.Offset(drawRect.X - viewportRect.X, drawRect.Y - viewportRect.Y);
            sprite.Draw(drawRect);
        }

        private bool InitializeSdl()
        {
            int result;
            try
            {
                result = SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            }
            catch (DllNotFoundException)
            {
                LogSdlError("Не найдена SDL2.dll в каталоге приложения.");
                return false;
            }

            if (result != 0)
            {
                LogSdlError("Ошибка инциализации SDL");
                return false;
            }

            result = SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

            if (((int)SDL_image.IMG_InitFlags.IMG_INIT_PNG & result) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
            {
                LogSdlError("Ошибка инциализации SDL Image");
                return false;
            }

            return true;
        }

        private bool CreateWindow()
        {
            var windowTitle = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";

            if (SDL.SDL_GetDesktopDisplayMode(0, out SDL.SDL_DisplayMode displayMode) != 0)
            {
                LogSdlError("Ошибка получения режима работы экрана.");
                return false;
            }

            _windowSize.Width = (int) Math.Floor(displayMode.w * DefaultSizePercent);
            _windowSize.Height = (int) Math.Floor(displayMode.h * DefaultSizePercent);
            var x = (displayMode.w - _windowSize.Width) / 2;
            var y = (displayMode.h - _windowSize.Height) / 2;

            _window = SDL.SDL_CreateWindow(
                windowTitle,
                x,
                y,
                _windowSize.Width,
                _windowSize.Height,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS);

            if (HasWindow)
            {
                return true;
            }

            LogSdlError("Ошибка при создании окна SDL.");
            return false;
        }

        private bool CreateRenderer()
        {
            _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (HasRender)
            {
                return true;
            }

            LogSdlError("Ошибка при создании рендерера SDL.");
            return false;
        }

        private void LogSdlError(string message)
        {
            _logger.Error(message);
            _logger.Error("Ошибка SDL: {0}", SDL.SDL_GetError());
        }

        private Rectangle GetViewportRect()
        {
            return new Rectangle(_cameraPosition.X, _cameraPosition.Y, _windowSize.Width, _windowSize.Height);
        }
    }
}
