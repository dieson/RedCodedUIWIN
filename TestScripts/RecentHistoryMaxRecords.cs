using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Test.Keywords;
using Test.Model;
using System.Collections;

namespace Test.TestScripts
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class RecentHistoryMaxRecords
    {
        private static UIMap map = new UIMap();
        public XamlButton backButton = map.UILexisNexisRedWindow.UIBackButton;
        public XamlList recentHistoryItemsList = map.UILexisNexisRedWindow.UIItemPane.UIRecentHistoryItemsList;
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        ArrayList historyList = new ArrayList();
        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private KeyTapContent keyTapContent = new KeyTapContent();
        private KeyContent keyContent = new KeyContent();

        [TestMethod]
        public void CodedUITestMethod1()
        {
            Chapter chapter = new Chapter();
            History history = new History();
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            history.setBook(choseBook(getOperactionIndex()));
            Playback.Wait(3000);
            for (int i = 0; i < 10; i++)
            {
                keyTapContent.enterChapter(1);
                keyTapContent.goToc();
            }
            chapter = keyTapContent.enterChapter(2);
            history.setChapter(chapter.getTitle());
            Gesture.Tap(backButton);
            Playback.Wait(1000);
            if (!verifyHistory(history))
            {
                Assert.Fail("The history record is not correct");
            }
            keyLaunch.closeApp();
        }


        
        private bool verifyHistory(History history)
        {
            bool rs = false;
            String book = recentHistoryItemsList.GetChildren()[0].GetChildren()[0].Name;
            String chapter = recentHistoryItemsList.GetChildren()[0].GetChildren()[2].Name;
            if(book.Trim().Equals(history.getBook().Trim()) &&
                history.getChapter().Trim().Equals(chapter.Trim()))
            {
                rs = true;
            }
            return rs;
        }


        private String choseBook(int index)
        {
            String book = publicationItemsList.GetChildren()[index].GetChildren()[0].Name;
            Gesture.Tap(publicationItemsList.GetChildren()[index]);
            return book;
        }


        private int getOperactionIndex()
        {
            int operationIndex = 666;
            bookNum = getBookNum();
            for (int i = 0; i < bookNum; i++)
            {
                if (!verifyDownload(i))
                {
                    operationIndex = i;
                    break;
                }
            }
            return operationIndex;
        }

        private bool verifyDownload(int index)
        {
            bool canDownload = false;
            int status = publicationItemsList.GetChildren()[index].GetChildren().Count - 3;
            if (publicationItemsList.GetChildren()[index].GetChildren()[status].Name.Equals("Download"))
            {
                canDownload = true;
            }
            return canDownload;
        }


        private int getBookNum()
        {
            bookNum = publicationItemsList.GetChildren().Count;
            return bookNum;
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
