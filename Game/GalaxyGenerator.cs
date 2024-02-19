using Sdl2Test.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sdl2Test.Game
{
    public class GalaxyGenerator
    {
        /// <summary>
        /// Множители для определения минимального и максимального числа звездных систем в зависимости от размера галактики.
        /// </summary>
        private const double MinStarSystemCountFactor = 0.0002;
        private const double MaxStarSystemCountFactor = 0.0003;

        /// <summary>
        /// Отступ от края в котором не создаются звездные системы.
        /// </summary>
        private const int BorderMargin = 10;

        /// <summary>
        /// Минимальное расстояние между системами.
        /// </summary>
        private const double MinDistanceBetweenStarSystems = 10.0;

        /// <summary>
        /// Карта соответствия размера галактики и ее размера.
        /// </summary>
        private static readonly IDictionary<GalaxySize, Size> GalaxySizeToSizeMap = new Dictionary<GalaxySize, Size>()
        {
            { GalaxySize.XS, new Size(100, 100) },
            { GalaxySize.S, new Size(200, 200) },
            { GalaxySize.M, new Size(500, 500) },
            { GalaxySize.L, new Size(700, 700) },
            { GalaxySize.XL, new Size(1000, 1000) }
        };

        /// <summary>
        /// Карта соответствия класса звезды звездной системы и процента ее на карте.
        /// </summary>
        private static readonly IDictionary<StarClass, double> StarSystemClasses = new Dictionary<StarClass, double>()
        {
            { StarClass.NormalClassO, 10 },
            { StarClass.NormalClassB, 10 },
            { StarClass.NormalClassA, 10 },
            { StarClass.NormalClassF, 15 },
            { StarClass.NormalClassG, 30 },
            { StarClass.NormalClassK, 15 },
            { StarClass.NormalClassM, 10 }
        };

        private readonly Random _random = new(Guid.NewGuid().GetHashCode());

        public IEnumerable<StarSystem> CreateGalaxy(GalaxySize galaxySize, ILogger logger)
        {
            var result = new List<StarSystem>();
            var size = GalaxySizeToSizeMap[galaxySize];
            var minStarSystemCount = (int)Math.Round(size.Width * size.Height * MinStarSystemCountFactor);
            var maxStarSystemCount = (int)Math.Round(size.Width * size.Height * MaxStarSystemCountFactor);
            var allStarSystemCount = _random.Next(minStarSystemCount, maxStarSystemCount);
            var starSystemCounts = new Dictionary<StarClass, int>();

            logger.Debug("Создается {AllStarSystemCount} звездных систем {{{MinStarSystemCount}, {MaxStarSystemCount}}}", allStarSystemCount, minStarSystemCount, maxStarSystemCount);

            var total = 0;
            foreach (var pair in StarSystemClasses)
            {
                var count = (int) Math.Round(allStarSystemCount * pair.Value / 100.0);
                total += count;
                starSystemCounts.Add(pair.Key, count);
            }

            size.Width -= BorderMargin;
            size.Height -= BorderMargin;

            var starCoordinates = new Queue<Point>();

            do
            {
                for (; total > 0 ; total--)
                {
                    var coordinates = new Point(
                        _random.Next(BorderMargin, size.Width),
                        _random.Next(BorderMargin, size.Height));
                        
                    starCoordinates.Enqueue(coordinates);
                }

            } while (false);

            foreach (var pair in starSystemCounts)
            {
                for (var i = pair.Value; i > 0; i--)
                {
                    if (starCoordinates.Count > 0)
                    {
                        result.Add(new StarSystem(starCoordinates.Dequeue(), pair.Key));
                    }
                }
            }

            return result;
        }
    }
}
