using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public class DataHolderImpl : IDataHolder<double>
    {
        private readonly Stack<double> s;

        public DataHolderImpl()
        {
            s = new Stack<double>();
        }
        public Stack<double> GetData()
        {
            return s;
        }

        public int GetSize()
        {
            return s.Count;
        }

        public void InsertElement(double elemnt)
        {
            s.Push(elemnt);
        }

        public double RemoveElement()
        {
            return s.Pop();
        }
    }
}
