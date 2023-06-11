// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Threading.Tasks;
using NUnit.Framework;
using UGUIExample.PageObjects;

namespace UGUIExample
{
    /// <summary>
    /// ページオブジェクトパターンを使用したシナリオテストの例
    /// </summary>
    [TestFixture]
    public class ScenarioTest
    {
        [Test]
        public async Task Login_ユーザー名とパスワードを入力してログインボタンをクリック_メインメニューに遷移()
        {
            var loginPage = await LoginPage.Load();
            loginPage.InputUsername("Taro");
            loginPage.InputPassword("12345678");

            var mainMenuPage = await loginPage.Login();

            Assert.That(mainMenuPage.IsShown, Is.True);
        }

        [Test]
        public async Task MainMenu_難易度設定画面に遷移してEASYボタンをクリック_メインメニューに戻る()
        {
            var mainMenuPage = await MainMenuPage.Load();
            var difficultyPage = await mainMenuPage.DifficultySelect();
            Assert.That(difficultyPage.IsShown, Is.True);

            var returnedMainMenuPage = await difficultyPage.Easy();
            Assert.That(returnedMainMenuPage.IsShown, Is.True);
        }

        [Test]
        public async Task MainMenu_ヘルプ画面に遷移してCloseボタンをクリック_メインメニューに戻る()
        {
            var mainMenuPage = await MainMenuPage.Load();
            var helpPage = await mainMenuPage.Help();
            Assert.That(helpPage.IsShown, Is.True);

            var returnedMainMenuPage = await helpPage.Close();
            Assert.That(returnedMainMenuPage.IsShown, Is.True);
        }
    }
}
