// Copyright (c) 2021-2025 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Janken
{
    /// <summary>
    /// 決定的なテストが可能なじゃんけんロジック
    /// </summary>
    public class Janken
    {
        private readonly IRandom _random; // 依存オブジェクト

        /// <summary>
        /// 擬似乱数生成器をコンストラクタインジェクションで受け取るコンストラクタ
        /// </summary>
        /// <param name="random">擬似乱数生成器</param>
        public Janken(IRandom random = null)
        {
            _random = random ?? new UnityEngineRandom();
        }

        /// <summary>
        /// じゃんけんぽん
        /// </summary>
        /// <returns>ランダムな手（本当にランダムかどうかは <c>_random</c> に依存）</returns>
        public Hand Pon()
        {
            var value = _random.Range(0.0f, 1.0f); // 0.0〜1.0のランダムな値が返る
            if (value < 0.33f)
            {
                return Hand.Rock;
            }

            if (value < 0.66f)
            {
                return Hand.Scissors;
            }

            return Hand.Paper;
        }
    }
}
