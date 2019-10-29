using Sdl2Test.Core;
using Sdl2Test.Interfaces;
using System;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private const double StartBlockVelocity = 50;

        private readonly IGraphicsService _graphicsService;
        private readonly GameEntityUpdateEngine _engine;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public GameEngine(
            IGraphicsService graphicsService,
            GameEntityUpdateEngine engine)
        {
            _graphicsService = graphicsService;
            _engine = engine;
        }

        public void Update()
        {
            _engine.Update();
            _graphicsService.Draw();
        }
    }
}
