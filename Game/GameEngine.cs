using Sdl2Test.Core;
using Sdl2Test.Interfaces;
using System;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private readonly IGraphicsService _graphicsService;
        private readonly GameEntityUpdateEngine _engine;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly StarSpriteProvider _starSpriteProvider;

        public GameEngine(
            IGraphicsService graphicsService,
            GameEntityUpdateEngine engine)
        {
            _graphicsService = graphicsService;
            _engine = engine;

            _starSpriteProvider = new StarSpriteProvider(_graphicsService);
        }

        public void Update()
        {
            _engine.Update();
            _graphicsService.Draw();
        }
    }
}
