using System;
using Sdl2Test.Core;
using Sdl2Test.Game;
using Sdl2Test.Services;

namespace Sdl2Test;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Program
{
    private static void Main(string[] args)
    {
        var logger = LoggerFactory.GetLogger();
        logger.Information("===== Start =====");

        using (var graphicsService = new GraphicsService(logger))
        {
            if (graphicsService.TryInitialize())
            {
                var moveEngine = new GameEntityUpdateEngine();
                var gameEngine = new GameEngine(graphicsService, moveEngine);

                gameEngine.CreateNew();

                try
                {

                    var quit = false;

                    while (!quit)
                    {
                        while (SDL2.SDL.SDL_PollEvent(out var sdlEvent) != 0)
                        {
                            if (sdlEvent.type != SDL2.SDL.SDL_EventType.SDL_QUIT)
                            {
                                continue;
                            }
                            
                            quit = true;
                            break;
                        }

                        gameEngine.Update();
                    }
                }
                catch (Exception error)
                {
                    logger.Error(error, "Unknown error");
                }
            }
        }

        logger.Information("===== Stopped =====");
    }
}