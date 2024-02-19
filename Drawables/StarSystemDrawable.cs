using System.Drawing;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;

namespace Sdl2Test.Drawables
{
    /// <summary>
    /// Отрисовка звездной системы.
    /// </summary>
    internal class StarSystemDrawable(StarSystem starSystem, ISprite sprite) : IDrawable
    {
        private const int SpriteWidth = 16;
        private const int SpriteHeight = 16;

        private Rectangle DrawRectangle => new(starSystem.Coordinates.X, starSystem.Coordinates.Y, SpriteWidth, SpriteHeight);

        public void Draw(IDrawService drawService)
        {
            drawService.Draw(DrawRectangle, sprite);
        }
    }
}
