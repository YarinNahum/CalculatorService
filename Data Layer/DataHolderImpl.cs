using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public class DataHolderImpl : IDataHolder<double>
    {
        private Stack<double> s;

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

        public void InsertElement(double element)
        {
            s.Push(element);
        }

        public double RemoveElement()
        {
            return s.Pop();
        }

        public void SetData(Stack<double> data)
        {
            s = new Stack<double>(data);
        }
    }
}
