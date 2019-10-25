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
        private const double StartBlockVelocity = 50;

        private readonly IList<Block> _positiveBlocks = new List<Block>();
        private readonly IList<Block> _negativeBlocks = new List<Block>();
        private readonly IGraphicsService _graphicsService;
        private readonly BlockMoveEngine _blockMoveEngine;
        private readonly ISprite _positiveSprite;
        private readonly ISprite _negativeSprite;
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private readonly int _maxBlockWidth;
        private readonly int _minBlockWidth;
        private readonly int _maxBlockHeight;
        private readonly int _minBlockHeight;

        public GameEngine(
            IGraphicsService graphicsService, 
            BlockMoveEngine blockMoveEngine)
        {
            _graphicsService = graphicsService;
            _blockMoveEngine = blockMoveEngine;

            _positiveSprite = graphicsService.CreateSprite(10, 10, "positive");
            _negativeSprite = graphicsService.CreateSprite(10, 10, "negative");

            var width = graphicsService.GetWindowDimensions().Width;
            _maxBlockWidth = Math.Max(width / 2, 40);
            _minBlockWidth = Math.Max(width / 4, 20);

            var height = graphicsService.GetWindowDimensions().Height;
            _maxBlockHeight = Math.Max(height / 4, 40);
            _minBlockHeight = Math.Max(height / 10, 20);
        }

        public void Update()
        {
            CreateNewPositiveBlock();
            CreateNewNegativeBlock();

            _blockMoveEngine.Update();
            _graphicsService.Draw();
        }

        private Size GetNewBlockDimensions()
        {
            var width = _random.Next(_minBlockWidth, _maxBlockWidth);
            var height = _random.Next(_minBlockHeight, _maxBlockHeight);

            return new Size(width, height);
        }

        private void CreateNewPositiveBlock()
        {
            CreateNewBlock(_positiveBlocks, _positiveSprite, true);
        }

        private void CreateNewNegativeBlock()
        {
            CreateNewBlock(_negativeBlocks, _negativeSprite, false);
        }

        private void CreateNewBlock(
            IList<Block> blocks,
            ISprite sprite,
            bool leftAligned)
        {
            if (blocks.Count > 0)
            {
                var lastBlock = blocks.Last();

                if (lastBlock.Y < 0)
                {
                    // Самый верхний блок еще не видно целиком - нет необходимости добавлять следующий.
                    return;
                }
            }

            var size = GetNewBlockDimensions();
            var x = 0;

            if (!leftAligned)
            {
                var windowSize = _graphicsService.GetWindowDimensions();
                x = windowSize.Width - size.Width;
            }

            var block = new Block(sprite, x, -size.Height + 2, size.Width, size.Height, 0, StartBlockVelocity);

            blocks.Add(block);
            _blockMoveEngine.Add(block);
            _graphicsService.Add(block);
        }

    }
}
