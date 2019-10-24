using Sdl2Test.Core;
using Sdl2Test.Game;
using Sdl2Test.Interfaces;
using Sdl2Test.Model;
using Sdl2Test.Services;

namespace Sdl2Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory.GetLogger();
            logger.Information("===== Запуск =====");

            using (IGraphicsService graphicsService = new GraphicsService(logger))
            {
                if (graphicsService.TryInitialize())
                {
                    var blockMoveEngine = new BlockMoveEngine();
                    var gameEngine = new GameEngine(graphicsService, blockMoveEngine);

                    try
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

                            gameEngine.Update();
                        }
                    }
                    finally
                    {
                        blockMoveEngine = null;
                    }
                }
            }

            logger.Information("===== Останов =====");
        }
    }
}
