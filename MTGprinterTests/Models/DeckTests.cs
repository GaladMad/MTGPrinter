using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTGprinter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGprinter.Models.Tests
{
    [TestClass()]
    public class DeckTests
    {


        [TestMethod()]
        public void GetPreviousDifferentCardTest_ImageOrderFullDeck_FirstCard()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard", "newDeck");
            for (int i=0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source"+i;
                }

                i += deck.Cards[i].Quantity;
            }
            Card previousCard;

            //Act
            previousCard = deck.GetPreviousDifferentCard(deck.Cards[0]);

            //Assert
            Assert.AreEqual(previousCard.Source, "Source5");
        }

        [TestMethod()]
        public void GetPreviousDifferentCardTest_ImageOrderFullDeck_SecondImage()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard", "newDeck");
            for (int i = 0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source" + i;
                }

                i += deck.Cards[i].Quantity;
            }
            Card previousCard;

            //Act
            previousCard = deck.GetPreviousDifferentCard(deck.Cards[0]);
            previousCard = deck.GetPreviousDifferentCard(deck.Cards[previousCard.No]);

            //Assert
            Assert.AreEqual(previousCard.Source, "Source2");
        }

        [TestMethod()]
        public void GetNextDifferentCardTest_ImageOrderFullDeck()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard", "newDeck");
            for (int i = 0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source" + i;
                }

                i += deck.Cards[i].Quantity;
            }
            Card currentCard;

            //Act
            currentCard = deck.GetNextDifferentCard(deck.Cards[0]);

            //Assert
            Assert.AreEqual(currentCard.Source, "Source2");
        }

        [TestMethod()]
        public void GetNextDifferentCardTest_ImageOrderFullDeckSecondImage()
        {
            //Arrange
            Deck deck = new Deck("2 FirstCard\n3 SecondCard\n1 TheerdCard","newDeck");
            for (int i = 0; i < deck.Cards.Count;)
            {
                for (int k = 0; k < deck.Cards[i].Quantity; k++)
                {
                    deck.Cards[i + k].Source = "Source" + i;
                }

                i += deck.Cards[i].Quantity;
            }
            Card currentCard;

            //Act
            currentCard = deck.GetNextDifferentCard(deck.Cards[0]);
            currentCard = deck.GetNextDifferentCard(deck.Cards[currentCard.No]);

            //Assert
            Assert.AreEqual(currentCard.Source, "Source5");
        }
    }
}