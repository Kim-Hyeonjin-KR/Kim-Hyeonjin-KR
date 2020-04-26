using System;

namespace Study
{
    class EmptyCheck
    {
        public EmptyCheck(string _tempstr)
        {
            tempstr = _tempstr;
        }
        string tempstr;

        public void getStr(string _tempstr)
        {
            tempstr = _tempstr;
        }
        public bool isempty()
        {
            return (tempstr == null || tempstr == "");
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            string tempstr = null;
            EmptyCheck emptycheck = new EmptyCheck(tempstr);
            bool isflag = emptycheck.isempty();
            Console.WriteLine("{0}", isflag);

            string tempstr2 = "";
            emptycheck.getStr(tempstr2);
            isflag = emptycheck.isempty();
            Console.WriteLine("{0}", isflag);

        }
    }
}