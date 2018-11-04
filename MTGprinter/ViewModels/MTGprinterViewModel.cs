using MTGprinter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MTGprinter.ViewModels
{
    public class MTGprinterViewModel
    {
        #region properties

        public ICommand CloseCommand { get; set; }

        public ListTabViewModel ListTab { get; set; }
        public CardTabViewModel CardTab { get; set; }
        public DeckTabViewModel DeckTab { get; set; }

        public Deck UserDeck { get; set; }

        #endregion

        public MTGprinterViewModel()
        {
            CloseCommand = new RelayCommand(new Action<object>(closeCommand));

            ListTab = new ListTabViewModel(this);
            CardTab = new CardTabViewModel(this);
            DeckTab = new DeckTabViewModel(this);

            ListTab.SourceList = "1 Civilized Schoolar\n" +
                                "Planeswalker (1)\n" +
                                 "1 Liliana, Death Wielder\n" +
                                 "Creature(20)\n" +
                                 "1 Festering Mummy\n" +
                                 "2 Dune Beetle\n" +
                                 "4 Tattered Mummy\n"; //+
                                               //"1 Channeler Initiate\n" +
                                               //"2 Baleful Ammit\n" +
                                               //"3 Desiccated Naga\n" +
                                               //"2 Gravedigger\n" +
                                               //"1 Crocodile of the Crossing\n" +
                                               //"2 Giant Spider\n" +
                                               //"2 Decimator Beetle\n" +
                                               //"Sorcery(2)\n" +
                                               //"2 Liliana's Influence\n" +
                                               //"Instant(2)\n" +
                                               //"2 Splendid Agony\n" +
                                               //"Artifact(3)\n" +
                                               //"1 Edifice of Authority\n" +
                                               //"1 Luxa River Shrine\n" +
                                               //"1 Oracle's Vault\n" +
                                               //"Enchantment(7)\n" +
                                               //"2 Trial of Ambition\n" +
                                               //"2 Cartouche of Ambition\n" +
                                               //"2 Cartouche of Strength\n" +
                                               //"1 Gift of Paradise\n" +
                                               //"Land(25)\n" +
                                               //"4 Foul Orchard\n" +
                                               //"1 Grasping Dunes\n" +
                                               //"11 Swamp\n" +
                                               //"9 Forest";
        }

        private void closeCommand(object obj)
        {
            Directory.Delete($"Temp/", true);
        }
    }
}
