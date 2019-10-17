using Sdl2Test.Interfaces;
using Serilog;
using System;
using SDL2;

namespace Sdl2Test.Services
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    public class GraphicsService : IGraphicsService
    {
        private readonly ILogger logger;
        private IntPtr window = IntPtr.Zero;
        private IntPtr renderer = IntPtr.Zero;

        private bool HasWindow => window != IntPtr.Zero;
        private bool HasRender => renderer != IntPtr.Zero;

        private bool CanDraw => HasWindow && HasRender;

        public GraphicsService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            if (HasRender)
            {
                SDL.SDL_DestroyRenderer(renderer);
            }

            if (HasWindow)
            {
                SDL.SDL_DestroyWindow(window);
            }

            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }

        public bool TryInitialize()
        {
            var result = InitializeSdl() 
                && CreateWindow()
                && CreateRenderer();

            var positiveTexture = SDL_image.IMG_Load("Assets/positive.png");
            var negativeTexture = SDL_image.IMG_Load("Assets/negative.png");

            return result;
        }

        public void Draw()
        {
            if (!CanDraw)
            {
                return;
            }

            SDL.SDL_SetRenderDrawColor(renderer, 0xcb, 0xc6, 0xaf, 0xff);
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_RenderPresent(renderer);
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

            var width = (int) Math.Floor(displayMode.w * 0.3);
            var height = (int) Math.Floor(displayMode.h * 0.7);
            var x = (displayMode.w - width) / 2;
            var y = (displayMode.h - height) / 2;

            window = SDL.SDL_CreateWindow(
                windowTitle,
                x,
                y,
                width,
                height,
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
            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (HasRender)
            {
                return true;
            }

            LogSdlError("Ошибка при создании рендерера SDL.");
            return false;
        }

        private void LogSdlError(string message)
        {
            logger.Error(message);
            logger.Error("Ошибка SDL: {0}", SDL.SDL_GetError());
        }
    }
}
