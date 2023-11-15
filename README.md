# Unity Test Examples

[![meta-check](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml)
[![test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml)
[![Integration Test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test-integration.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test-integration.yml)

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



## サンプルプロジェクトの構造

統合テスト編のサンプルには、 **Integration** カテゴリーを設定しています。

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

> **Note**  
> 『Unity Test Framework完全攻略ガイド 第3版』より、6.4「カスタムアサーション」の例を Test Helper パッケージ（com.nowsprinting.test-helper）に含まれる [GameObjectNameComparer](https://github.com/nowsprinting/test-helper#gameobjectnamecomparer) および [DestroyedConstraint](https://github.com/nowsprinting/test-helper#destroyed) を日本語訳したものに置き換えました。  
> 変更差分はコミット &lt;[dc1b643](https://github.com/nowsprinting/UnityTestExamples/commit/dc1b643cd7e1275388881933b5edfcabde0413ba)&gt; を参照してください。



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

### InternalsVisibleToExample

第9章 Unity Test Framework Tips

```
Assets
└── InternalsVisibleToExample   // 「9.10 internalメソッドのテストを書きたい」
```

### SceneExample

第10章 Sceneを使用するテスト

```
Assets
└── SceneExample
```

> **Note**  
> 『Unity Test Framework完全攻略ガイド 第3版』より、10.2.2 および 10.3 で紹介している Scene のロード処理を、Test Helper パッケージ（com.nowsprinting.test-helper）に含まれる [LoadScene](https://github.com/nowsprinting/test-helper#loadscene) 属性を使用する形に置き換えました。  
> 変更差分はコミット &lt;[86f8ab9](https://github.com/nowsprinting/UnityTestExamples/commit/86f8ab9373d9d1c7de0c7cde925adc1a94aaafe0)&gt; を参照してください。

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



## WebGL Player

以下のUnityバージョンで問題が取り除かれたため（[UUM-1170](https://issuetracker.unity3d.com/issues/webgl-chrome-the-message-header-is-corrupted-and-for-security-reasons-connection-will-be-terminated-dot-errors)）、WebGLプレイヤーでもUnityTest属性のテストを実行するようにしてあります。

Fixed in 2020.3.42f1, 2021.3.8f1, 2022.1.12f1, 2022.2.0b3, 2023.1.0a4

これらのUnityバージョン未満では、WebGLプレイヤーでPlay Modeテストを実行しようとするとコンパイルエラーとなります。



## Unity Test Framework v2.0

Add examples for Unity Test Framework v2.0 [#1](https://github.com/nowsprinting/UnityTestExamples/pull/1)を参照してください



## License

MIT License
