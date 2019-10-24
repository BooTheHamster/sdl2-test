using Sdl2Test.Core.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using SDL2;

namespace Sdl2Test.Core
{
    public sealed class BlockMoveEngine
    {
        private readonly IList<GameEntityState> entityStates = new List<GameEntityState>();

        public void Add(IGameEntity entity)
        {
            ulong lastUpdateTime = SDL.SDL_GetPerformanceCounter();
            entityStates.Add(new GameEntityState(entity, lastUpdateTime));
        }

        public void Update()
        {
            MoveEntities();
        }

        private void MoveEntities()
        {
            var frequency = SDL.SDL_GetPerformanceFrequency() * 1.0d;

            foreach (var es in entityStates)
            {
                var entityState = es;
                var currentCount = SDL.SDL_GetPerformanceCounter();
                var elapsedTime = TimeSpan.FromMilliseconds((currentCount - entityState.LastUpdateCount) * 1000.0d / frequency);

                entityState.Entity.Update(elapsedTime);
                entityState.LastUpdateCount = currentCount;
            }
        }

        private struct GameEntityState
        {
            public readonly IGameEntity Entity;
            public ulong LastUpdateCount;

            public GameEntityState(IGameEntity entity, ulong lastUpdateCount)
                : this()
            {
                Entity = entity;
                LastUpdateCount = lastUpdateCount;
            }
        }
    }
}
