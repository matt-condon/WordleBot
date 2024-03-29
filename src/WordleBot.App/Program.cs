﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleBot.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var guessesDictionary = new Dictionary<int, int>();

            var WordListRepo = new WordListRepository();
            var Guesser = new GuessOrchestrator();
            
            var possibleWords = WordListRepo.GetAllPossibleWordsFromFile();

            //var correctWord = "";
            foreach (var correctWord in possibleWords)
            {
                Guesser.MakeGuess(possibleWords, correctWord, out int numberOfGuesses);

                guessesDictionary.TryGetValue(numberOfGuesses, out int currentCount);
                guessesDictionary[numberOfGuesses] = currentCount + 1;
            }

            var numberOfGuessesSorted = guessesDictionary.Keys.ToList();
            numberOfGuessesSorted.Sort();

            Console.WriteLine("Guesses:");

            foreach (var k in numberOfGuessesSorted)
            {
                Console.WriteLine("{0} = {1}", k, guessesDictionary[k]);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Time to finish: {elapsedMs}ms");
        }
    }
}
