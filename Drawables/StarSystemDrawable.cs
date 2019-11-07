using System.Drawing;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;

namespace Sdl2Test.Drawables
{
    /// <summary>
    /// Отрисовка звездной системы.
    /// </summary>
    class StarSystemDrawable : IDrawable
    {
        private const int SpriteWidth = 16;
        private const int SpriteHeight = 16;
        private readonly StarSystem _starSystem;
        private readonly ISprite _sprite;

        public Rectangle DrawRectangle => new Rectangle(_starSystem.Coordinates.X, _starSystem.Coordinates.Y, SpriteWidth, SpriteHeight);

        public StarSystemDrawable(StarSystem starSystem, ISprite sprite)
        {
            _starSystem = starSystem;
            _sprite = sprite;
        }

        public void Draw(IDrawService drawService)
        {
            drawService.Draw(DrawRectangle, _sprite);
        }
    }
}
