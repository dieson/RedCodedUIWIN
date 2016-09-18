using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Test.Keywords;
using Test.Model;
using System.Collections.Generic;

namespace Test.TestScripts
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class Search
    {
        private static UIMap map = new UIMap();
        public XamlEdit searchTextBoxEdit = map.UILexisNexisRedWindow.UISearchTextBoxEdit;
        public XamlButton itemButton = map.UILexisNexisRedWindow.UIItemButton;
        public UITOCListList tocList = map.UILexisNexisRedWindow.UITOCListList;
        public XamlList searchList = map.UILexisNexisRedWindow.UIPopupWindow1.UISearchListList;
        public UIPublicationItemsList publicationItemsList = map.UILexisNexisRedWindow.UIPublicationItemsList;

        private int bookNum = 0;
        public KeyLogin keyLogin = new KeyLogin();
        public KeyLaunchRed keyLaunch = new KeyLaunchRed();
        private String email = Constants.EMAIL;
        private String password = Constants.PASSWORD;
        private String searchKeyword = "National";
        private KeyTapContent keyTapContent = new KeyTapContent();
        private Chapter chapter;
        private List<SearchResult> resultList = new List<SearchResult>();

        [TestMethod]
        public void search()
        {
            String currentTitle = "";
            Playback.PlaybackSettings.LoggerOverrideState = HtmlLoggerState.AllActionSnapshot;
            keyLaunch.launchApp();
            keyLogin.login(email, password);
            Playback.Wait(3000);
            choseBook(getOperactionIndex());
            chapter = keyTapContent.enterChapter(1);
            Gesture.Tap(searchTextBoxEdit);
            searchTextBoxEdit.Text = searchKeyword;
            Gesture.Tap(itemButton);
            Playback.Wait(5000);
            getResultList();
            currentTitle = getCurrentTitle();
            choseDocument();
            if (!compare(getCurrentTitle(), currentTitle))
            {
                Assert.Fail("The document search result is not correct");
            }
            String serachRs = chosePublication();
            String highlight = getCurrentTitle();
            if (!compare(serachRs, highlight))
            {
                Assert.Fail("The publication search result is not correct");
            }
            keyLaunch.closeApp();
        }

        private bool compare(String r, String d)
        {
            bool rs = false;
            double min = 1;
            double equalNum = 0;
            double equalPercent = 0;
            char[] arrayR = r.ToCharArray();
            char[] arrayD = d.ToCharArray();
            if (arrayR.Length >= arrayD.Length)
            {
                min = arrayD.Length;
            }
            else
            {
                min = arrayR.Length;
            }
            for (int i = 0; i < min; i++)
            {
                if(arrayR[i].Equals(arrayD[i])){
                    equalNum ++;
                }
            }
            equalPercent = equalNum/min;
            if (equalPercent >= 0.8)
            {
                rs = true;
            }
            return rs;
        }

        private void choseDocument()
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].getType().Trim().Equals("Document"))
                {
                    Gesture.Tap(resultList[i].getItem());
                    break;
                }
            }
        }

        private String chosePublication()
        {
            String rs = "";
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].getType().Trim().Equals("Publication"))
                {
                    Gesture.Tap(resultList[i].getItem());
                    rs = resultList[i].getFirstLevelTitle();
                    break;
                }
            }
            return rs;
        }


        private String getCurrentTitle()
        {
            String rs = "";
            int size = tocList.GetChildren().Count;
            for (int i = 0; i < size; i++)
            {
                if (tocList.GetChildren()[i].GetChildren()[1].GetProperty("Name").Equals("True"))
                {
                    rs = tocList.GetChildren()[i].GetChildren()[0].Name;
                }
            }
            return rs;
        }


        private void getResultList()
        {
            int size = searchList.GetChildren().Count;
            for (int i = 0; i < size; i++)
            {
                String fLevelTitle = searchList.GetChildren()[i].GetChildren()[0].FriendlyName;
                String sLevelTitle = searchList.GetChildren()[i].GetChildren()[1].FriendlyName;
                String type = searchList.GetChildren()[i].GetChildren()[2].FriendlyName;
                UITestControl item = searchList.GetChildren()[i];
                SearchResult rs = new SearchResult();
                rs.setFirstLevelTitle(fLevelTitle);
                rs.setSecondLevelTitle(sLevelTitle);
                rs.setItem(item);
                rs.setType(type);
                resultList.Add(rs);
            }
        }








        private String choseBook(int index)
        {
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
