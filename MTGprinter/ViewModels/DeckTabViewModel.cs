using MTGprinter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MTGprinter.ViewModels
{
    public class DeckTabViewModel : ChangingItem
    {
        #region Properties

        private ImageSource source;
        public MTGprinterViewModel Parent { get; set; }
        public ICommand PreparePdfs { get; set; }
        private string backgroundSource;

        public ImageSource Source
        {
            get => source;
            set
            {
                source = value;
                NotifyPropertyChanged("Source");
            }
        }

        #endregion

        public DeckTabViewModel(MTGprinterViewModel parent)
        {
            Name = "Deck";
            Parent = parent;
            backgroundSource = "Templates\\Background.png";
            Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + backgroundSource));
            //"C:\\C#\\MTGprinter\\MTGprinter\\bin\\Debug\\Images\\Templates\\Background.png";

            PreparePdfs = new RelayCommand(new Action<object>(preparePdfs));
        }

        private void preparePdfs(object obj)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            DocPreparator preparator = new DocPreparator();
            Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + preparator.preparePrint(Parent.UserDeck, backgroundSource).First()));

            Mouse.OverrideCursor = null;

        }
    }
}
