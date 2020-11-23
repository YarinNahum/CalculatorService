using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    /// <summary>
    /// The interface for the data that the calculator uses.
    /// </summary>
    /// <typeparam name="T">A type for the data</typeparam>
    public interface IDataHolder<T>
    {
        /// <summary>
        /// Returns the stack.
        /// </summary>
        /// <returns>A stack of T</returns>
        Stack<T> GetData();
        
        /// <summary>
        /// Insert an element in the stack
        /// </summary>
        /// <param name="element"></param>
        void InsertElement(T element);
        
        /// <summary>
        /// Remove the top elemtent in the stack and returns it
        /// </summary>
        /// <returns>The top element in the stack</returns>
        T RemoveElement();
        
        /// <summary>
        /// Returns the number of elements in the stack
        /// </summary>
        /// <returns>An integer</returns>
        int GetSize();

        /// <summary>
        /// Replace the private stack of the class with the given stack.
        /// </summary>
        /// <param name="data">A stack of T</param>
        void SetData(Stack<T> data);

    }
}
