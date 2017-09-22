using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Logging;
using Voodoo;

namespace Core.Infrastructure.Logging
{
    public interface IErrorFactory
    {
        Error GetError();
    }
}