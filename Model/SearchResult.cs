using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Test.Model
{
    class SearchResult
    {
        
        private String firstLevelTitle;
        private String secondLevelTitle;
        private String type;
        private UITestControl item;

        public void setItem(UITestControl con)
        {
            this.item = con;
        }

        public UITestControl getItem()
        {
            return this.item;
        }

        public void setFirstLevelTitle(String title)
        {
            this.firstLevelTitle = title;
        }

        public String getFirstLevelTitle()
        {
            return this.firstLevelTitle;
        }

        public void setSecondLevelTitle(String title)
        {
            this.secondLevelTitle = title;
        }

        public String getSecondLevelTitle()
        {
            return this.secondLevelTitle;
        }

        public void setType(String t)
        {
            this.type = t;
        }

        public String getType()
        {
            return this.type;
        }

    }
}
