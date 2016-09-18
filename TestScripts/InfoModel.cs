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
    public class InfoModel
    {
        private static UIMap map = new UIMap();
        public XamlText loanText = map.UILexisNexisRedWindow.UIItemPane.UILOANText;
        public XamlRadioButton loanRadioButton = map.UILexisNexisRedWindow.UIItemPane.UILoanRadioButton;
        public XamlRadioButton subscriptionRadioButton = map.UILexisNexisRedWindow.UIItemPane.UISubscriptionRadioButton;
        public XamlButton openButton = map.UILexisNexisRedWindow.UIItemPane.UIOpenButton;
        public XamlButton backButton = map.UILexisNexisRedWindow.UIBackButton;
        public UIItemPane itemPane = map.UILexisNexisRedWindow.UIItemPane;
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
            Playback.Wait(3000);
            tapLoan();
            String title = openInfoModel(getOperactionIndex());
            verifyType();
            verifyBookTitle(title);
            openBook();
            back();
            tapSubscription();
            title = openInfoModel(getOperactionIndex());
            verifyBookTitle(title);
            openBook();
            back();
            keyLaunch.closeApp();
        }

        private void verifyType()
        {
            if (!loanText.Name.Contains("LOAN"))
            {
                Assert.Fail("The type 'LOAN' in the information is not correct");
            }
        }

        private void tapLoan()
        {
            Gesture.Tap(loanRadioButton);
        }

        private void tapSubscription()
        {
            Gesture.Tap(subscriptionRadioButton);
        }

        private void openBook()
        {
            Gesture.Tap(openButton);
        }

        private void back()
        {
            Gesture.Tap(backButton);
        }


        private void verifyBookTitle(String title){
            String infoTitle = itemPane.GetChildren()[0].Name;
            if (!title.Trim().Equals(infoTitle.Trim()))
            {
                Assert.Fail("The book title in information model is not correct");
            }
        }


        private String openInfoModel(int index)
        {
            int infoButton = publicationItemsList.GetChildren()[index].GetChildren().Count - 1;
            String book = publicationItemsList.GetChildren()[index].GetChildren()[0].Name;
            Gesture.Tap(publicationItemsList.GetChildren()[index].GetChildren()[infoButton]);
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
