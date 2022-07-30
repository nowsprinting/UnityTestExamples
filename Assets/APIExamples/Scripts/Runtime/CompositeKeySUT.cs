// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System;

namespace APIExamples
{
    /// <summary>
    /// テストでは Key1 + Key2 でユニークとみなす。<see cref="IEquatable{T}"/>は実装していない
    /// </summary>
    public class CompositeKeySUT
    {
        public readonly string Key1;
        public readonly string Key2;
        public int Value;

        public CompositeKeySUT(string key1, string key2, int value)
        {
            this.Key1 = key1;
            this.Key2 = key2;
            this.Value = value;
        }
    }
}
