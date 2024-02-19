using Sdl2Test.Interfaces;
using Sdl2Test.Models;
using System.Collections.Generic;

namespace Sdl2Test.Game
{
    public class StarSpriteProvider
    {
        private const int SpriteWidth = 16;
        private const int SpriteHeight = 16;

        private readonly IDictionary<StarClass, string> _starSpriteIdents = new Dictionary<StarClass, string>
        {
            { StarClass.NormalClassO, "o-class-star" },
            { StarClass.NormalClassB, "b-class-star" },
            { StarClass.NormalClassA, "a-class-star" },
            { StarClass.NormalClassF, "f-class-star" },
            { StarClass.NormalClassG, "g-class-star" },
            { StarClass.NormalClassK, "k-class-star" },
            { StarClass.NormalClassM, "m-class-star" }
        };

        private readonly Dictionary<StarClass, ISprite> _starSprites = new();

        public StarSpriteProvider(IGraphicsService graphicsService)
        {
            foreach (var spriteIdent in _starSpriteIdents)
            {
                _starSprites.Add(spriteIdent.Key, graphicsService.CreateSprite(SpriteWidth, SpriteHeight, spriteIdent.Value));
            }
        }

        public ISprite GetSprite(StarClass starClass)
        {
            return _starSprites[starClass];
        }
    }
}
