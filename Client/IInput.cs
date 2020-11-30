using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface IInput
    {
        /// <summary>
        /// Recieves an array of strings and check if each string is valid.
        /// </summary>
        /// <param name="requests">Array of strings</param>
        /// <returns></returns>

        bool CheckInput(string[] requests);
    }
}
