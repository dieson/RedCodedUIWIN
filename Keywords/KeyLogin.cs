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
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class KeyLogin
    {
        private static UIMap map = new UIMap();
        public XamlComboBox country = map.UILexisNexisRedWindow.UICountryBoxComboBox;
        public XamlEdit email = map.UILexisNexisRedWindow.UIEmailTextBoxEdit;
        public XamlEdit password = map.UILexisNexisRedWindow.UIPasswordBoxEdit;
        public XamlButton loginButton = map.UILexisNexisRedWindow.UILoginButton;
        public XamlText message = map.UILexisNexisRedWindow.UIPopupWindow.UIInvalidLoginInfoText;
        public XamlButton ok = map.UILexisNexisRedWindow.UIPopupWindow.UIOKButton;
        public XamlButton singout = map.UILexisNexisRedWindow.UISignOutButton;
        public XamlButton confirm = map.UILexisNexisRedWindow.UIPopupWindow.UIConfirmButton;
        public XamlCheckBox remember = map.UILexisRedWindow.UIRememberpasswordCheckBox;

        public void login(String userName, String password)
        {
            this.country.SelectedIndex = 0;
            this.email.Text = userName;
            this.password.Text = password;
            Gesture.Tap(loginButton);
        }

        public void loginInvalid(String userName, String password)
        {
            login(userName, password);
            String message = this.message.Name;
            System.Console.WriteLine(this.message);
            Gesture.Tap(this.ok);
        }

        public void logout()
        {
            Gesture.Tap(this.singout);
            Gesture.Tap(this.confirm);
        }

        public void rememberPasswor()
        {
            bool isRemember = this.remember.Checked;
            if (isRemember)
            {
                Gesture.Tap(this.remember);
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
