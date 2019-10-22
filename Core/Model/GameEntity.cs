using System;
using Sdl2Test.Core.Interfaces;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Core.Model
{
    public abstract class GameEntity : IGameEntity, IDrawable
    {
        protected ISprite sprite;

        protected GameEntity(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public abstract void Draw();

        public abstract void Update(TimeSpan elapsedTime);
    }
}
