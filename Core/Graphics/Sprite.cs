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
        private readonly IntPtr renderer;
        private readonly IntPtr texture;
        private bool HasTexture => texture != IntPtr.Zero;
        private SDL.SDL_Rect textureRect;

        public Sprite(
            int width, 
            int height,
            IntPtr renderer,
            IntPtr texture)
        {
            textureRect.x = 0;
            textureRect.y = 0;
            textureRect.w = width;
            textureRect.h = height;

            this.renderer = renderer;
            this.texture = texture;
        }

        public void Dispose()
        {
            if (HasTexture)
            {
                SDL.SDL_DestroyTexture(texture);
            }
        }

        public void Draw(Rectangle targetRectangle)
        {
            SDL.SDL_Rect rect;

            rect.x = targetRectangle.X;
            rect.y = targetRectangle.Y;
            rect.w = targetRectangle.Width;
            rect.h = targetRectangle.Height;

            SDL.SDL_RenderCopy(renderer, texture, ref textureRect, ref rect);
        }
    }
}
