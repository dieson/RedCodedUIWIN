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
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class DownloadCancel
    {
        private static UIMap map = new UIMap();
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;
        public XamlButton confirmButton = map.UILexisNexisRedWindow.UIPopupWindow.UIConfirmButton;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private int downloadIndex = 0;
        bool a;

        [TestMethod]
        public void downloadCancel()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            goTest();
        }

        protected void goTest()
        {
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(3000);
            downloadIndex = getDownloadIndex();
            beginDownload(downloadIndex);
            cancelDownload(downloadIndex);
            verifyCanceled(downloadIndex);
            keyLaunch.closeApp();
        }

        private bool verifyCanceled(int index)
        {
            bool isCanceled = false;
            int bookStatus = publicationItemsList.GetChildren()[index].GetChildren().Count - 3;
            String status = publicationItemsList.GetChildren()[index].GetChildren()[bookStatus].Name;
            if (status.Equals("Try again"))
            {
                isCanceled = true;
            }
            else
            {
                Assert.Fail("Canceled download the book failed");
            }

            return isCanceled;
        }


        private void cancelDownload(int index)
        {
            int cancelButton = publicationItemsList.GetChildren()[index].GetChildren().Count - 1;
            Gesture.Tap(publicationItemsList.GetChildren()[index].GetChildren()[cancelButton]);
            Gesture.Tap(confirmButton);

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
                if (verifyDownload(i))
                {
                    downloadIndex = i;
                    break;
                }
            }
            return downloadIndex;
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
