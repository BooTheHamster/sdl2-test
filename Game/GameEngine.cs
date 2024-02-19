using Sdl2Test.Core;
using Sdl2Test.Drawables;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;
using Serilog;
using System.Collections.Generic;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private readonly IGraphicsService _graphicsService;
        private readonly GameEntityUpdateEngine _engine;
        private readonly ILogger _logger;
        private readonly StarSpriteProvider _starSpriteProvider;
        private readonly GalaxyGenerator _galaxyGenerator = new();
        private IEnumerable<StarSystem> _galaxyStarSystems;

        public GameEngine(
            IGraphicsService graphicsService,
            GameEntityUpdateEngine engine,
            ILogger logger)
        {
            _graphicsService = graphicsService;
            _engine = engine;
            _logger = logger;
            _starSpriteProvider = new StarSpriteProvider(_graphicsService);
        }

        public void CreateNew()
        {
            _galaxyStarSystems = _galaxyGenerator.CreateGalaxy(GalaxySize.M, _logger);

            foreach (var starSystem in _galaxyStarSystems)
            {
                var sprite = _starSpriteProvider.GetSprite(starSystem.StarClass);
                var drawable = new StarSystemDrawable(starSystem, sprite);

                _graphicsService.Add(drawable);
            }
        }

        public void Update()
        {
            _engine.Update();
            _graphicsService.DrawAll();
        }
    }
}
