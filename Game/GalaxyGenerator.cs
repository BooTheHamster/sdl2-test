using Sdl2Test.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sdl2Test.Game
{
    public class GalaxyGenerator
    {
        /// <summary>
        /// Множители для определения минимального и максимального числа звездных систем в зависимости от размера галактики.
        /// </summary>
        private const double _minStarSystemCountFactor = 0.0002;
        private const double _maxStarSystemCountFactor = 0.0003;

        /// <summary>
        /// Отступ от края в котором не создаются звездные системы.
        /// </summary>
        private const int _borderMargin = 10;

        /// <summary>
        /// Минимальное расстояние между системами.
        /// </summary>
        private const double _minDistanceBetweenStarSystems = 10.0;

        /// <summary>
        /// Карта соответствия размера галактики и ее размера.
        /// </summary>
        private static readonly IDictionary<GalaxySize, Size> _galaxySizeToSizeMap = new Dictionary<GalaxySize, Size>()
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
        private static readonly IDictionary<StarClass, double> _starSystemClasses = new Dictionary<StarClass, double>()
        {
            { StarClass.NormalClassO, 10 },
            { StarClass.NormalClassB, 10 },
            { StarClass.NormalClassA, 10 },
            { StarClass.NormalClassF, 15 },
            { StarClass.NormalClassG, 30 },
            { StarClass.NormalClassK, 15 },
            { StarClass.NormalClassM, 10 }
        };

        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public IEnumerable<StarSystem> CreateGalaxy(GalaxySize galaxySize, ILogger logger)
        {
            var result = new List<StarSystem>();
            var size = _galaxySizeToSizeMap[galaxySize];
            var minStarSystemCount = (int)Math.Round(size.Width * size.Height * _minStarSystemCountFactor);
            var maxStarSystemCount = (int)Math.Round(size.Width * size.Height * _maxStarSystemCountFactor);
            var allStarSystemCount = _random.Next(minStarSystemCount, maxStarSystemCount);
            var starSystemCounts = new Dictionary<StarClass, int>();

            logger.Debug($"Создается {allStarSystemCount} звездных систем {{{minStarSystemCount}, {maxStarSystemCount}}}.");

            var total = 0;
            foreach (var pair in _starSystemClasses)
            {
                var count = (int) Math.Round(allStarSystemCount * pair.Value / 100.0);
                total += count;
                starSystemCounts.Add(pair.Key, count);
            }

            size.Width -= _borderMargin;
            size.Height -= _borderMargin;

            var starCoordinates = new Queue<Point>();

            do
            {
                for (; total > 0 ; total--)
                {
                    var coordinates = new Point(
                        _random.Next(_borderMargin, size.Width),
                        _random.Next(_borderMargin, size.Height));
                        
                    starCoordinates.Enqueue(coordinates);
                }

            } while (total > 0);

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
