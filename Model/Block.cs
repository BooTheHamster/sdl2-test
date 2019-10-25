using System;
using System.Drawing;
using Sdl2Test.Core.Model;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Model
{
    public class Block : GameEntity
    {
        private int _spriteX;
        private int _spriteY;

        /// <summary>
        /// Координата X.
        /// </summary>
        public double X;

        /// <summary>
        /// Координата Y.
        /// </summary>
        public double Y;

        /// <summary>
        /// Ширина.
        /// </summary>
        public int Height;

        /// <summary>
        /// Высота.
        /// </summary>
        public int Width;

        /// <summary>
        /// Скорость изменения координаты X в секунду.
        /// </summary>
        public double VelocityX;

        /// <summary>
        /// Скорость изменения координаты Y в секунду.
        /// </summary>
        public double VelocityY;

        public Block(
            ISprite sprite,
            double x,
            double y,
            int width,
            int height,
            double velocityX,
            double velocityY)
            : base(sprite)
        {
            X = x;
            Y = y;
            _spriteX = (int)Math.Round(X);
            _spriteY = (int)Math.Round(Y);
            VelocityX = velocityX;
            VelocityY = velocityY;
            Width = width;
            Height = height;
        }

        public override void Update(TimeSpan elapsedTime)
        {
            X += VelocityX * elapsedTime.TotalSeconds;
            Y += VelocityY * elapsedTime.TotalSeconds;

            if (_sprite == null)
            {
                return;
            }

            var newSpriteX = (int)Math.Round(X);
            var newSpriteY = (int)Math.Round(Y);

            if (_spriteX != newSpriteX)
            {
                _spriteX = newSpriteX;
            }

            if (_spriteY != newSpriteY)
            {
                _spriteY = newSpriteY;
            }
        }

        public override void Draw()
        {
            if (_sprite == null)
            {
                return;
            }

            var rect = new Rectangle(_spriteX, _spriteY, Width, Height);
            _sprite.Draw(rect);
        }
    }
}
