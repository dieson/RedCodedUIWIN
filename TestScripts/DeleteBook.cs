using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Collections;


namespace Test.TestScripts
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class DeleteBook
    {
        private static UIMap map = new UIMap();
        public XamlList publicationsCollectiList = map.UILexisNexisRedWindow.UIItemPane.UIPublicationsCollectiList;
        public XamlButton confirmButton = map.UILexisNexisRedWindow.UIPopupWindow.UIConfirmButton;
        public XamlButton settingsButton = map.UILexisNexisRedWindow.UISettingsButton;
        public UIPublicationItemsList itemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;
        public XamlListItem listItem = map.UILexisNexisRedWindow.UITOCListList.UILexisNexisRedWindowsListItem;


        private ArrayList booklist = new ArrayList();
        private ArrayList settingBookList = new ArrayList();
        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;


        [TestMethod]
        public void deleteBook()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(3000);
            verifyTitleMatch();
            verifyDeleteSuccess(delete());
            keyLaunch.closeApp();
        }

        private String delete()
        {
            String deletedTitle = "";
            deletedTitle = publicationsCollectiList.GetChildren()[0].GetChildren()[0].Name;
            Gesture.Tap(publicationsCollectiList.GetChildren()[0]);
            int deleteButton = publicationsCollectiList.GetChildren()[0].GetChildren().Count - 1;
            Gesture.Tap(publicationsCollectiList.GetChildren()[0].GetChildren()[deleteButton]);
            Gesture.Tap(confirmButton);
            return deletedTitle;
        }



        private void verifyDeleteSuccess(String deleteTitle)
        {
            Playback.Wait(2000);
            Gesture.Tap(listItem);
            Playback.Wait(2000);
            getBookList();
            if (verifyTitleExist(deleteTitle))
            {
                Assert.Fail("Delete book '" + deleteTitle + "' failed, the book still exist in the book list");
            }
        }


        private void verifyTitleMatch()
        {
            getBookList();
            getSettingBookList();
            if (settingBookList.Count == booklist.Count)
            {
                for (int i = 0; i < booklist.Count; i++)
                {
                    if (!verifyTitleExist(settingBookList[i].ToString()))
                    {
                        Assert.Fail("The booklists in mainpage and setting page are not matched");
                    }
                }
            }
            else
            {
                Assert.Fail("The booklists in mainpage and setting page are not matched");
            }
        }


        private bool verifyTitleExist(String title)
        {
            bool isExist = false;
            for (int i = 0; i < booklist.Count; i++)
            {
                if (booklist[i].ToString().Trim().Equals(title.Trim()))
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }



        private void getBookList()
        {
            this.booklist.Clear();
            bookNum = getBookNum();
            for (int i = 0; i < bookNum; i++)
            {
                booklist.Add(getBookTitle(i));
            }
        }


        private void getSettingBookList()
        {
            Gesture.Tap(settingsButton);
            int listCount = publicationsCollectiList.GetChildren().Count;
            for (int i = 0; i < listCount; i++)
            {
                settingBookList.Add(getSettingBookTitle(i));
            }
        }

        private int getBookNum()
        {
            bookNum = itemsList.GetChildren().Count;
            if (bookNum == 0)
            {
                Assert.Fail("Can not find any books in the page");
            }
            return bookNum;
        }

        private String getBookTitle(int index)
        {
            String title = "";
            title = itemsList.GetChildren()[index].GetChildren()[0].Name;
            return title;
        }

        private String getSettingBookTitle(int index)
        {
            String title = "";
            title = publicationsCollectiList.GetChildren()[index].GetChildren()[0].Name;
            return title;
        }







        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        
    }
}
