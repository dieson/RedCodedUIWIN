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
    public class ContactInfo
    {
        private static UIMap map = new UIMap();
        public XamlButton settingButton = map.UILexisNexisRedWindow.UISettingsButton;
        public XamlListItem itemHelp = map.UILexisNexisRedWindow.UISettingMenuList.UIItemHelp;
        public XamlHyperlink contactUs = map.UILexisNexisRedWindow.UIContactUsHyperlink;
        public XamlText elPhone = map.UILexisNexisRedWindow.UIItem1800999906Text;
        public XamlText elInternational = map.UILexisNexisRedWindow.UIItem61294222174Text;
        public XamlText elEmail = map.UILexisNexisRedWindow.UIEmailText;
        public XamlText elFax = map.UILexisNexisRedWindow.UIItem0294222405Text;


        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private String phone = "";
        private String international = "";
        private String fax = "";
        private String contactEmail = "";

        [TestMethod]
        public void CodedUITestMethod1()
        {
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(5000);
            goInfo();
            getInfo();
            verifyInofMatch(phone, "1800 999 906");
            verifyInofMatch(international, "+61 2 9422 2174");
            verifyInofMatch(fax, "02 9422 2405");
    //        verifyInofMatch(contactEmail, "techsupport@Lexis.com.au");
            keyLaunch.closeApp();
        }

        private void goInfo()
        {
            Gesture.Tap(settingButton);
            Playback.Wait(2000);
            Gesture.Tap(itemHelp);
            Playback.Wait(2000);
            Gesture.Tap(contactUs);
        }

        private void getInfo(){
            phone = elPhone.DisplayText;
            international = elInternational.DisplayText;
            contactEmail = elEmail.DisplayText;
            fax = elFax.DisplayText;
        }



        private void verifyInofMatch(String ot, String ct)
        {
            if ( !ot.Trim().Equals(ct.Trim()))
            {
                Assert.Fail("Contact information '" + ot + "' is not correct");
            }

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
