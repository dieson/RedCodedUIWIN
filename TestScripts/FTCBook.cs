using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Test.TestScripts
{
 
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class FTCBook
    {
        private static UIMap map = new UIMap();
        public UITOCListList tocList = map.UILexisNexisRedWindow.UITOCListList;
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;

        [TestMethod]
        public void CodedUITestMethod1()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            choseBook(getOperactionIndex());
            Playback.Wait(2000);
            verifyCaseExist();
            keyLaunch.closeApp();
        }

        private void verifyCaseExist()
        {
            int contentSize = tocList.GetChildren().Count;
            String bookTitle = tocList.GetChildren()[contentSize - 1].GetChildren()[0].Name;
            if (!bookTitle.ToLower().Contains("case"))
            {
                Assert.Fail("There is no case chapter in the FTC book");
            }
        }

        private String choseBook(int index)
        {
            if (index >= publicationItemsList.GetChildren().Count)
            {
                Assert.Fail("There is no FTC book found in the list");
            }
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
                if ((!verifyDownload(i)) && 
                      verifyFTC(i))
                {
                    operationIndex = i;
                    break;
                }
            }
            return operationIndex;
        }

        private bool verifyFTC(int index)
        {
            bool isFTC = false;
            if (publicationItemsList.GetChildren()[index].GetChildren()[1].Name.Equals("True"))
            {
                isFTC = true;
            }
            return isFTC;
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
