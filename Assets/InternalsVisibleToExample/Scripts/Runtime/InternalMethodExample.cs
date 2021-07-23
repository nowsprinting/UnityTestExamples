// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

namespace InternalsVisibleToExample
{
    public class InternalMethodExample
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        internal bool AlwaysTrue()
        {
            return true;
        }
    }
}
