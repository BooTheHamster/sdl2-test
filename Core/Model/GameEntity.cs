using System;
using Sdl2Test.Core.Interfaces;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Core.Model
{
    public abstract class GameEntity : IGameEntity
    {
        protected ISprite sprite;

        public abstract void Update(TimeSpan epalsedTime);
    }
}
