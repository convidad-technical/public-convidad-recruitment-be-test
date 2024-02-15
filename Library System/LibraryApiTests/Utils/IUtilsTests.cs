using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApiTests.Utils
{
    public interface IUtilsTests
    {
        /// <summary>
        /// Generates a 13-digit ISBN
        /// </summary>
        /// <returns></returns>
        string GenerateISBN();
    }
}
