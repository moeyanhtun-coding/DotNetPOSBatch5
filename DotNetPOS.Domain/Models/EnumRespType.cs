using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public enum EnumRespType
    {
        None,
        Pending,
        Success,
        ValidationError,
        SystemError,
        NotFound
    }
}
