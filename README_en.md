# Unity Test Examples

[![meta-check](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml)
[![test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml)

Click [日本語](./README.md) for the Japanese page if you need.



## About this repository

This repository is a sample project for the "Unity Test Framework Perfect Guidebook".

Books can be purchased from the following websites, but they are written in Japanese.

#### BOOTH
[Unity Test Framework完全攻略ガイド 第2版 - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3139036)

#### Tech Book Fest Market
[Unity Test Framework完全攻略ガイド 第2版：いか小屋](https://techbookfest.org/product/5936401533108224)



## About English support

Test method names and comments in the sample code are written in Japanese. 

We are planning to add the comments in English little by little.
For test method names, English comments will be added by the `Description` attribute.



## Structure of sample project

### APIExamples

API catalog of Unity Test Framework and NUnit3

```
Assets
└── APIExamples
    ├── Scripts
    │   └── (snip)
    └── Tests
        ├── Editor
        │   └── UnityTestFramework
        │       └── Chapter 3 Edit Mode tests, Chapter 5 Test asynchronous processing
        └── Runtime
            ├── NUnit
            │   └── Chapter 6 Assertion, Chapter 8 Parameterized Tests, Chapter 9 Unity Test Framework Tips
            └── UnityTestFramework
                └── Chapter 5 Test asynchronous processing, Chapter 9 Unity Test Framework Tips
```

### BasicExample

Chapter 2 Unity Test Framework Basics

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
    │       │   ├── CharacterStatus.cs          // SUT of "2.4 How to write test code"
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
                │   └── HitPointGaugeSettingTest.cs // Test code of "14.2 Test cases via specification testing"
                ├── CharacterStatueTest.cs      // Test code of "2.4 How to write test code"
                └── PassiveEffectTest.cs        // Test code of "14.2.5 State transition testing"
```

### TestDoubleExample

Chapter 7 Test Doubles

```
Assets
└── TestDoubleExample
```

### InternalsVisibleToExample

9.10 Testing internal method

```
Assets
└── InternalsVisibleToExample
```

### SceneExample

Chapter 10 Testing using Scene

```
Assets
└── SceneExample
```

### Packages

Chapter 11 Testing UPM packages

#### Embedded package

```
Packages
└── com.nowsprinting.embedded-package-sample
```

#### Local package

```
LocalPackages
└── com.nowsprinting.local-package-sample
```



## WebGL Player

The following Unity versions have removed the problem [UUM-1170](https://issuetracker.unity3d.com/issues/webgl-chrome-the-message-header-is-corrupted-and-for-security-reasons-connection-will-be-terminated-dot-errors), so run the UnityTestAttribute test on the WebGL player as well.

Fixed in 2020.3.42f1, 2021.3.8f1, 2022.1.12f1, 2022.2.0b3, 2023.1.0a4



## Unity Test Framework v2.0

See: Add examples for Unity Test Framework v2.0 [#1](https://github.com/nowsprinting/UnityTestExamples/pull/1)



## License

MIT License
