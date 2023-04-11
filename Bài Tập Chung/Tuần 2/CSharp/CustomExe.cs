using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCoban
{
    internal class CustomExe : Exception
    {
        const string str = "new Exp";
        public CustomExe() : base(str) { }
    }
}
