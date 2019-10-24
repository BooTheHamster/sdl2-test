using Sdl2Test.Core;
using Sdl2Test.Interfaces;
using Sdl2Test.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Sdl2Test.Game
{
    public sealed class GameEngine
    {
        private const int MinBlockSize = 3;
        private const int MaxBlockSize = 10;
        private const double StartBlockVelocity = 0.2;

        private readonly List<Block> positiveBlocks = new List<Block>();
        private readonly List<Block> negativeBlocks = new List<Block>();
        private readonly IGraphicsService graphicsService;
        private readonly BlockMoveEngine blockMoveEngine;
        private readonly ISprite positiveSprite;
        private readonly ISprite negativeSprite;
        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        public GameEngine(
            IGraphicsService graphicsService, 
            BlockMoveEngine blockMoveEngine)
        {
            this.graphicsService = graphicsService;
            this.blockMoveEngine = blockMoveEngine;

            positiveSprite = graphicsService.CreateSprite(10, 10, "positive");
            negativeSprite = graphicsService.CreateSprite(10, 10, "negative");
        }

        public void Update()
        {
            CreateNewPositiveBlock();

            blockMoveEngine.Update();
            graphicsService.Draw();
        }

        private Size GetNewBlockDimensions()
        {
            var width = random.Next(MinBlockSize, MaxBlockSize);
            var height = random.Next(MinBlockSize, MaxBlockSize);

            return new Size(width, height);
        }

        private void CreateNewPositiveBlock()
        {
            if (positiveBlocks.Count > 0)
            {
                var lastBlock = positiveBlocks.Last();

                if ((lastBlock.Y - lastBlock.Height) < 0)
                {
                    // Самый верхний блок еще не видно целиком - нет необходимости добавлять следующий.
                    return;
                }
            }

            var size = GetNewBlockDimensions();
            var block = new Block(positiveSprite, 0, -size.Height, size.Width, size.Height, 0, StartBlockVelocity);

            positiveBlocks.Add(block);
            blockMoveEngine.Add(block);
            graphicsService.Add(block);
        }
    }
}
