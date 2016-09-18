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
    public class DownloadBooksTwo
    {
        private static UIMap map = new UIMap();
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = "moran.cheng.1@Lexis.com";
        private String password = "bgt56yhn";

        [TestMethod]
        public void downloadTwoBooks()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            goTest();
            keyLaunch.closeApp();
        }

        protected void goTest()
        {
            ArrayList downloadBooks = new ArrayList();
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(3000);
            downloadBooks = getDownloadIndex();
            if (downloadBooks.Count < 2)
            {
                Assert.Fail("There is no enough books to download, please check");
            }
            else{
                for(int i = 0; i < 2; i++)
                {
                    int index = (int)downloadBooks[i];
                    beginDownload(index);
                    Playback.Wait(1000);
                }

                int book1Index = (int)downloadBooks[0];
                int book2Index = (int)downloadBooks[1];
                if (downloadStatus(book1Index, book2Index))
                {
                    keyLaunch.closeApp();
                }
                else
                {
                    Assert.Fail("Failed to download the book after 600 seconds");
                }
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


        private ArrayList getDownloadIndex()
        {
            ArrayList downloadIndex = new ArrayList();
            bookNum = getBookNum();
            for (int i = 0; i < bookNum; i++)
            {
                if (verifyDownload(i))
                {
                    downloadIndex.Add(i);
                    if (downloadIndex.Count >= 2)
                    {
                        break;
                    }
                }
            }
            return downloadIndex;
        }


        private bool downloadStatus(int book1, int book2)
        {
            bool isCompleted = false;
            int retryTime = 20;
            for (int i = 0; i < retryTime; i++)
            {
                bool book1Status = verifyProgressBar(book1);
                bool book2Status = verifyProgressBar(book2);
                if ( book1Status && book2Status)
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
