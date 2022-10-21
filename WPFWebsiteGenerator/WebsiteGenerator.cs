/*
 * De olika inputs som vi vill skicka in i objekten (hemsidorna)
 */
//string[] techniques = { "   C#", "daTAbaser", "WebbuTVeCkling ", "clean Code   " };
using System.IO;
using System;
using System.ComponentModel.Design;
using System.Windows;

namespace WPFWebsiteGenerator
{
     /*
     * Vi skapar ett interface (ett "kontrakt") med de delar som vår klass måste innehålla
     */
    interface Website
    {
        void PrintPage();
        void PrintToFile();
    }

    /*
     * Vi skapar vår WebsiteGenerator klass, med denna kan vi skapa objekt senare
     * Klassen innehåller data och behavior 
     */
    class WebsiteGenerator : Website
    {

        /*
         * De olika egenskaperna (datat) i varje objekt
         */
        string[] messagesToClass, techniques;
        string className;
        string kurser = "";

        /*
         * En konstruktor som tillåter oss att lägga in egen data i objektens egenskaper
         */
        public WebsiteGenerator(string className, string[] messageToClass, string[] techniques)
        {
            this.className = className;
            this.messagesToClass = messageToClass;
            this.techniques = techniques;
        }

        /*
         * Flera olika metoder för att utföra diverse funktionalitet
         * virtual = tillåter oss att override:a (göra egen version utav) metoden i ärvda klasser
         */
        virtual protected string printStart()
        {
            return "<!DOCTYPE html>\n<html>\n<body>\n<main>\n";
        }
        string printWelcome(string className, string[] message)
        {
            string welcome = $"<h1> Välkomna {className}! </h1>";

            string welcomeMessage = "";

            foreach (string msg in message)
            {
                welcomeMessage += $"\n<p><b> Meddelande: </b> {msg} </p>";
            }

            return welcome + welcomeMessage;
        }
        string printKurser()
        {
            return courseGenerator(this.techniques);
        }
        string printEnd()
        {
            return "</main>\n</body>\n</html>";
        }

        public void PrintPage()
        {
            Console.WriteLine(printStart());
            Console.WriteLine(printWelcome(this.className, this.messagesToClass));
            Console.WriteLine(printKurser());
            Console.WriteLine(printEnd());
        }

        public string getFileName()
        {
            string websiteName = "index";
            //Console.WriteLine("Enter filename for website: ");
            try
            {
                websiteName = Console.ReadLine();
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Saving file as: " + websiteName + ".html");
            }

            return websiteName;
        }

        public void PrintToFile()
        {

            try
            {
                FileInfo fi = new FileInfo(getFileName() + ".html");
                FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(printStart());
                    sw.WriteLine(printWelcome(this.className, this.messagesToClass));
                    sw.WriteLine(printKurser());
                    sw.WriteLine(printEnd());

                    sw.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public void Print()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Websitegenerator";
            dlg.DefaultExt = "";
            dlg.Filter = "HTML file (.html)|*.html";
            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                string fileName = dlg.FileName;
                using (StreamWriter sw = new StreamWriter($"{fileName}"))
                {
                    sw.WriteLine(printStart());
                    sw.WriteLine(printWelcome(this.className, this.messagesToClass));
                    sw.WriteLine(printKurser());
                    sw.WriteLine(printEnd());
                }
                MessageBox.Show($"Saved as {fileName}");
            }
        }

        /*
         * En utility metod
         */
        string courseGenerator(string[] techniques)
        {

            foreach (string technique in techniques)
            {
                string tmp = technique.Trim();
                kurser += "<p>" + tmp[0].ToString().ToUpper() + tmp.Substring(1).ToLower() + "</p>\n";
            }

            return kurser;
        }
    }

    /*
     * Här ärver vi egenskaper och metoder ifrån WebsiteGenerator för att kunna återanvända delar i vår StyledWebsiteGenerator
     */
    class StyledWebsiteGenerator : WebsiteGenerator
    {
        // En extra egenskap
        string color;

        /*
         * En utökad konstruktor.
         * Vi vill lägga in alla del egenskaper som behövs i base-klassen vi ärvde ifrån
         * Och också lägga in en färg (data) i vår nya egenskap
         */
        public StyledWebsiteGenerator(string className, string color, string[] messageToClass, string[] techniques) : base(className, messageToClass, techniques)
        {
            this.color = color;
        }

        /*
         * Vi skapar en egen version av printStart (override:ar den) för att kunna få resultatet vi önskar
         */
        override protected string printStart()
        {
            return $"<!DOCTYPE html>\n<html>\n<head>\n<style>\np {{ color: {this.color}; }}\n" +
                              "</style>\n</head>\n<body>\n<main>\n";
        }
    } 
}