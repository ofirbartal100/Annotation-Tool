using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Prism.Regions;

namespace AnnotationTool.Utils
{
    public static class RegionManagerExtentions
    {
        private static NotifyCollectionChangedEventHandler _callback;
        private static int _stackcount;

        /// <summary>
        /// Navigates the given regions into the desired views
        /// </summary>
        /// <param name="regionManager">the region manager</param>
        /// <param name="regionViewDictionary">dictionary containing region names as keys and view names as values</param>
        public static void Navigate(this IRegionManager regionManager,
            Dictionary<string, string> regionViewDictionary)
        {
            InitializeNavigations(regionManager, regionViewDictionary);
            foreach (KeyValuePair<string, string> keyValuePair in regionViewDictionary)
            {
                regionManager.RequestNavigate(keyValuePair.Key, new Uri(keyValuePair.Value, UriKind.Relative));
            }
        }

        /// <summary>
        /// Navigates the given regions into the desired views, waiting for region to be initialized
        /// </summary>
        /// <param name="regionManager">the region manager</param>
        /// <param name="regionViewDictionary">dictionary containing region names as keys and view names as values</param>
        public static void InitializeNavigations(this IRegionManager regionManager,
            Dictionary<string, string> regionViewDictionary)
        {
            _callback = GenerateCallbackWithScopeToThis(regionManager, regionViewDictionary);
            _stackcount = regionViewDictionary.Count;
            regionManager.Regions.CollectionChanged += _callback;
        }
        //create the callback and remove from the subscription
        private static NotifyCollectionChangedEventHandler GenerateCallbackWithScopeToThis(IRegionManager regionManager, Dictionary<string, string> regionViewDictionary)
        {
            return (sender, args) =>
            {
                //request navigation for all pairs
                foreach (KeyValuePair<string, string> keyValuePair in regionViewDictionary)
                {
                    regionManager.RequestNavigate(keyValuePair.Key, new Uri(keyValuePair.Value, UriKind.Relative));
                }

                //remove callback from RegionManager, when finished all the regions
                if (--_stackcount == 0)
                {
                    regionManager.Regions.CollectionChanged -= _callback;
                }

            };
        }
    }

    public static class EmguExtentions
    {
        /// <summary>
        /// Converts AnnotationTools <see cref="System.Drawing.Image"/> into AnnotationTools WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Image source)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(source);

            var bitSrc = bitmap.ToBitmapSource();

            bitmap.Dispose();
            bitmap = null;

            return bitSrc;
        }

        /// <summary>
        /// Converts AnnotationTools <see cref="System.Drawing.Bitmap"/> into AnnotationTools WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <remarks>Uses GDI to do the conversion. Hence the call to the marshalled DeleteObject.
        /// </remarks>
        /// <param name="source">The source bitmap.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Bitmap source)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    source.Save(ms,ImageFormat.Bmp);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            //BitmapSource bitSrc = null;

            //var hBitmap = source.GetHbitmap();

            //try
            //{
            //    bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            //        hBitmap,
            //        IntPtr.Zero,
            //        Int32Rect.Empty,
            //        BitmapSizeOptions.FromEmptyOptions());
            //}
            //catch (Win32Exception)
            //{
            //    bitSrc = null;
            //}
            //finally
            //{
            //    NativeMethods.DeleteObject(hBitmap);
            //}

            //return bitSrc;
        }

        ///// <summary>
        ///// FxCop requires all Marshalled functions to be in AnnotationTools class called NativeMethods.
        ///// </summary>
        //internal static class NativeMethods
        //{
        //    [DllImport("gdi32.dll")]
        //    [return: MarshalAs(UnmanagedType.Bool)]
        //    internal static extern bool DeleteObject(IntPtr hObject);
        //}
    }
}
