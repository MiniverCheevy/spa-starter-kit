using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fernweh.Core.Infrastructure
{
    public interface IRequestContextProvider
    {
        RequestContext RequestContext { get; }
    }
}