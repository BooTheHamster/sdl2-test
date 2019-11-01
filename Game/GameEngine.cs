using Sdl2Test.Core;
using Sdl2Test.Drawables;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;
using System.Collections.Generic;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private readonly IGraphicsService _graphicsService;
        private readonly GameEntityUpdateEngine _engine;
        private readonly StarSpriteProvider _starSpriteProvider;
        private readonly GalaxyGenerator _galaxyGenerator = new GalaxyGenerator();
        private IEnumerable<StarSystem> _galaxyStarSystems;

        public GameEngine(
            IGraphicsService graphicsService,
            GameEntityUpdateEngine engine)
        {
            _graphicsService = graphicsService;
            _engine = engine;

            _starSpriteProvider = new StarSpriteProvider(_graphicsService);
        }

        public void CreateNew()
        {
            _galaxyStarSystems = _galaxyGenerator.CreateGalaxy(GalaxySize.XS, 10);

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
