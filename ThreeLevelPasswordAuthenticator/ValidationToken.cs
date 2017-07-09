using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kdt.ThreeLevelPasswordAuthenticator.Authentication
{
    public class ValidationToken
    {
        public Token Result { get; set; }
        public ValidationType ValidationType { get; set; }
    }
}