﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure.Cryptography
{
    public interface ICryptographer
    {
        string Encrypt(string input);
    }
}
