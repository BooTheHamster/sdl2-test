﻿using System;
using Sdl2Test.Core.Interfaces;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Core.Model
{
    public abstract class GameEntity : IGameEntity, IDrawable
    {
        protected ISprite _sprite;

        protected GameEntity(ISprite sprite)
        {
            _sprite = sprite;
        }

        public abstract void Draw(IDrawService drawService);

        public abstract void Update(TimeSpan elapsedTime);
    }
}
