using System;
using System.Drawing;
using Sdl2Test.Core.Model;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Model
{
    public class Block : GameEntity
    {
        private int spriteX;
        private int spriteY;

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
            spriteX = (int)Math.Round(X);
            spriteY = (int)Math.Round(Y);
            VelocityX = velocityX;
            VelocityY = velocityY;
            Width = width;
            Height = height;
        }

        public override void Update(TimeSpan elapsedTime)
        {
            X += VelocityX * elapsedTime.TotalSeconds;
            Y += VelocityY * elapsedTime.TotalSeconds;

            if (sprite == null)
            {
                return;
            }

            var newSpriteX = (int)Math.Round(X);
            var newSpriteY = (int)Math.Round(Y);

            if (spriteX != newSpriteX)
            {
                spriteX = newSpriteX;
            }

            if (spriteY != newSpriteY)
            {
                spriteY = newSpriteY;
            }
        }

        public override void Draw()
        {
            if (sprite == null)
            {
                return;
            }

            var rect = new Rectangle(spriteX, spriteY, Width, Height);
            sprite.Draw(rect);
        }
    }
}
