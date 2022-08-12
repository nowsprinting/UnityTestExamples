# Unity Test Examples

[![meta-check](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml)
[![test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml)

Click [English](./README_en.md) for English page if you need.



## このリポジトリについて

このリポジトリは、同人誌『Unity Test Framework完全攻略ガイド』のサンプルコードです。

電子版 (pdf) は次のWebサイトから購入できます。

#### BOOTH
[Unity Test Framework完全攻略ガイド【電子版】 - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3139036)

#### 技術書典マーケット
[Unity Test Framework完全攻略ガイド：いか小屋](https://techbookfest.org/product/5936401533108224)



## サンプルプロジェクトの構造

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

### TestDoubleExample

第7章 テストダブル

```
Assets
└── TestDoubleExample
```

### InternalsVisibleToExample

9.10 internalメソッドのテストを書きたい

```
Assets
└── InternalsVisibleToExample
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



## Unity Test Framework v2.0

Add examples for Unity Test Framework v2.0 [#1](https://github.com/nowsprinting/UnityTestExamples/pull/1)を参照してください



## License

MIT License
