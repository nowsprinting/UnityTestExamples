// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using NUnit.Framework;
using TestHelper.Attributes;
using UnityEngine;
using UnityEngine.TestTools;

[assembly: Category("Integration")]
[assembly: GameViewResolution(1920, 1080, "Full HD")]

[assembly: UnityPlatform(RuntimePlatform.OSXEditor)] // Expected画像がOSXで撮影したもののため
