﻿using System;

namespace WeCantSpell.Hunspell
{
    sealed class WordEntry : IEquatable<WordEntry>
    {
        public static bool operator ==(WordEntry a, WordEntry b) => a is null ? b is null : a.Equals(b);

        public static bool operator !=(WordEntry a, WordEntry b) => a is null ? !(b is null) : !a.Equals(b);

        public WordEntry(string word, FlagSet flags, MorphSet morphs, WordEntryOptions options)
            : this(word, new WordEntryDetail(flags, morphs, options)) { }

        public WordEntry(string word, WordEntryDetail detail)
        {
#if DEBUG
            if (word == null) throw new ArgumentNullException(nameof(word));
            if (detail == null) throw new ArgumentNullException(nameof(detail));
#endif
            Word = word;
            Detail = detail;
        }

        public string Word { get; }

        public WordEntryDetail Detail { get; }

        public bool ContainsFlag(FlagValue flag) => Detail.ContainsFlag(flag);

        public bool Equals(WordEntry other) =>
            !(other is null)
            && string.Equals(other.Word, Word, StringComparison.Ordinal)
            && other.Detail.Equals(Detail);

        public override bool Equals(object obj) => Equals(obj as WordEntry);

        public override int GetHashCode() => unchecked(Word.GetHashCode() ^ Detail.GetHashCode());
    }
}
