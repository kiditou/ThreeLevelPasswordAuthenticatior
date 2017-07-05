using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kdt.ThreeLevelPasswordAuthenticator.Authentication
{
    public enum Token
    {
        Matched,
        NotMatched,
        Blank,
        Existing,
        NonExisting
    }
}