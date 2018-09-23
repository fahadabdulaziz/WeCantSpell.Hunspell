﻿using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites
{
    [SimpleJob(launchCount: 1, warmupCount: 2, targetCount: 3), RankColumn, MemoryDiagnoser]
    public class EnUsWordListCheckSuite
    {
        private WordList WordList;

        private CategorizedWordData WordData;

        private const int MaxWords = 1000;

        [GlobalSetup]
        public void Setup()
        {
            WordList = WordList.CreateFromFiles(Path.Combine(DataFilePaths.TestFilesFolderPath, "English (American).dic"));

            WordData = CategorizedWordData.Create(
                CategorizedWordData.GetAssortedEnUsWords(),
                isCorrect: WordList.Check,
                isRoot: WordList.ContainsEntriesForRootWord);
        }


        [Benchmark(Description = "Check an assortment of words", Baseline = true)]
        public void CheckAllWords()
        {
            foreach (var word in WordData.AllWords.Take(MaxWords))
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check root words")]
        public void CheckRootWords()
        {
            foreach (var word in WordData.RootWords.Take(MaxWords))
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check correct words")]
        public void CheckCorrectWords()
        {
            foreach (var word in WordData.CorrectWords.Take(MaxWords))
            {
                var result = WordList.Check(word);
            }
        }

        [Benchmark(Description = "Check wrong words")]
        public void CheckWrongWords()
        {
            foreach (var word in WordData.WrongWords.Take(MaxWords))
            {
                var result = WordList.Check(word);
            }
        }
    }
}
