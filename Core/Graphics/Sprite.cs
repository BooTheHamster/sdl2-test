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

        public void DrawAt(int x, int y)
        {
            SDL.SDL_Rect rect;

            rect.x = x;
            rect.y = y;
            rect.w = textureRect.w;
            rect.h = textureRect.h;

            SDL.SDL_RenderCopy(renderer, texture, ref textureRect, ref rect);
        }
    }
}
