﻿using Sdl2Test.Interfaces;
using Serilog;
using System;
using SDL2;
using Sdl2Test.Graphics;
using System.Collections.Generic;
using System.Drawing;

namespace Sdl2Test.Services
{
    /// <summary>
    /// Графическая подсистема.
    /// </summary>
    sealed public class GraphicsService : IGraphicsService
    {
        private readonly ILogger logger;
        private ISurfaceProvider surfaceProvider;
        private IntPtr window = IntPtr.Zero;
        private IntPtr renderer = IntPtr.Zero;
        private readonly IList<IDrawable> drawables = new List<IDrawable>();
        private readonly IList<IDisposable> disposables = new List<IDisposable>();
        private Size windowSize = new Size();

        private bool HasWindow => window != IntPtr.Zero;
        private bool HasRender => renderer != IntPtr.Zero;

        private bool CanDraw => HasWindow && HasRender;

        public GraphicsService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }

            if (HasRender)
            {
                SDL.SDL_DestroyRenderer(renderer);
            }

            if (HasWindow)
            {
                SDL.SDL_DestroyWindow(window);
            }

            surfaceProvider.Dispose();

            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }

        public bool TryInitialize()
        {
            var result = InitializeSdl() 
                && CreateWindow()
                && CreateRenderer();

            surfaceProvider = new SurfaceProvider(logger);

            return result;
        }

        public void Draw()
        {
            if (!CanDraw)
            {
                return;
            }

            SDL.SDL_SetRenderDrawColor(renderer, 0xcb, 0xc6, 0xaf, 0xff);
            SDL.SDL_RenderClear(renderer);

            foreach (var drawable in drawables)
            {
                drawable.Draw();
            }

            SDL.SDL_RenderPresent(renderer);
        }

        public ISprite CreateSprite(int width, int height, string imageIdent)
        {
            if (!HasRender)
            {
                return null;
            }

            if (!surfaceProvider.TryGetSurface(imageIdent, out IntPtr surface))
            {
                return null;
            }

            var texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);

            if (texture == IntPtr.Zero)
            {
                LogSdlError($"Ошибка при создании текстуры {imageIdent}");
                return null;
            }

            var sprite = new Sprite(width, height, renderer, texture);
            disposables.Add(sprite);

            return sprite;
        }

        public void Add(IDrawable drawable)
        {
            drawables.Add(drawable);
        }

        public Size GetWindowDimensions()
        {
            return new Size(windowSize.Width, windowSize.Height);
        }

        private bool InitializeSdl()
        {
            int result;
            try
            {
                result = SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

            }
            catch (DllNotFoundException)
            {
                LogSdlError("Не найдена SDL2.dll в каталоге приложения.");
                return false;
            }

            if (result != 0)
            {
                LogSdlError("Ошибка инциализации SDL");
                return false;
            }

            result = SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

            if (((int)SDL_image.IMG_InitFlags.IMG_INIT_PNG & result) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
            {
                LogSdlError("Ошибка инциализации SDL Image");
                return false;
            }

            return true;
        }

        private bool CreateWindow()
        {
            var windowTitle = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";

            if (SDL.SDL_GetDesktopDisplayMode(0, out SDL.SDL_DisplayMode displayMode) != 0)
            {
                LogSdlError("Ошибка получения режима работы экрана.");
                return false;
            }


            windowSize.Width = (int) Math.Floor(displayMode.w * 0.3);
            windowSize.Height = (int) Math.Floor(displayMode.h * 0.7);
            var x = (displayMode.w - windowSize.Width) / 2;
            var y = (displayMode.h - windowSize.Height) / 2;

            window = SDL.SDL_CreateWindow(
                windowTitle,
                x,
                y,
                windowSize.Width,
                windowSize.Height,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS);

            if (HasWindow)
            {
                return true;
            }

            LogSdlError("Ошибка при создании окна SDL.");
            return false;
        }

        private bool CreateRenderer()
        {
            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (HasRender)
            {
                return true;
            }

            LogSdlError("Ошибка при создании рендерера SDL.");
            return false;
        }

        private void LogSdlError(string message)
        {
            logger.Error(message);
            logger.Error("Ошибка SDL: {0}", SDL.SDL_GetError());
        }
    }
}
