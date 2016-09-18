using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{

  
    class History
    {
        private String book;
        private String chapter;
        private String time;

        public void setBook(String title)
        {
            this.book = title;
        }

        public String getBook()
        {
            return this.book;
        }

        public void setChapter(String ch)
        {
            this.chapter = ch;
        }

        public String getChapter()
        {
            return this.chapter;
        }

        public void setTime(String time)
        {
            this.time = time;
        }
    }
}
