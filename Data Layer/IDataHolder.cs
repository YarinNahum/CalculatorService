using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public interface IDataHolder<T>
    {
        Stack<T> GetData();
        void InsertElement(T elemnt);
        T RemoveElement();
        int GetSize();

    }
}
