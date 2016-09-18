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
    public class downloadBooks
    {
        private static UIMap map = new UIMap();
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email =Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private int downloadIndex = 0;


        [TestMethod]
        public void downloadBook()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            goTest();
            keyLaunch.closeApp();
        }

        protected void goTest()
        {
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(3000);
            downloadIndex = getDownloadIndex();
            beginDownload(downloadIndex);
            bool a = verifyProgressBar(downloadIndex);
            if (downloadStatus(downloadIndex))
            {
                keyLaunch.closeApp();
            }
            else
            {
                Assert.Fail("Failed to download the book after 600 seconds");
            }
        }



        private void beginDownload(int index)
        {
            if (index == 666)
            {
                Assert.Fail("There is no book to download");
            }
            else
            {
                int downloadButton = publicationItemsList.GetChildren()[index].GetChildren().Count - 4;
                Gesture.Tap(publicationItemsList.GetChildren()[index].GetChildren()[downloadButton]);
            }
           
        }


        private int getDownloadIndex()
        {
            int downloadIndex = 666;
            bookNum = getBookNum();
            for (int i = 0; i < bookNum; i++)
            {
                if(verifyDownload(i))
                {
                    downloadIndex = i;
                    break;
                }
            }
            return downloadIndex;
        }


        private bool downloadStatus(int index)
        {
            bool isCompleted = false;
            int retryTime = 20;
            for (int i = 0; i < retryTime; i++)
            {
                if (verifyProgressBar(index))
                {
                    Playback.Wait(30000);
                }
                else
                {
                    isCompleted = true;
                    break;
                }
            }
            return isCompleted;
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

        private bool verifyProgressBar(int index)
        {
            bool bar = false;
            int status = publicationItemsList.GetChildren()[index].GetChildren().Count - 3;
            if (publicationItemsList.GetChildren()[index].GetChildren()[status].ClassName.Equals("ProgressBar"))
            {
                bar = true;
            }
            return bar;
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
