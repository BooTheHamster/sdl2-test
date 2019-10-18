using System;
using SDL2;
using Sdl2Test.Interfaces;

namespace Sdl2Test.Graphics
{
    /// <summary>
    /// Базовый спрайт.
    /// </summary>
    sealed public class Sprite : ISprite
    {
        private IntPtr renderer;
        private IntPtr texture;

        private bool HasTexture => texture != IntPtr.Zero;

        private SDL.SDL_Rect rect;
        private SDL.SDL_Rect textureRect;

        public Sprite(
            int x, 
            int y, 
            int width, 
            int height,
            IntPtr renderer,
            IntPtr texture)
        {
            textureRect.x = 0;
            textureRect.y = 0;
            textureRect.w = width;
            textureRect.h = height;

            rect.x = x;
            rect.y = y;
            rect.w = width;
            rect.h = height;
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

        public void Draw()
        {
            SDL.SDL_RenderCopy(renderer, texture, ref textureRect, ref rect);
        }
    }
}
