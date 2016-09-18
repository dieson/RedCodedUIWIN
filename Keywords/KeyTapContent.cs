using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Test.Model;

namespace Test.Keywords
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class KeyTapContent
    {
        private static UIMap map = new UIMap();
        public UITOCListList tocList = map.UILexisNexisRedWindow.UITOCListList;

        private int contentCount = 0;
        String currentTitle = "";
        public Chapter enterChapter(int beginIndex)
        {
            Chapter chapter = new Chapter();
            
            contentCount = getContentCount();
            currentTitle = getContentTitle(beginIndex);
            tapContent(beginIndex);
            Playback.Wait(2000);
            if (contentCount != getContentCount())
            {
                int currentIndex = getUIControlIndex(getUIControl(currentTitle));
                enterChapter(currentIndex + 1);
            }
            chapter.setTitle(currentTitle);
            chapter.setTitleIndex(beginIndex);
            chapter.setContent("");
            return chapter;
        }

        public void goToc()
        {
            Gesture.Tap(tocList.GetChildren()[0]);
        }

        public Chapter getChapter(String title)
        {
            Chapter chapter = new Chapter();
            int cIndex = getUIControlIndex(getUIControl(title));
            String cTitle = getContentTitle(cIndex);
            chapter.setTitle(cTitle);
            chapter.setTitleIndex(cIndex);
            return chapter;
        }


        private void tapContent(int index)
        {
            Gesture.Tap(tocList.GetChildren()[index]);
        }

        private String getContentTitle(int index)
        {
            String title = "";
            title = tocList.GetChildren()[index].GetChildren()[0].Name;
            return title;
        }

        private int getContentCount()
        {
            int count = 0;
            count = tocList.GetChildren().Count;
            return count;
        }

        private UITestControl getUIControl(String title)
        {
            UITestControl control = null;
            int contentCount = tocList.GetChildren().Count;
            for (int i = 0; i < contentCount; i++)
            {
                if (verifyTitleMatch(getContentTitle(i), title))
                {
                    control = tocList.GetChildren()[i];
                }
            }
            return control;
        }

        private int getUIControlIndex(UITestControl control)
        {
            int index = 0;
            int contentCount = tocList.GetChildren().Count;
            for (int i = 0; i < contentCount; i++)
            {
                String oTitle = tocList.GetChildren()[i].GetChildren()[0].Name;
                if (oTitle.Trim().Equals(control.GetChildren()[0].Name.Trim()))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }


        private bool verifyTitleMatch(String ot, String ct)
        {
            if (ot.Trim().Equals(ct.Trim()))
            {
                return true;
            }
            else
            {
                return false;
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
