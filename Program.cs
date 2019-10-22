using Sdl2Test.Core;
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
                    var positiveSprite = graphicsService.CreateSprite(10, 10, "positive");
                    var negativeSprite = graphicsService.CreateSprite(10, 10, "negative");

                    using (var gameEngine = new GameEngine(logger))
                    {
                        var block = new Block(positiveSprite, 0, 0, 0, 0.1);
                        gameEngine.Add(block);
                        graphicsService.Add(block);

                        block = new Block(positiveSprite, 20, 0, 0, 0.2);
                        gameEngine.Add(block);
                        graphicsService.Add(block);

                        block = new Block(negativeSprite, 40, 0, 0, 0.05);
                        gameEngine.Add(block);
                        graphicsService.Add(block);

                        block = new Block(negativeSprite, 60, 0, 0, 0.1);
                        gameEngine.Add(block);
                        graphicsService.Add(block);


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
                            graphicsService.Draw();
                        }
                    }
                }
            }

            logger.Information("===== Останов =====");
        }
    }
}
