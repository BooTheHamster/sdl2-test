using Sdl2Test.Services;
using System;

namespace Sdl2Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory.GetLogger();
            logger.Information("===== Запуск =====");

            int result;
            try
            {
                result = SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_EVERYTHING);
            }
            catch (DllNotFoundException)
            {
                logger.Error("Не найдена SDL2.dll в каталоге приложения.");
                return;
            }

            if (result != 0)
            {
                return;
            }

            var windowTitle = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";
            var window = SDL2.SDL.SDL_CreateWindow(
                windowTitle,
                0,
                0,
                640,
                480,
                SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                return;
            }

            var quit = false;

            while (!quit)
            {
                while (SDL2.SDL.SDL_PollEvent(out SDL2.SDL.SDL_Event sdlEvent) != 0)
                {
                    if (sdlEvent.type == SDL2.SDL.SDL_EventType.SDL_QUIT)
                    {
                        quit = true;
                        break;
                    }
                }
            }

            SDL2.SDL.SDL_DestroyWindow(window);
            SDL2.SDL.SDL_Quit();
            logger.Information("===== Останов =====");
        }
    }
}
