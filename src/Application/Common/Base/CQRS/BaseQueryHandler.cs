using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Base.CQRS
{
    public class BaseQueryHandler : CQRSBase
    {
        protected BaseQueryHandler(ILogger logger) : base(logger)
        {

        }
    }
}
