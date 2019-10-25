using Sdl2Test.Core;
using Sdl2Test.Interfaces;
using Sdl2Test.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private const int MinBlockSize = 10;
        private const int MaxBlockSize = 100;
        private const double StartBlockVelocity = 50;

        private readonly List<Block> _positiveBlocks = new List<Block>();
        private readonly List<Block> _negativeBlocks = new List<Block>();
        private readonly IGraphicsService _graphicsService;
        private readonly BlockMoveEngine _blockMoveEngine;
        private readonly ISprite _positiveSprite;
        private readonly ISprite _negativeSprite;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        public GameEngine(
            IGraphicsService graphicsService, 
            BlockMoveEngine blockMoveEngine)
        {
            this._graphicsService = graphicsService;
            this._blockMoveEngine = blockMoveEngine;

            _positiveSprite = graphicsService.CreateSprite(10, 10, "positive");
            _negativeSprite = graphicsService.CreateSprite(10, 10, "negative");
        }

        public void Update()
        {
            CreateNewPositiveBlock();

            _blockMoveEngine.Update();
            _graphicsService.Draw();
        }

        private Size GetNewBlockDimensions()
        {
            var width = _random.Next(MinBlockSize, MaxBlockSize);
            var height = _random.Next(MinBlockSize, MaxBlockSize);

            return new Size(width, height);
        }

        private void CreateNewPositiveBlock()
        {
            if (_positiveBlocks.Count > 0)
            {
                var lastBlock = _positiveBlocks.Last();

                if (lastBlock.Y < 0)
                {
                    // Самый верхний блок еще не видно целиком - нет необходимости добавлять следующий.
                    return;
                }
            }

            var size = GetNewBlockDimensions();
            var block = new Block(_positiveSprite, 0, -size.Height + 1, size.Width, size.Height, 0, StartBlockVelocity);

            _positiveBlocks.Add(block);
            _blockMoveEngine.Add(block);
            _graphicsService.Add(block);
        }
    }
}
