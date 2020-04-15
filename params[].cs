using System;

namespace CConsoleApp1      //namespace는 public이 없음
{

    class Program
    {
        static void SUM(params int[] list)  //parmas로 배열을 만들면 가변인자로 매개변수를 받는다.
        {
            int sum = 0;

            foreach (int num in list)       //배열 list를 반복문으로 돌리는데 그 값을 num에 넣는다.
            {                               //부가적으로, for문과는 달리 여러 스레드에서 동시에 처리할 수 있는 기능이 있다.
               Console.Write(num.ToString() + " ");
               sum += num;
            }
            Console.Write($" = {sum}");

        }


        static void Main(string[] args)
        {
            Console.WriteLine("가변인자로 건낸 매개변수들의 합");
            SUM(1, 2, 3, 4, 5, 6, 7);       //parmas 배열로 만들어진 함수에 원하는 만큼 매개변수를 써서 보낼 수 있다.
        }
    }
}

