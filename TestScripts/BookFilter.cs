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
    public class BookFilter
    {
        private static UIMap map = new UIMap();
        public XamlRadioButton all = map.UILexisNexisRedWindow.UIItemPane.UIAllRadioAll;
        public XamlRadioButton loan = map.UILexisNexisRedWindow.UIItemPane.UILoanRadioButton;
        public XamlRadioButton subscription = map.UILexisNexisRedWindow.UIItemPane.UISubscriptionRadioButton;
        public UIPublicationItemsList itemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        private ArrayList allBookList = new ArrayList();
        private ArrayList loanBookList = new ArrayList();
        private ArrayList subscriptionBookList = new ArrayList();
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
            setAll();
            setLoan();
            setSubscription();
            verifyList();
            keyLaunch.closeApp();
        }

        private void verifyList()
        {
            loanBookList.AddRange(subscriptionBookList);
            if (!verifyList(allBookList, loanBookList))
            {
                Assert.Fail("The booklist is not correct, please check the filter");
            }
        }

        private void setAll()
        {
            Gesture.Tap(all);
            setBookList(allBookList);
        }

        private void setLoan()
        {
            Gesture.Tap(loan);
            setBookList(loanBookList);
        }

        private void setSubscription()
        {
            Gesture.Tap(subscription);
            setBookList(subscriptionBookList);
        }

        private void setBookList(ArrayList list)
        {
            int count = itemsList.GetChildren().Count;
            for (int i = 0; i < count; i++)
            {
                list.Add(getBookTitle(i));
            }
        }

        private String getBookTitle(int index)
        {
            String title = "";
            title = itemsList.GetChildren()[index].GetChildren()[0].Name;
            return title;
            
        }

        private bool verifyList(ArrayList l1, ArrayList l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < l1.Count; i++)
                {
                    if (!isExist(l1[i].ToString(), l2))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private bool isExist(String s, ArrayList l)
        {
            bool isExist = false;
            for (int i = 0; i < l.Count; i++)
            {
                if (s.Trim().Equals(l[i].ToString().Trim()))
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
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
