using System.Drawing;

namespace Sdl2Test.Models
{
    public class StarSystem
    {
        public readonly Point Coordinates;
        
        public readonly StarClass StarClass;

        public StarSystem(
            Point coordinates,
            StarClass starClass)
        {
            Coordinates = coordinates;
            StarClass = starClass;
        }
    }
}
