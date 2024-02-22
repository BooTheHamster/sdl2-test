using Sdl2Test.Core;
using Sdl2Test.Drawables;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;
using System.Collections.Generic;
using Sdl2Test.Services;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        /// <summary>
        /// Интевал координат в котором генерируются звездные системы.
        /// </summary>
        private const double MinCoordinate = -50;
        private const double MaxCoordinate = 50;

        private readonly IGraphicsService _graphicsService;
        private readonly GameEntityUpdateEngine _engine;
        private readonly StarSpriteProvider _starSpriteProvider;
        private readonly IDrawCoordinatesConverter _drawCoordinatesConverter;
        private IEnumerable<StarSystem> _galaxyStarSystems;

        public GameEngine(
            IGraphicsService graphicsService,
            GameEntityUpdateEngine engine)
        {
            _graphicsService = graphicsService;
            _engine = engine;
            _starSpriteProvider = new StarSpriteProvider(_graphicsService);
            _drawCoordinatesConverter = new DrawCoordinatesConverter(graphicsService, MinCoordinate, MaxCoordinate);
        }

        public void CreateNew()
        {
            _galaxyStarSystems = GalaxyGenerator.CreateGalaxy(GalaxySize.XS, MinCoordinate, MaxCoordinate);

            foreach (var starSystem in _galaxyStarSystems)
            {
                var sprite = _starSpriteProvider.GetSprite(starSystem.StarClass);
                var drawable = new StarSystemDrawable(starSystem, sprite, _drawCoordinatesConverter);

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
