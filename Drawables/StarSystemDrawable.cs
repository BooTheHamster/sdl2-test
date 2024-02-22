using System.Drawing;
using Sdl2Test.Interfaces;
using Sdl2Test.Models;

namespace Sdl2Test.Drawables
{
    /// <summary>
    /// Отрисовка звездной системы.
    /// </summary>
    internal class StarSystemDrawable : IDrawable
    {
        private readonly StarSystem _starSystem;
        private readonly ISprite _sprite;
        private readonly IDrawCoordinatesConverter _drawCoordinatesConverter;
        private const int SpriteWidth = 16;
        private const int SpriteHeight = 16;

        private Rectangle _drawRectangle;

        public StarSystemDrawable(StarSystem starSystem, ISprite sprite, IDrawCoordinatesConverter drawCoordinatesConverter)
        {
            _starSystem = starSystem;
            _sprite = sprite;
            _drawCoordinatesConverter = drawCoordinatesConverter;
            
            Update();
        }

        public void Draw(IDrawService drawService)
        {
            drawService.Draw(_drawRectangle, _sprite);
        }

        private void Update()
        {
            _drawCoordinatesConverter.Convert(_starSystem.X, _starSystem.Y, out var drawX, out var drawY);
            _drawRectangle = new Rectangle(drawX, drawY, SpriteWidth, SpriteHeight);
        }
    }
}
