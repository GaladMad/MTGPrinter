using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
//using System.Windows.Media.Imaging;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace MTGprinter.Extensions
{
    public static class BitmapEx
    {
        public static Bitmap Contrast( Bitmap sourceBitmap, int threshold)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                        sourceBitmap.Width, sourceBitmap.Height),
                                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);


            double blue = 0;
            double green = 0;
            double red = 0;


            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = ((((pixelBuffer[k] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                green = ((((pixelBuffer[k + 1] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                red = ((((pixelBuffer[k + 2] / 255.0) - 0.5) *
                            contrastLevel) + 0.5) * 255.0;


                if (blue > 255)
                { blue = 255; }
                else if (blue < 0)
                { blue = 0; }


                if (green > 255)
                { green = 255; }
                else if (green < 0)
                { green = 0; }


                if (red > 255)
                { red = 255; }
                else if (red < 0)
                { red = 0; }


                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                        resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;

            //BitmapData resultData = sourceBitmap.LockBits(new Rectangle(0, 0,
            //                            sourceBitmap.Width, sourceBitmap.Height),
            //                            ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            //Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            //sourceBitmap.UnlockBits(resultData);
        }

        public static Bitmap Brightness(Bitmap sourceBitmap, int Value)
        {

            Bitmap TempBitmap = sourceBitmap;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float FinalValue = (float)Value / 255.0f;

            float[][] FloatColorMatrix ={

                    new float[] {1, 0, 0, 0, 0},

                    new float[] {0, 1, 0, 0, 0},

                    new float[] {0, 0, 1, 0, 0},

                    new float[] {0, 0, 0, 1, 0},

                    new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);

            Attributes.Dispose();

            NewGraphics.Dispose();

            return NewBitmap;
        }

        public static System.Windows.Media.ImageSource GetImage(string source)
        {
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();

            if (source != null && source != string.Empty)
            {
                Uri uriSource = new Uri(source);
                bitmap.BeginInit();
                bitmap.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.IgnoreImageCache;
                bitmap.UriSource = uriSource;
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }

            return bitmap;
        }


        
        /// //////////////////////////////////////////////////////////////////////////////////        
        //public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        //{
        //    using (var handle = new SafeHBitmapHandle(bitmap))
        //    {
        //        //return Imaging.CreateBitmapSourceFromHBitmap(handle.DangerousGetHandle(),
        //        //    IntPtr.Zero, Int32Rect.Empty,
        //        //    BitmapSizeOptions.FromEmptyOptions());
            

        //    BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(handle.DangerousGetHandle(),
        //                                IntPtr.Zero, Int32Rect.Empty,
        //                                BitmapSizeOptions.FromEmptyOptions());

        //    PngBitmapEncoder encoder = new PngBitmapEncoder();
        //    MemoryStream memoryStream = new MemoryStream();
        //    BitmapImage bImg = new BitmapImage();

        //    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        //    encoder.Save(memoryStream);

        //    memoryStream.Position = 0;
        //    bImg.BeginInit();
        //    bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
        //    bImg.EndInit();

        //    memoryStream.Close();

        //    return bImg;
        //    }
        //    //using (var memory = new MemoryStream())
        //    //{
        //    //    bitmap.Save(memory, ImageFormat.Png);
        //    //    memory.Position = 0;

        //    //    var bitmapImage = new BitmapImage();
        //    //    bitmapImage.BeginInit();
        //    //    bitmapImage.StreamSource = memory;
        //    //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //    //    bitmapImage.EndInit();
        //    //    bitmapImage.Freeze();

        //    //    return bitmapImage;
        //    //}

        //}

        //[DllImport("gdi32.dll")]
        //private static extern int DeleteObject(IntPtr o);

        //private sealed class SafeHBitmapHandle : SafeHandleZeroOrMinusOneIsInvalid
        //{
        //    [SecurityCritical]
        //    public SafeHBitmapHandle(Bitmap bitmap)
        //        : base(true)
        //    {
        //        SetHandle(bitmap.GetHbitmap());
        //    }

        //    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        //    protected override bool ReleaseHandle()
        //    {
        //        return DeleteObject(handle) > 0;
        //    }
        //}
    }
}
