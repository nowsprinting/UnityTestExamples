// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 決定的なテストが可能なじゃんけんロジック
    /// </summary>
    public class Janken
    {
        private readonly IRandom _random;

        /// <summary>
        /// 擬似乱数発生器をコンストラクタインジェクションで受け取ります
        /// </summary>
        /// <param name="random">擬似乱数発生器</param>
        public Janken(IRandom random)
        {
            _random = random;
        }

        /// <summary>
        /// じゃんけんぽん
        /// </summary>
        /// <returns>ランダムな手（本当にランダムかどうかは_randomに依存）</returns>
        public Hand Pon()
        {
            return (Hand)_random.Next(0, 3);
        }
    }
}
