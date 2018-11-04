using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using MTGprinter.Extensions;

namespace MTGprinter.Models
{
    public class Card
    {
        #region Properties

        //public Bitmap Awers { get; set; }
        //public Bitmap Rewers { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int No { get; set; }
        public string Source { get; set; }
        public string TempSource { get; set; }
        public string BackSource { get; set; }
        public string BackTempSource { get; set; }
        public int Page { get; set; }
        public Point Position { get; set; }
        public int Contrast { get; set; }
        public int Brightness { get; set; }
        public int BackContrast { get; set; }
        public int BackBrightness { get; set; }

        #endregion

        public static Card NullCard()
        {
            return new Card() { Name = string.Empty, Source = string.Empty };
        }

        public void SaveAvers()
        {
            SaveCardToSource(Source, TempSource, Contrast, Brightness);
        }

        public void SaveReverse()
        {
            SaveCardToSource(BackSource, BackTempSource, BackContrast, BackBrightness);
        }

        public ImageSource GetAvers()
        {
            return BitmapEx.GetImage(Source);
        }

        public ImageSource GetReverse()
        {
            return BitmapEx.GetImage(BackSource);
        }

        //public ImageSource GetTempAwers()
        //{
        //    return BitmapEx.GetImage(TempSource);

        //    //Bitmap card = new Bitmap(Source);
        //    //card = BitmapEx.Contrast(card, Contrast);
        //    //card = BitmapEx.Brightness(card, Brightness);
        //    ////probably is source of memory leak!
        //    //BitmapImage result = BitmapEx.ToBitmapImage(card);
        //    //card.Dispose();

        //    //return result;
        //}

        private void SaveCardToSource(string source, string tempSource, int contrast, int brightness)
        {
            Bitmap card = new Bitmap(tempSource);
            card = BitmapEx.Contrast(card, contrast);
            card = BitmapEx.Brightness(card, brightness);
            Bitmap newCard = new Bitmap(card);
            card.Dispose();
            newCard.Save(source);
            newCard.Dispose();
        }
    }
}