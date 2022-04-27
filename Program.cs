using System;

namespace WordUnscrambler
{
    class Program
    {
        static void Main(String[] args) 
        {
            Console.WriteLine("Hello world!");
            Console.WriteLine("How to input words to scramble: # F - file :: # M - Manually");
            char option = Convert.ToChar(Console.ReadLine() ?? "M");
           
            Console.WriteLine("The option is: "+ option);
            option = char.ToLower(option);
            
            String commaSeparatedWords = "";
            String[] commaSeparatedWordsArray;
            List<string> userScrambledWords = new List<string>();
            switch (option)
            {
                case 'f':
                    Console.WriteLine("You gonna provide a file!");
                    Console.Write("Enter the file path: ");
                    break;

                case 'm':
                    Console.WriteLine("\nYou gonna provide the words comma-separated!");
                    Console.Write("\nEnter the words: ");
                    commaSeparatedWords = Console.ReadLine() ?? "";
                    commaSeparatedWordsArray = commaSeparatedWords.Split(",");
                    userScrambledWords = commaSeparatedWordsArray.ToList();
                    
                    Console.Write("You entered: [");
                    foreach(var item in userScrambledWords) {
                        Console.Write(item + ", ");
                    }
                    Console.WriteLine("\b\b]");

                    break;
                default:
                    Console.WriteLine("No such option! Type *F* or *M*");
                    break;
            }
            var loadedWords = File.ReadAllLines("./wordsList.txt");

            //Console.WriteLine("\nThe loaded words are: ");
            // printListItems(loadedWords);

            // This list will store words that matches.
            var matchedWords = new List<string>();

            foreach(var scrambledWord in userScrambledWords) {
                foreach(var unscrambledWord in loadedWords) {
                    if (doWordsMatched(scrambledWord, unscrambledWord)) {
                        matchedWords.Add(unscrambledWord);
                    } else {
                         /**
                            * Before doing this test, we must be sure
                             that the both words have the same length.
                        */
                        if( scrambledWord.Length == unscrambledWord.Length ) {
                            char[] arr1 = scrambledWord.ToCharArray();
                            char[] arr2 = unscrambledWord.ToCharArray();

                            Array.Sort(arr1);
                            Array.Sort(arr2);

                            if (areCharsArraySame(arr1, arr2)) matchedWords.Add(unscrambledWord);
                    }
                }
            }

            Console.WriteLine("Words that mathes are: ");
            printListItems(matchedWords);

           /*  Console.WriteLine("Test test test: ");
            string de = "gexa";
            Console.WriteLine("Before sort: "+ de);
            char[] dec = de.ToCharArray();

             Array.Sort(dec);
            foreach(var item in dec) {
                Console.WriteLine("After sort: " + item);
            } */



        }

        // This helper method compares two arrays of char.
        static bool areCharsArraySame(char[] arr1, char[] arr2) {
            bool response = false;
            for (int i = 0; i < arr1.Length; i++) {
                if (arr1[i] != arr2[i]) {
                    response = false;
                } else {
                    response = true;
                }
            }
            return response;
        }

        // This helper method is used to lowercase and compare to given strings.
        static bool doWordsMatched(string scrambledWord, string unscrambledWord)
        {
            return scrambledWord.ToLower().Equals(unscrambledWord.ToLower());
        }

        static void printListItems(List<string> list)
        {
            foreach (var item in list) Console.WriteLine(item);
        }
    }
}