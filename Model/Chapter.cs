using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Chapter
    {
        private String book;
        private String title;
        private int titleIndex;
        private String content;

        public String getBook()
        {
            return this.book;
        }

        public void setBook(String title)
        {
            this.book = title;
        }

        public String getTitle(){
            return this.title;
        }
        public void setTitle(String t)
        {
            this.title = t;
        }

        public int getTitleIndex()
        {
            return this.titleIndex;
        }
        public void setTitleIndex(int i)
        {
            this.titleIndex = i;
        }

        public String getContent()
        {
            return this.content;
        }

        public void setContent(String c)
        {
            this.content = c;
        }


    }
}
