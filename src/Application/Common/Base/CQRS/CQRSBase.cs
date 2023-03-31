using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Base.CQRS
{
    public class CQRSBase
    {
        protected ILogger Logger;

        public CQRSBase(ILogger logger)
        {
            Logger = logger;
        }
    }
}
