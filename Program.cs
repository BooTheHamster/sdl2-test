using Sdl2Test.Interfaces;
using Sdl2Test.Services;

namespace Sdl2Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory.GetLogger();
            logger.Information("===== Запуск =====");

            using (var graphicsService = new GraphicsService(logger) as IGraphicsService)
            {
                if (graphicsService.TryInitialize())
                {
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

                        graphicsService.Draw();
                    }
                }
            }

            logger.Information("===== Останов =====");
        }
    }
}
