﻿using System;
using AnnotationTool.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using AnnotationTool.Views.Canvas;
using AnnotationTool.Views.Image;
using AnnotationTool.Views.Video;

namespace AnnotationTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        { 
            
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(Object), typeof(ImageAnnotationCanvas), "ImageAnnotationCanvas");
            containerRegistry.Register(typeof(Object), typeof(VideoAnnotationCanvas), "VideoAnnotationCanvas");
            containerRegistry.Register(typeof(Object), typeof(SourceSelection), "SourceSelection");
            containerRegistry.Register(typeof(Object), typeof(VideoAnnotationStudio), "VideoAnnotationStudio");
            containerRegistry.Register(typeof(Object), typeof(ImagesAnnotationStudio), "ImageAnnotationStudio");
            containerRegistry.Register(typeof(Object), typeof(VideoAnnotationControls), "VideoAnnotationControls");
            containerRegistry.Register(typeof(Object), typeof(AnnotationCanvas), "AnnotationCanvas");
            containerRegistry.Register(typeof(Object), typeof(VideoControls), "VideoControls");
        }
    }
}