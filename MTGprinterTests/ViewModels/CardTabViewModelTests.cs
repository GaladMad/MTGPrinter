using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTGprinter.Models;
using MTGprinter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGprinter.ViewModels.Tests
{
    [TestClass()]
    public class CardTabViewModelTests
    {
        [TestMethod()]
        public void switchNextCardTest_FirstCard()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard", "newDeck"); //0,2,5
            for (int i = 0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source" + i;
                }

                i += deck.Cards[i].Quantity;
            }

            MTGprinterViewModel parent = new MTGprinterViewModel();
            parent.UserDeck = deck;
            parent.CardTab.CurrentCard = parent.UserDeck.Cards[0];

            //Act
            parent.CardTab.moveToRight(new object());

            //string previous = parent.CardTab.PreviousSource;
            //string current = parent.CardTab.Source;
            //string next = parent.CardTab.NextSource;


            //Assert

            //Assert.IsTrue((previous=="Source2" && current=="Source5" && next=="Source0"));
        }

        [TestMethod()]
        public void switchPreviousCardTest_FirstCard()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard", "newDeck"); //0,2,5
            for (int i = 0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source" + i;
                }

                i += deck.Cards[i].Quantity;
            }

            MTGprinterViewModel parent = new MTGprinterViewModel();
            parent.UserDeck = deck;
            parent.CardTab.CurrentCard = parent.UserDeck.Cards[0];

            //Act
            parent.CardTab.moveToLeft(new object());

            //string previous = parent.CardTab.PreviousSource;
            //string current = parent.CardTab.Source;
            //string next = parent.CardTab.NextSource;


            //Assert

            /*Assert.IsTrue((previous == "Source0" && current == "Source2" && next == "Source5"))*/;
        }
    }
}