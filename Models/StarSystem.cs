namespace Sdl2Test.Models;

public class StarSystem(
    double x,
    double y,
    StarClass starClass)
{
    public double X { get; private set; } = x;

    public  double Y { get; private set; } = y;
        
    public readonly StarClass StarClass = starClass;

    public void Update(double x, double y)
    {
        X = x;
        Y = y;
    } 
}