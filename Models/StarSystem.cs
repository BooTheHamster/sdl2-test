using System.Drawing;

namespace Sdl2Test.Models
{
    public class StarSystem(
        Point coordinates,
        StarClass starClass)
    {
        public readonly Point Coordinates = coordinates;
        
        public readonly StarClass StarClass = starClass;
    }
}
