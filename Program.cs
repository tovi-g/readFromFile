using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace ReadFromFile
{
    class Program
    {
        static int NumOfLines(System.IO.StreamReader file)
        {
            string line;
            int numOfLines = 0;
            while ((line = file.ReadLine()) != null) { numOfLines++; }
            return numOfLines;
        }

        static int NumOfWords(System.IO.StreamReader file)
        {
            string line;
            int numOfWords = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split();
                numOfWords += words.Length;
            }
            return numOfWords;
        }
        static int DistinctWords(System.IO.StreamReader file)
        {
            HashSet<string> distinctWords = new HashSet<string>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var word1 = "";
                string[] words = line.Split();
                foreach (var word in words)
                {
                    word1 = word.TrimEnd('.');
                    distinctWords.Add(word1.ToLower());
                }
            }
            return distinctWords.Count;
        }
        static int AverageLength(System.IO.StreamReader file, out int maxLenght)
        {
            maxLenght = 0;
            string line;
            int numOfWordsInSentence = 0, sumOfSentences = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] sentence = line.Split(".");
                sumOfSentences += sentence.Length - 1;
                var word1 = "";
                foreach (var word in sentence)
                {
                    if (word.StartsWith(" "))
                    {
                        word1 = word.Substring(1);
                    }
                    else word1 = word;
                    string[] words = word1.Split();
                    if (word1 != "") { numOfWordsInSentence += words.Length; }

                    if (words.Length > maxLenght) { maxLenght = words.Length; }
                }
            }
            return numOfWordsInSentence / sumOfSentences;
        }
        static int SubTextWithoutK(System.IO.StreamReader file)
        {
            string line;
            int max = 0, count = 0;
            //read file line by line
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split();
                foreach (var word in words)
                {
                    if (word.ToLower().Contains('k'))
                    {
                        max = Math.Max(count, max);
                        count = 0;
                    }
                    else count++;
                }
            }
            return Math.Max(count, max);
        }
        static Dictionary<String, int> ColorsDictionary(System.IO.StreamReader file)
        {
            string line;
            Dictionary<string, int> colorDictionary = new Dictionary<string, int>();
            while ((line = file.ReadLine()) != null)
            {
                var colorName1 = "";
                string[] words = line.Split();
                foreach (var colorName in words)
                {
                    colorName1 = colorName;
                    colorName1 = colorName.TrimEnd('.');
                    if (IsColor(colorName1))
                    {
                        if (!colorDictionary.ContainsKey(colorName1))
                        {
                            colorDictionary.Add(colorName1, 1);
                        }
                        else
                        {
                            colorDictionary[colorName1]++;
                        }
                    }
                }
            }
            return colorDictionary;
        }
        public static bool IsColor(string colorName)
        {
            //returns the ABG values of the number
            Color color = Color.FromName(colorName);
            //if it is not a color all the parameters are equal to 0
            return !(color.R == 0 && color.B == 0 && color.A == 0 && color.G == 0);
        }
        static void Main(string[] args)
        {
            //create string to acsses the file
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            //read the file
            using (System.IO.StreamReader file = new System.IO.StreamReader(_filePath + "/file.txt"))
            {
                //return StreamReader to beginning 
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("1.Num of lines: " + NumOfLines(file));
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                int numOfWords = NumOfWords(file);
                Console.WriteLine("2.Num of words:" + " " + numOfWords);
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("3.Num of distinct words:  " + DistinctWords(file));
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("4.1.Average number of words in a sentence: " + AverageLength(file, out int maxLenght));
                Console.WriteLine("4.2.max lenght of sentence: " + maxLenght);
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("6.Max length of sub text without K: " + SubTextWithoutK(file));
                file.DiscardBufferedData();
                file.BaseStream.Seek(0, SeekOrigin.Begin);
                Console.Write("7.");
                Dictionary<string, int> colorDictionary = ColorsDictionary(file);
                foreach (var item in colorDictionary)
                {
                    Console.WriteLine("color: " + item.Key + " occurrences: " + item.Value);
                }
            }
            Console.ReadLine();
        }

    }
}
