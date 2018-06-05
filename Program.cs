using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            //原始數列
            List<double> _original_list = new List<double>();
            _original_list.Add(1682);
            _original_list.Add(1508);
            _original_list.Add(1595);
            _original_list.Add(1067);

            Console.WriteLine(new GreyTheory().GM11(_original_list));

            Console.Read();

        }
    }
}
