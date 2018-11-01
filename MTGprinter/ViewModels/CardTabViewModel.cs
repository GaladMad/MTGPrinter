using MTGprinter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MTGprinter.ViewModels
{
    public class CardTabViewModel : ChangingItem
    {
        #region Fields

        private ImageSource source;
        private ImageSource nextSource;
        private ImageSource previousSource;
        private string contrastValue;
        private string brightnessValue;
        private string contrastForAll;
        private string brightnessForAll;

        #endregion

        #region Properties

        public ImageSource Source {
            get => source;
            set
            {
                source = value;
                NotifyPropertyChanged("Source");
            }
        }

        public ImageSource NextSource
        {
            get => nextSource;
            set
            {
                nextSource = value;
                NotifyPropertyChanged("NextSource");
            }
        }

        public ImageSource PreviousSource
        {
            get => previousSource;
            set
            {
                previousSource = value;
                NotifyPropertyChanged("PreviousSource");
            }
        }

        public string ContrastValue
        {
            get => contrastValue;
            set
            {
                contrastValue = value;
                NotifyPropertyChanged("ContrastValue");
            }
        }

        public string BrightnessValue
        {
            get => brightnessValue;
            set
            {
                brightnessValue = value;
                NotifyPropertyChanged("BrightnessValue");
            }
        }

        public string ContrastForAll
        {
            get => contrastForAll;
            set
            {
                contrastForAll = value;
                NotifyPropertyChanged("ContrastForAll");
            }
        }

        public string BrightnessForAll
        {
            get => brightnessForAll;
            set
            {
                brightnessForAll = value;
                NotifyPropertyChanged("BrightnessForAll");
            }
        }

        public MTGprinterViewModel Parent { get; set; }

        public ICommand DownloadCards { get; set; }
        public ICommand NextCard { get; set; }
        public ICommand PreviousCard { get; set; }
        public ICommand ChangeContrast { get; set; }
        public ICommand ChangeBrightness { get; set; }
        public ICommand SaveCurrentCard { get; set; }
        public ICommand SaveAllCards { get; set; }
        public ICommand SetContrastForAll { get; set; }
        public ICommand SetBrightnessForAll { get; set; }           
        public Card CurrentCard { get; set; }

        #endregion

        #region Constructor

        public CardTabViewModel(MTGprinterViewModel parent)
        {
            Name = "Card";
            Parent = parent;

            //Bitmap bmp = new Bitmap("Images\\Templates\\BlankCard.png");            
            //ImageSourceConverter c = new ImageSourceConverter();
            //ImageSource imgSource = (ImageSource)c.ConvertFrom(bmp);

            PreviousSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Templates\\BlankCard.png"));
            Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Templates\\BlankCard.png"));
            NextSource = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\Templates\\BlankCard.png"));

            DownloadCards = new RelayCommand(new Action<object>(downloadCards));
            NextCard = new RelayCommand(new Action<object>(moveToRight));
            PreviousCard = new RelayCommand(new Action<object>(moveToLeft));
            ChangeContrast = new RelayCommand(new Action<object>(changeContrast));
            ChangeBrightness = new RelayCommand(new Action<object>(changeBrightness));
            SaveCurrentCard = new RelayCommand(new Action<object>(saveCurrentCard));
            SaveAllCards = new RelayCommand(new Action<object>(saveAllCards));
            SetContrastForAll = new RelayCommand(new Action<object>(setContrastForAll));
            SetBrightnessForAll = new RelayCommand(new Action<object>(setBrightnessForAll));

            ContrastValue = "0";
            BrightnessValue = "0";
        }

        #endregion

        #region Commands
        private void downloadCards(object obj)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (Parent.UserDeck.Cards.Count > 0)
            {
                PreviousSource = null;// string.Empty;
                Source = null;// string.Empty;
                NextSource = null;// string.Empty;

                Searcher mysearcher = new Searcher();
                foreach (var card in Parent.UserDeck.Cards)
                {
                    card.Source = Directory.GetCurrentDirectory() + mysearcher.searchCard(card.Name, Parent.UserDeck.Name);
                    card.TempSource = Directory.GetCurrentDirectory() + "\\Temp" + mysearcher.searchCard(card.Name, Parent.UserDeck.Name);
                }

                CurrentCard = Parent.UserDeck.Cards[0];
                setImages(CurrentCard);
            }

            Mouse.OverrideCursor = null;
        }

        private void changeContrast(object obj)
        {
            if (isConditionsFulfilled())
            {
                CurrentCard.Contrast = Int32.Parse(ContrastValue);
                CurrentCard.SaveCard();
                Source = CurrentCard.GetAwers();
            }
        }

        private void changeBrightness(object obj)
        {
            if (isConditionsFulfilled())
            {
                CurrentCard.Brightness = Int32.Parse(BrightnessValue);
                CurrentCard.SaveCard();
                Source = CurrentCard.GetAwers();
            }
        }

        private void saveCurrentCard(object obj)
        {
            if (isConditionsFulfilled())
            {
                CurrentCard.SaveCard();
                setImages(CurrentCard);
            }
        }

        private void saveAllCards(object obj)
        {
            if (isConditionsFulfilled())
            {
                foreach (var card in Parent.UserDeck.Cards)
                {
                    card.Contrast = CurrentCard.Contrast;
                    card.Brightness = CurrentCard.Brightness;
                    card.SaveCard();
                }
                setImages(CurrentCard);
            }
        }

        public void moveToLeft(object obj)
        {
            if (CurrentCard != null)
            {
                Card temp = Parent.UserDeck.GetNextDifferentCard(CurrentCard);

                if (temp.Source != string.Empty)
                {
                    CurrentCard = temp;
                }

                setImages(CurrentCard);
                ContrastValue = Convert.ToString(CurrentCard.Contrast);
                BrightnessValue = Convert.ToString(CurrentCard.Brightness);
            }
        }

        public void moveToRight(object obj)
        {
            if (CurrentCard != null)
            {
                Card temp = Parent.UserDeck.GetPreviousDifferentCard(CurrentCard);

                if (temp.Source != string.Empty)
                {
                    CurrentCard = temp;
                }

                setImages(CurrentCard);
                ContrastValue = Convert.ToString(CurrentCard.Contrast);
                BrightnessValue = Convert.ToString(CurrentCard.Brightness);
            }
        }

        private void setBrightnessForAll(object obj)
        {
            if (isConditionsFulfilled())
            {
                if (Convert.ToBoolean(BrightnessForAll) == true)
                {
                    foreach (var card in Parent.UserDeck.Cards)
                    {
                        card.Brightness = Convert.ToInt32(BrightnessValue);
                        card.SaveCard();
                    }
                }
                else
                {
                    foreach (var card in Parent.UserDeck.Cards)
                    {
                        card.Brightness = 0;
                        card.SaveCard();
                    }
                    CurrentCard.Brightness = Convert.ToInt32(BrightnessValue);
                    CurrentCard.SaveCard();
                }

                setImages(CurrentCard);
            }
        }

        private void setContrastForAll(object obj)
        {
            if (isConditionsFulfilled())
            {
                if (Convert.ToBoolean(ContrastForAll) == true)
                {
                    foreach (var card in Parent.UserDeck.Cards)
                    {
                        card.Contrast = Convert.ToInt32(ContrastValue);
                        card.SaveCard();
                    }
                }
                else
                {
                    foreach (var card in Parent.UserDeck.Cards)
                    {
                        card.Contrast = 0;
                        card.SaveCard();
                    }
                    CurrentCard.Contrast = Convert.ToInt32(ContrastValue);
                    CurrentCard.SaveCard();
                }

                setImages(CurrentCard);
            }
        }

        #endregion

        #region Helpers

        private void setImages(Card card)
        {
            PreviousSource = Parent.UserDeck.GetPreviousDifferentCard(card).GetAwers();
            Source = card.GetAwers();
            NextSource = Parent.UserDeck.GetNextDifferentCard(card).GetAwers();
        }

        private bool isConditionsFulfilled()
        {
            bool result = (CurrentCard != null && CurrentCard != Card.NullCard()) ? true : false;
            return result;
        }
        #endregion
    }
}
