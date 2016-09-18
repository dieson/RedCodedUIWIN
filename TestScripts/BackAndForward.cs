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


namespace Test.TestScripts
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class BackAndForward
    {
        private static UIMap map = new UIMap();
        public UIPublicationItemsList itemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;
        public XamlButton menuBack = map.UILexisNexisRedWindow.UIMenuBack;
        public XamlButton menuForward = map.UILexisNexisRedWindow.UIMenuForward;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private KeyTapContent keyTapContent = new KeyTapContent();
        private KeyContent keyContent = new KeyContent();


        [TestMethod]
        public void backAndForward()
        {
            Chapter chapterForward = new Chapter();
            Chapter chapterBack = new Chapter();
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            choseBook(getOperactionIndex());
            Playback.Wait(3000);
            chapterForward = keyTapContent.enterChapter(1);
            keyTapContent.goToc();
            chapterBack = keyTapContent.enterChapter(2);
            back(chapterForward);
            forward(chapterBack);
            keyLaunch.closeApp();
        }

        private void back(Chapter ch)
        {
            pressBack();
            if ( !verifyChapter(ch, keyTapContent.getChapter(ch.getTitle())))
            {
                Assert.Fail("Press back button return wrong page");
            }
        }

        private void forward(Chapter ch)
        {
            pressForward();
            if (!verifyChapter(ch, keyTapContent.getChapter(ch.getTitle())))
            {
                Assert.Fail("Press forward button go to wrong page");
            }
        }

        private bool verifyChapter(Chapter c1, Chapter c2)
        {
            if (c1.getTitle().Trim().Equals(c2.getTitle().Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void pressBack(){
            Gesture.Tap(menuBack);
        }

        private void pressForward(){
            Gesture.Tap(menuForward);
        }

        private void choseBook(int index)
        {
            Gesture.Tap(itemsList.GetChildren()[index]);
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
            int status = itemsList.GetChildren()[index].GetChildren().Count - 3;
            if (itemsList.GetChildren()[index].GetChildren()[status].Name.Equals("Download"))
            {
                canDownload = true;
            }
            return canDownload;
        }


        private int getBookNum()
        {
            bookNum = itemsList.GetChildren().Count;
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
