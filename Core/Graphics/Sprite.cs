using System;
using System.Drawing;
using SDL2;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Graphics
{
    /// <summary>
    /// Базовый спрайт.
    /// </summary>
    sealed public class Sprite : ISprite
    {
        private readonly IntPtr _renderer;
        private readonly IntPtr _texture;

        private SDL.SDL_Rect _textureRect;

        private bool HasTexture => _texture != IntPtr.Zero;

        public Sprite(
            int width, 
            int height,
            IntPtr renderer,
            IntPtr texture)
        {
            _textureRect.x = 0;
            _textureRect.y = 0;
            _textureRect.w = width;
            _textureRect.h = height;

            _renderer = renderer;
            _texture = texture;
        }

        public void Dispose()
        {
            if (HasTexture)
            {
                SDL.SDL_DestroyTexture(_texture);
            }
        }

        public void Draw(Rectangle targetRectangle)
        {
            SDL.SDL_Rect rect;

            rect.x = targetRectangle.X;
            rect.y = targetRectangle.Y;
            rect.w = targetRectangle.Width;
            rect.h = targetRectangle.Height;

            SDL.SDL_RenderCopy(_renderer, _texture, ref _textureRect, ref rect);
        }
    }
}
