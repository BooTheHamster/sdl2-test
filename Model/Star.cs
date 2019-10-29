using System;
using Sdl2Test.Core.Interfaces;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Model
{
    public class Star : IGameEntity, IDrawable
    {
        private readonly StarClass _starClass;

        public readonly double X;
        public readonly double Y;

        public Star(
            StarClass starClass,
            double x,
            double y)
        {
            _starClass = starClass;
            X = x;
            Y = y;
        }

        public void Update(TimeSpan elapsedTime)
        {
            
        }

        public void Draw()
        {
        }
    }
}
