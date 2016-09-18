using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace Test
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class KeyLaunchRed
    {
        private static UIMap map = new UIMap();
        public XamlText signin = map.UILexisNexisRedWindow.UISigninwithyourLexisNText;
        public XamlButton signOutButton = map.UILexisNexisRedWindow.UISignOutButton;
        public XamlButton confirmButton = map.UILexisNexisRedWindow.UIPopupWindow.UIConfirmButton;
        public XamlButton cancelButton = map.UILexisNexisRedWindow.UICancelButton;

        private static int RetryTime = 0;
        public XamlWindow window;
        public void launchApp()
        {
            try
            {
                window = XamlWindow.Launch("LexisNexisAPAC.LexisRed_wsek3cqrhvvz2!App");
                String msg = signin.Name;
            }
            catch (Exception e)
            {
                Console.Write(e);  
            }
        }

        public void closeApp()
        {
            Gesture.Tap(signOutButton);
            Gesture.Tap(confirmButton);
            Gesture.Tap(cancelButton);
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
