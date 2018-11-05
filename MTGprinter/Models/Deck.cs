using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGprinter.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        public string Name { get; set; }
        public bool WithRewers { get; set; }
        public bool WithLands { get; set; }
        private enum SerchDirection
        {
            Previous = 0,
            Next
        }

        public Deck(string list, string deckName)
        {
            Name = deckName;
            Cards = new List<Card>();
            int counter = 0;
            string name = "";

            string[] lines = list.Split(Environment.NewLine.ToCharArray());

            //System.Drawing.Point pos = new System.Drawing.Point();
            int number = 0;

            foreach (var line in lines)
            {
                string[] words = line.Split(' ');

                if (Int32.TryParse(words[0], out counter))
                {
                    for (int i = 1; i < words.Length; i++)
                    {
                        if (i == words.Length - 1)
                        {
                            name = name + words[i];
                        }
                        else
                        {
                            name = name + words[i] + " ";
                        }
                    }

                    for (int i = 0; i < counter; i++)
                    {
                        Card nextCard = new Card() { Name = name };
                        nextCard.Page = number / 9;
                        nextCard.Quantity = counter;
                        nextCard.No = number;

                        int posnum = number % 9;
                        //nextCard.Position = new System.Drawing.Point(187 + (posnum % 3) * 1576, 304 + (posnum / 3) * 2189);
                        nextCard.Position = new System.Drawing.Point(51 + (posnum % 3) * 526, 78 + (posnum / 3) * 741);

                        Cards.Add(nextCard);

                        number++;
                    }

                    name = "";
                }
            }
        }

        private Card GetDifferentCard(Card card, SerchDirection direction)
        {
            Card result = Card.NullCard();

            if( Cards.Count > 1 )
            {
                result = Cards[card.No];
                bool isFound = false;
                int counter = 0;

                while (!isFound)
                {
                    if(counter < Cards.Count)
                    {
                        switch (direction)
                        {
                            case SerchDirection.Previous:
                                result = (result.No == 0) ? Cards[Cards.Count - 1] : Cards[result.No - 1];
                                break;
                            case SerchDirection.Next:
                            default:
                                result = (result.No == Cards.Count - 1) ? Cards[0] : Cards[result.No + 1];
                                break;
                        }                        

                        if (card.Source != result.Source)
                        {
                            isFound = true;
                        }
                    }
                    else
                    {
                        result = Card.NullCard();
                        isFound = true;
                    }

                    counter++;
                }
            }

            return result;
        }

        public Card GetPreviousDifferentCard(Card card)
        {
            return GetDifferentCard(card, SerchDirection.Previous);
        }

        public Card GetNextDifferentCard(Card card)
        {
            return GetDifferentCard(card, SerchDirection.Next);
        }
    }
}
