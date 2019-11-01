using Sdl2Test.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sdl2Test.Game
{
    public class GalaxyGenerator
    {
        private static readonly IDictionary<GalaxySize, Size> _galaxySizeToSizeMap = new Dictionary<GalaxySize, Size>()
        {
            { GalaxySize.XS, new Size(100, 100) },
            { GalaxySize.S, new Size(200, 200) },
            { GalaxySize.M, new Size(500, 500) },
            { GalaxySize.L, new Size(700, 700) },
            { GalaxySize.XL, new Size(1000, 1000) }
        };

        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public IEnumerable<StarSystem> CreateGalaxy(GalaxySize galaxySize, int starSystemCount)
        {
            var result = new List<StarSystem>();
            var size = _galaxySizeToSizeMap[galaxySize];
            var coordinates = new Point(
                _random.Next(0, size.Width),
                _random.Next(0, size.Height));
            coordinates = new Point(0, 0);

            result.Add(new StarSystem(coordinates, StarClass.NormalClassG));

            return result;
        }
    }
}
