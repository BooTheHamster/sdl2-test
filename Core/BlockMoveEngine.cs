using Sdl2Test.Core.Interfaces;
using System;
using System.Collections.Generic;
using SDL2;

namespace Sdl2Test.Core
{
    public sealed class BlockMoveEngine
    {
        private readonly IList<GameEntityState> _entityStates = new List<GameEntityState>();

        public void Add(IGameEntity entity)
        {
            ulong lastUpdateTime = SDL.SDL_GetPerformanceCounter();
            _entityStates.Add(new GameEntityState(entity, lastUpdateTime));
        }

        public void Update()
        {
            MoveEntities();
        }

        private void MoveEntities()
        {
            var frequency = SDL.SDL_GetPerformanceFrequency() * 1.0d;

            foreach (var es in _entityStates)
            {
                var currentCount = SDL.SDL_GetPerformanceCounter();
                var elapsedTime = TimeSpan.FromMilliseconds((currentCount - es.LastUpdateCount) * 1000.0d / frequency);

                es.Entity.Update(elapsedTime);
                es.LastUpdateCount = currentCount;
            }
        }

        private class GameEntityState
        {
            public readonly IGameEntity Entity;
            public ulong LastUpdateCount;

            public GameEntityState(IGameEntity entity, ulong lastUpdateCount)
            {
                Entity = entity;
                LastUpdateCount = lastUpdateCount;
            }
        }
    }
}
