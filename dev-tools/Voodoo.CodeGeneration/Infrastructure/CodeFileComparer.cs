using System.Collections.Generic;
using Voodoo.CodeGeneration.Models;

namespace Voodoo.CodeGeneration.Infrastructure
{
    public class CodeFileComparer : IEqualityComparer<CodeFile>
    {
        public bool Equals(CodeFile x, CodeFile y)
        {
            return x.FileName == y.FileName;
        }

        public int GetHashCode(CodeFile obj)
        {
            return obj.FileName.GetHashCode();
        }
    }
}