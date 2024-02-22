using Sdl2Test.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sdl2Test.Game;

public static class GalaxyGenerator
{
    /// <summary>
    /// Отступ от края в котором не создаются звездные системы.
    /// </summary>
    private const int BorderMargin = 10;

    /// <summary>
    /// Минимальное расстояние между системами.
    /// </summary>
    private const double MinDistanceBetweenStarSystems = 10.0;
    
    /// <summary>
    /// Карта соответствия размера галактики и числа зведных систем в ней.
    /// </summary>
    private static readonly Dictionary<GalaxySize, int> GalaxySizeToSizeMap = new()
    {
        { GalaxySize.XS, 50 },
        { GalaxySize.S, 100 },
        { GalaxySize.M, 250 },
        { GalaxySize.L, 350 },
        { GalaxySize.XL, 500 }
    };

    /// <summary>
    /// Карта соответствия класса звезды звездной системы и вероятности появления ее на карте.
    /// </summary>
    private static readonly Dictionary<StarClass, double> StarSystemClasses = new()
    {
        { StarClass.NormalClassO, 5 },
        { StarClass.NormalClassB, 10 },
        { StarClass.NormalClassA, 10 },
        { StarClass.NormalClassF, 15 },
        { StarClass.NormalClassG, 30 },
        { StarClass.NormalClassK, 15 },
        { StarClass.NormalClassM, 10 },
        { StarClass.BrownDwarf, 10 },
        { StarClass.WhiteDwarf, 10 },
        { StarClass.RedGiant, 10 }
    };

    private static readonly Random Random = new(Guid.NewGuid().GetHashCode());

    public static IEnumerable<StarSystem> CreateGalaxy(GalaxySize galaxySize, double minCoordinate, double maxCoordinate)
    {
        var result = new List<StarSystem>();
        var totalPercents = StarSystemClasses.Values.Sum(d => d);

        foreach (var pair in StarSystemClasses)
        {
            var count = Convert.ToInt32(Math.Floor(pair.Value / totalPercents * GalaxySizeToSizeMap[galaxySize]));

            if (count < 1)
            {
                count = 1;
            }

            for (var i = 0; i < count; i++)
            {
                // Считаем что все звездные системы расположены в квадрате. 
                var x = GetRandomPoint(minCoordinate, maxCoordinate);
                var y = GetRandomPoint(minCoordinate, maxCoordinate);
                
                result.Add(new StarSystem(x, y, pair.Key));
            }
        }

        return result;
    }

    private static double GetRandomPoint(double minCoordinate, double maxCoordinate)
    {
        return Random.NextDouble() * (Math.Abs(minCoordinate) + Math.Abs(maxCoordinate)) + minCoordinate;
    }
}