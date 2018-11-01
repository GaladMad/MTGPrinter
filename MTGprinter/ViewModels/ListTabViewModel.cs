using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MTGprinter.Models;

namespace MTGprinter.ViewModels
{
    public class ListTabViewModel : ChangingItem
    {
        #region Properties

        private string sourceList;
        private string deckName;

        public string SourceList
        {
            get => sourceList;
            set
            {
                sourceList = value;
                NotifyPropertyChanged("SourceList");
            }
        }

        public string DeckName
        {
            get => deckName;
            set
            {
                deckName = value;
                NotifyPropertyChanged("DeckName");
            }
        }

        public string CardsList { get; set; }
        public MTGprinterViewModel Parent { get; set; }
        public ICommand PrepareDeck { get; set; }

        #endregion

        public ListTabViewModel(MTGprinterViewModel parent)
        {
            Name = "List";
            Parent = parent;
            DeckName = "NewDeck";
            PrepareDeck = new RelayCommand(new Action<object>(prepareDeck));
        }

        public void prepareDeck(object obj)
        {
            CardsList = string.Empty;
            Parent.UserDeck = new Deck(SourceList, DeckName);

            foreach (var card in Parent.UserDeck.Cards)
            {
                CardsList += (card.No + 1) + ". " + card.Name + "\n";
            }

            NotifyPropertyChanged("CardsList"); //or in set from CardsList property
        }
    }
}
