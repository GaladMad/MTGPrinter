using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MTGprinter.Models
{
    public class Searcher
    {
        public string searchCard(string cardName, string deckName)
        {
            //http://mtg.wtf/card?q=brazen+scourge

            string htmlName = cardName.Replace(' ', '+').ToLower(); ;

            string input = new WebClient().DownloadString($"http://mtg.wtf/card?q={htmlName}");


            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(input);

            HtmlNode htmlDiv = htmlDoc.DocumentNode.SelectSingleNode("//body/div/div/div/div");

            HtmlDocument htmlDoc2 = new HtmlDocument();
            htmlDoc2.LoadHtml(htmlDiv.InnerHtml);

            HtmlNodeCollection htmlTags = htmlDoc2.DocumentNode.SelectNodes("//img");
            //var htmlTag = htmlDoc.DocumentNode.SelectSingleNode("//body/div/div/div/div/img");
            //http://mtg.wtf/cards_hq/kld/107.png

            for (int i = 0; i < htmlTags.Count; i++)
            {
                string src = $"http://mtg.wtf{htmlTags[i].Attributes["src"].Value}";
                //string name = cardName;//htmlTags[i].Attributes["alt"].Value;
                //-----------------------------------------------------------------------------------------
                //name = name.Replace("&#39;", "'");
                //-----------------------------------------------------------------------------------------
                Searcher file = new Searcher();

                Directory.CreateDirectory($"Images/{deckName}/");
                Directory.CreateDirectory($"Temp/Images/{deckName}/");
                string[] existedFiles = Directory.GetFiles($"Images/{deckName}/");
                var hashExistedFiles = new HashSet<string>(existedFiles);
                if (!hashExistedFiles.Contains($"Images/{deckName}/{cardName}.png"))
                {
                    file.downloadFile(src, cardName, htmlName, deckName);
                }

                DirectoryCopy($"Images/{deckName}/", $"Temp/Images/{deckName}/", true);
                //Console.WriteLine("-----------" + src);
            }

            return $"\\Images\\{deckName}\\{cardName}.png";
        }

        //src="http://mtg.wtf/cards_hq/ne/101.png"
        public void downloadFile(string src, string name, string cnme, string deckName)
        {
            try
            {
                // Start a task - calling an async function in this example
                Task<string> callTask = Task.Run(() => CallHttp(src));
                // Wait for it to finish
                callTask.Wait();
                // Get the result
                string astr = callTask.Result;
                // Write it our
                //Console.WriteLine(astr);
            }
            catch (Exception ex)  //Exceptions here or in the function will be caught here
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(src, $"Images/{deckName}/{name}.png");// - html-{cnme}.png");
            }
        }

        // Simple async function returning a string...
        static public async Task<string> CallHttp(string src)
        {
            // Just a demo.  Normally my HttpClient is global (see docs)
            HttpClient aClient = new HttpClient();
            // async function call we want to wait on, so wait
            string astr = await aClient.GetStringAsync(src);
            // return the value
            return astr;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            Directory.Delete(destDirName,true);
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
