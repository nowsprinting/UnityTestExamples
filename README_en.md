# Unity Test Examples

[![meta-check](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/metacheck.yml)
[![test](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml/badge.svg)](https://github.com/nowsprinting/UnityTestExamples/actions/workflows/test.yml)

Click [日本語](./README.md) for the Japanese page if you need.



## About this repository

This repository is a sample project for the "Unity Test Framework Perfect Guidebook".

Books can be purchased from the following websites, but they are written in Japanese.

#### BOOTH
[Unity Test Framework完全攻略ガイド【電子版】 - いか小屋 - BOOTH](https://ikagoya.booth.pm/items/3139036)

#### Tech Book Fest Market
[Unity Test Framework完全攻略ガイド：いか小屋](https://techbookfest.org/product/5936401533108224)



## About English support

Test method names and comments in the sample code are written in Japanese. 

We are planning to add the comments in English little by little.
For test method names, English comments will be added by the `Description` attribute.



## Structure of sample project

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
                ├── CharacterStatueTest.cs      // Test code of "2.4 How to write test code"
                ├── Enums
                │   └── ElementTest.cs
                └── Settings
                    └── HitPointGaugeSettingTest.cs
```

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



## License

MIT License
