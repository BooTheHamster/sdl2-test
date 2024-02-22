namespace Sdl2Test.Interfaces;

/// <summary>
/// Сервис конвертации координат в координаты для отрисовки.
/// </summary>
public interface IDrawCoordinatesConverter
{
    void Convert(double x, double y, out int drawX, out int drawY);
}