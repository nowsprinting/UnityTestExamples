# Unity Test Examples

[![meta-check](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml)
[![test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml)
[![Integration Test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test-integration.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test-integration.yml)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/nowsprinting/UnityTestExamples)

Click [English](./README_en.md) for English page if you need.



## このリポジトリについて

このリポジトリは、同人誌『Unity Test Framework完全攻略ガイド』および同『統合テスト編』のサンプルコードです。

電子版 (PDF) は次のWebサイトから購入できます。

#### BOOTH
- [Unity Test Framework完全攻略ガイド 第2版 - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3139036)
- [Unity Test Framework完全攻略ガイド 統合テスト編 - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/4807367)

#### 技術書典マーケット
- [Unity Test Framework完全攻略ガイド 第2版：いか小屋](https://techbookfest.org/product/5936401533108224)
- [Unity Test Framework完全攻略ガイド 統合テスト編：いか小屋](https://techbookfest.org/product/p5zcUfG5sLmgmd7ZtDhXNm)

本リポジトリの `master` ブランチは随時更新しています。
過去の版に準拠したコードを確認したい場合は、次のタグを参照してください。

- `v1.0.0`: Unity Test Framework完全攻略ガイド 第1版
- `v2.0.0`: Unity Test Framework完全攻略ガイド 第2版 および 統合テスト編 第1版



## サンプルプロジェクトの構造

### APIExamples

Unity Test FrameworkおよびNUnit3のAPIカタログ

```
Assets
└── APIExamples
    ├── Scripts
    │   └── (snip)
    └── Tests
        ├── Editor
        │   └── UnityTestFramework
        │       └── 第3章 Edit Modeテスト, 第5章 非同期処理のテスト
        └── Runtime
            ├── NUnit
            │   └── 第6章 アサーション, 第8章 パラメタライズドテスト, 第9章 Unity Test Framework Tips
            └── UnityTestFramework
                └── 第5章 非同期処理のテスト, 第9章 Unity Test Framework Tips
```

### BasicExample

第2章 Unity Test Frameworkの基本

```
Assets
└── BasicExample
    ├── Scenes
    │   └── (snip)
    ├── ScriptableObjects
    │   └── (snip)
    ├── Scripts
    │   ├── Editor
    │   │   └── (snip)
    │   └── Runtime
    │       ├── Entities
    │       │   ├── CharacterStatus.cs          // 「2.4 テストコードの書きかた」のSUT
    │       │   └── (snip)
    │       └── Level
    │           └── (snip)
    └── Tests
        ├── Editor
        │   └── AssetValidators
        │       ├── LevelValidator.cs
        │       └── RaceValidator.cs
        └── Runtime
            └── Entities
                ├── Enums
                │   └── ElementTest.cs
                ├── Settings
                │   └── HitPointGaugeSettingTest.cs // 「14.2 仕様テストにおけるテストケースの考えかた」のテストコード
                ├── CharacterStatueTest.cs      // 「2.4 テストコードの書きかた」のテストコード
                └── PassiveEffectTest.cs        // 「14.2.5 状態遷移テスト」のテストコード
```

### TestDoubleExample

第7章 テストダブル

```
Assets
└── TestDoubleExample
```

### SceneExample

第10章 Sceneを使用するテスト

```
Assets
└── SceneExample
```

### Packages

第11章 UPM パッケージのテスト

#### 埋め込みパッケージ

```
Packages
└── com.nowsprinting.embedded-package-sample
```

#### ローカルパッケージ

```
LocalPackages
└── com.nowsprinting.local-package-sample
```

### UGUIExample

統合テスト編 第2章 uGUI操作の自動化

```
Assets
└── UGUIExample
```

### InputSystemExample

統合テスト編 第3章 Input Systemによる操作の自動化

```
Assets
└── InputSystemExample
```

### InputExample

統合テスト編 第4章 Input Managerによる操作の自動化

```
Assets
└── InputExample
```

### VisualRegressionExample

統合テスト編 第5章 ビジュアルリグレッションテスト

```
Assets
└── VisualRegressionExample
```



## 備考

### utf2 ブランチ

実験的バージョンであったUnity Test Framework v2.0の開発は中断されました[^utf2]。
それに伴ない `utf2` ブランチは削除しました。

[^utf2]: [https://forum.unity.com/threads/unity-test-framework-2-0-ready-for-feedback.1230126/page-3#post-9531214](https://forum.unity.com/threads/unity-test-framework-2-0-ready-for-feedback.1230126/page-3#post-9531214)

### WebGL Player

以下のUnityバージョンで問題が取り除かれたため[^UUM-1170]、WebGLプレイヤーでもUnityTest属性のテストを実行するようにしてあります。

Fixed in 2020.3.42f1, 2021.3.8f1, 2022.1.12f1, 2022.2.0b3, 2023.1.0a4

これらのUnityバージョン未満では、WebGLプレイヤーでPlay Modeテストを実行しようとするとコンパイルエラーとなります。

[^UUM-1170]: [https://issuetracker.unity3d.com/product/unity/issues/guid/UUM-1170](https://issuetracker.unity3d.com/product/unity/issues/guid/UUM-1170)



## License

MIT License
