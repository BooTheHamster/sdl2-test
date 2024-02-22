using System;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Services;

/// <summary>
/// Сервис преобразования координат.
/// </summary>
internal sealed class DrawCoordinatesConverter : IDrawCoordinatesConverter
{
    private readonly IGraphicsService _graphicsService;
    private readonly double _minCoordinate;
    private readonly double _maxCoordinate;
    private int _centerPointX;
    private int _centerPointY;
    private double _coefficient;

    public DrawCoordinatesConverter(IGraphicsService graphicsService, double minCoordinate, double maxCoordinate)
    {
        _graphicsService = graphicsService;
        _minCoordinate = minCoordinate;
        _maxCoordinate = maxCoordinate;
        
        Calculate();
    }
    
    public void Convert(double x, double y, out int drawX, out int drawY)
    {
        drawX = System.Convert.ToInt32(Math.Floor(x * _coefficient)) + _centerPointX;
        drawY = System.Convert.ToInt32(Math.Floor(y * _coefficient)) + _centerPointY;
    }

    private void Calculate()
    {
        var size = _graphicsService.GetWindowDimensions();
        var sizeMin = Math.Min(size.Width, size.Height);
        
        _centerPointX = size.Width / 2;
        _centerPointY = size.Height / 2;
        _coefficient = sizeMin / (Math.Abs(_minCoordinate) + Math.Abs(_maxCoordinate));
    }
}