using System;
using System.IO;
using System.Collections;
using System.Text;

namespace OP2
{
    class Program
    {
        static void Main(string[] args)
        {
            int number;
            var enc1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251);
            StreamWriter newfile = new StreamWriter("C:\\Users\\lsere\\OneDrive\\Рабочий стол\\laba\\reiting.csv", false, enc1251);
            StreamReader firstfile = new StreamReader("C:\\Users\\lsere\\OneDrive\\Рабочий стол\\laba\\students.csv", enc1251);
            StreamReader firstfile1 = new StreamReader("C:\\Users\\lsere\\OneDrive\\Рабочий стол\\laba\\students.csv", enc1251);
            number = Convert.ToInt32(firstfile.ReadLine());
            int number1 = Convert.ToInt32(firstfile1.ReadLine());
            string[] last_name = new string[number];
            double[] average = new double[100];
            int numberofmarks = CheckForAllMarks(firstfile, number, newfile);
            int numberofstudents = 0;
            ReadFile(numberofmarks, numberofstudents, number, average, last_name, firstfile1, newfile);
            
            
            Output(last_name, average, numberofstudents, newfile);
            newfile.Close();
        }
        static int CheckForAllMarks(StreamReader firstfile, int number, StreamWriter newfile)
        {
            int n = 0;
            string[] last_name = new string[number];
            double[] averege = new double[39];
            int j = 0;
            for (int i = 0; i < number; i++)
            {
                string Line = firstfile.ReadLine();
                string[] name = Line.Split(",");
                j = 0;
                for (int a = 0; a < name.Length; a++)
                {

                    if (name[a] != "FALSE" && name[a] != "TRUE" && a > 0)
                    {
                        j++;
                    }
                }
                if (j > n)
                {
                    n = j;
                }
            }
            return n;
        }
        static void ReadFile(int numberofmarks, int numberofstudents, int number, double[] average, string[] last_name, StreamReader firstfile, StreamWriter newfile)
        {
            
            for (int i = 0; i < number; i++)
            {
                string Line = firstfile.ReadLine();
                string[] name = Line.Split(",");
                if (FindAverage(name, numberofmarks) != 0) //проверка на наличие всех оценок
                {
                    average[numberofstudents] = FindAverage(name, numberofmarks); //запись среднего балла в массив
                    last_name[numberofstudents] = name[0];
                    numberofstudents++;
                }
            }
            Sort(average, last_name, numberofstudents, newfile);
        }
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        static double FindAverage(string[] name, int nm)
        {
            double average = 0;
            int n = 0;
            for (int i = 0; i < name.Length - 1; i++)
            {

                if (name[i] != "True" && name[i] != "FALSE" && i > 0)
                {
                    average = Convert.ToDouble(name[i]) + average;
                    n++;
                }
            }
            if(n == nm)
            {
                average = average / nm;
                return average;
            }
            else
                return 0;
        }
        static void Sort(double[] average, string[] last_name, int numberofstudents, StreamWriter newfile)
        {
            for (int i = 0; i < numberofstudents; i++)
            {
                for (int j = 0; j < numberofstudents; j++)
                {

                    if ((average[i] > average[j]))
                    {
                        Swap<double>(ref average[i], ref average[j]);
                        Swap<string>(ref last_name[i], ref last_name[j]);

                    }
                }
            }
            Output(last_name, average, numberofstudents, newfile);
        }
        static void Output(string[] last_name, double[] average, int numberofstudents, StreamWriter newfile)
        {
            for (int i = 0; i < numberofstudents * 0.4; i++)
            {
                newfile.WriteLine(last_name[i] + "  " + average[i]);
            }
        }
        
    }
}
