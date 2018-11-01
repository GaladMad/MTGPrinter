using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGprinter.Models
{
    public class DocPreparator
    {
        public List<string> preparePrint(Deck deck, string backgroundSource)
        {
            //Bitmap background = new Bitmap(bmp, new Size(4960, 7016));
            //Bitmap picture = new Bitmap($"Images/New/{source}.png");
            List<string> resultList = new List<string>();
            int pageCounter = deck.Cards.Last().Page;
            int cardCounter = 0;

            for (int i = 0; i <= pageCounter; i++)
            {
                using (Document doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($"Images/{deck.Name}/{deck.Name}{i}.pdf", FileMode.Create));
                    using (Bitmap bmp = new Bitmap(backgroundSource))
                    {
                        for (int k = 0; k < 9 && cardCounter < deck.Cards.Count; k++)                        {
                            using (Bitmap card = new Bitmap($"Images/{deck.Name}/{deck.Cards[cardCounter].Name}.png"))//, new Size(120, 170));
                            {                                   //Pen blackPen = new Pen(Color.Black, 3);
                                                                //test
                                Color whichColor = card.GetPixel(0, 0);
                                // Draw line to screen.
                                using (var graphics = Graphics.FromImage(bmp))
                                {
                                    //graphics.DrawLine(blackPen, x1, y1, x2, y2);
                                    //card = Extension.Contrast(card, 12);------------------------------------------------------------------------------------------------------------------------------------
                                    graphics.DrawImage(card, deck.Cards[cardCounter].Position.X, deck.Cards[cardCounter].Position.Y, 1434, 2031);
                                    //graphics.DrawImage(card, 188, 305, 1434, 2031);
                                    //graphics.DrawImage(card, 1763, 305, 1434, 2031);

                                }
                            }
                            bmp.Save($"Images/{deck.Name}/{deck.Name}{i}.png");
                            doc.Open();

                            try
                            {
                                string imageURL = $"Images/{deck.Name}/{deck.Name}{i}.png";
                                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);

                                //Resize image depend upon your need

                                jpg.ScaleToFit(iTextSharp.text.PageSize.A4.Width, iTextSharp.text.PageSize.A4.Height);

                                //Give space before image
                                //jpg.SpacingBefore = 0f;
                                //Give some space after the image
                                //jpg.SpacingAfter = 0f;

                                jpg.Alignment = Element.ALIGN_LEFT;
                                doc.Add(jpg);
                                //doc.NewPage();
                                //doc.Add(jpg);



                            }

                            catch (Exception ex)

                            { throw ex; }

                            cardCounter++;
                        }
                    }
                    //if(i<pageCounter){doc.NewPage();}
                    doc.Close();
                }
                resultList.Add($"Images/{deck.Name}/{deck.Name}{i}.png");
                //doc.Close();
            }

            return resultList;
        }
    }
}
