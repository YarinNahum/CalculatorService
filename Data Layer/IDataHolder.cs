using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public interface IDataHolder<T>
    {
        Stack<T> GetData();
        void InsertElement(T element);
        T RemoveElement();
        int GetSize();

        void SetData(Stack<T> data);

    }
}
