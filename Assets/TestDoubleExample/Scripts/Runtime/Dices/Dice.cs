// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

namespace TestDoubleExample.Dices
{
    /// <summary>
    /// テスト可能なダイス
    /// 擬似乱数発生器をコンストラクタインジェクションで受け取ります
    /// </summary>
    public class Dice
    {
        private readonly IRandom _random;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="random">このクラスで使用する擬似乱数発生器</param>
        public Dice(IRandom random)
        {
            _random = random;
        }

        /// <summary>
        /// 六面体ダイスを振る
        /// </summary>
        /// <returns>1〜6のランダムな値</returns>
        public int Roll()
        {
            return _random.Next(1, 7);
        }
    }
}
