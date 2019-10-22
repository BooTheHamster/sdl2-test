using System;
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
        /// Скорость изменения координаты X в секунду.
        /// </summary>
        public double VelocityX;

        /// <summary>
        /// Скорость изменения координаты Y в секунду.
        /// </summary>
        public double VelocityY;

        public Block(
            ISprite sprite,
            double x = 0d,
            double y = 0d,
            double velocityX = 0d,
            double velocityY = 0d)
            : base(sprite)
        {
            X = x;
            Y = y;
            spriteX = (int)Math.Round(X);
            spriteY = (int)Math.Round(Y);
            VelocityX = velocityX;
            VelocityY = velocityY;
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

            sprite.DrawAt(spriteX, spriteY);
        }
    }
}
