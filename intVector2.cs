using System;
using System.Collections;
using System.Collections.Generic;

namespace Proj_2B
{
    public class vectorint
    {
        int[] m_ArrayInt = new int[2];
        int m_Length = 0;

        //프로퍼티 형식으로 만든 count 메서드. 단순히 배열의 길이를 가져오는 역할이므로 set은 구현하지 않는다.
        public int count    
        {
            get
            {
                return m_Length;
            }
        }
        public string GetString()
        {
            if (m_Length <= 0)
                return "null";

            string outstr = "";
            for (int i = 0; i < m_Length; i++)
            {
                outstr += string.Format($"{m_ArrayInt[i]}, ");
            }
            return outstr;
        }
        public void Push(int p_val)
        {
            if (m_Length >= m_ArrayInt.Length)
            {
                int tempsize = m_Length * 2;
                int[] tempint = new int[tempsize];

                for (int i = 0; i < m_ArrayInt.Length; i++)
                {
                    tempint[i] = m_ArrayInt[i];
                }
                m_ArrayInt = tempint;
            }
            m_ArrayInt[m_Length++] = p_val;
        }
        //insertat와 addrange메서드에서 자주 쓰는 기능을 메서드로 따로 구현.
        //Arrayint에서 p_val[]이 들어갈 자리를 만들기 위해 요소들을 이동시키는 메서드.
        protected void MovElement(int[] tempint, int m_Length, int p_index, int p_valL = 0)
        {
            for (int i = m_Length; i > p_index + 1; i--)
            {
                tempint[i+p_valL] = m_ArrayInt[i - 1];
            }
            for (int i = p_index; i >= 0; i--)
            {
                tempint[i] = m_ArrayInt[i];
            }
            m_ArrayInt = tempint;
        }
        public void insertat(int p_index, int p_val)
        {
            //예외 처리
            if((p_index >= m_ArrayInt.Length-1) || (p_index < 0))
            {
                Console.WriteLine("현재 배열 상태에서 입력 가능한 범위를 벗어난 p_index입니다.");
                return;
            }
            if(m_Length == 0)
            {
                Push(p_val);
                return;
            }
            if(m_Length == 1 && p_index == 0)
            {
                m_ArrayInt[p_index+1] = p_val;
                m_Length++;
                return;
            }

            //일반적인 경우
            if (m_Length >= m_ArrayInt.Length-1)
            {
                int tempsize = m_Length * 2;
                int[] tempint = new int[tempsize];

                if (p_index < m_Length-1)
                {
                    MovElement(tempint, m_Length, p_index);
                }
            }
            else
            {
                int[] tempint = new int[m_ArrayInt.Length];
                if (p_index < m_Length-1)
                {
                    MovElement(tempint, m_Length, p_index);
                }
            }
            m_ArrayInt[p_index+1] = p_val;
            m_Length++;
        }
        public void addrange(int p_index, params int[] p_val)
        {
            //예외 처리
            if (p_index >= m_ArrayInt.Length - 1 || p_index < 0)
            {
                Console.WriteLine("현재 배열 상태에서 입력 가능한 범위를 벗어난 p_index입니다.");
                return;
            }
            if (m_Length == 0)
            {
                int[] tempint = new int[p_val.Length * 2];
                m_ArrayInt = tempint;
                foreach (int num in p_val)
                {
                    m_ArrayInt[++p_index] = num;
                }
                //m_Length가 0일 때, addrange로 [1]번째 배열부터 요소를 추가할 경우.
                //[0]번째 요소도 자동적으로 길이로 취급해야 하므로 아래에서 p_val.Length +1을 한다.
                m_Length += (p_val.Length +1);
                return;
            }
            if (m_Length == 1 && p_index == 0)
            {
                int[] tempint = new int[p_val.Length * 2];
                //m_Length가 1이므로 m_ArrayInt[0]의 요소도 빼먹지 말고 가져와야 한다.
                tempint[0] = m_ArrayInt[0];
                m_ArrayInt = tempint;
                
                foreach (int num in p_val)
                {
                    m_ArrayInt[++p_index] = num;
                }
                m_Length += p_val.Length;
                return;
            }

            //일반적인 경우
            if (m_Length + p_val.Length >= m_ArrayInt.Length - 1)
            {
                int tempsize = (m_Length + p_val.Length) * 2;
                int[] tempint = new int[tempsize];

                if (p_index < m_Length - 1)
                {
                    //m_Length와 p_val.Length는 배열의 특성상 m_ArrayInt.Length보다 
                    //비교적 1 높은 위치에 있으므로 아래 메서드의 4번째 매개변수에서 1을 빼준다.
                    MovElement(tempint, m_Length, p_index, p_val.Length-1);
                }
            }
            else
            {
                int[] tempint = new int[m_ArrayInt.Length];
                if (p_index < m_Length - 1)
                {
                    MovElement(tempint, m_Length, p_index, p_val.Length - 1);
                }
            }
            foreach (int num in p_val)
            {
                m_ArrayInt[++p_index] = num;
            }
            m_Length += p_val.Length;
        }
        public void RemoveAt(int p_index)
        {
            if (m_Length > 0)
            {
                for (int i = p_index; i < m_Length - 1; i++)
                {
                    m_ArrayInt[i] = m_ArrayInt[i + 1];
                }
                m_Length--;
            }
            else
                Console.WriteLine("실패 : 배열에 남아있는 요소가 없습니다.");
        }
        public void Clear()
        {
            m_ArrayInt = null;
            m_ArrayInt = new int[2];
            m_Length = 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            vectorint tempvector = new vectorint();
            string tempstr = tempvector.GetString();


            tempvector.Push(10);    //10,
            tempvector.Push(20);    //10, 20,
            tempvector.Push(30);    //10, 20, 30,
            tempvector.insertat(1, 5);  //10, 5, 20, 30,
            tempvector.addrange(1, 1, 2, 3);   //10, 20, 1, 2, 3, 5, 30,

            tempstr = tempvector.GetString();
            Console.WriteLine($"{tempstr}");    //10, 20, 1, 2, 3, 5, 30,
            Console.WriteLine($"{tempvector.count}");   //7

            tempvector.RemoveAt(1);
            tempstr = tempvector.GetString();
            Console.WriteLine($"{tempstr}");    //10, 1, 2, 3, 5, 30,
            Console.WriteLine($"{tempvector.count}");   //6

            tempvector.Clear();
            tempstr = tempvector.GetString();
            Console.WriteLine($"{tempstr}");    //null
            Console.WriteLine($"{tempvector.count}");   //0

            tempvector.Push(10);    //10,
            tempvector.addrange(0, 1, 2, 3);   //10, 20, 1, 2, 3, 5, 30,
            tempstr = tempvector.GetString();
            Console.WriteLine($"{tempstr}");    //null
            Console.WriteLine($"{tempvector.count}");   //0
        }
    }
}
