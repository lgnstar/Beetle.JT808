using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808.Messages
{
    [MessageType(ID = 0x0102)]
    public class ClientSignature
    {
        [GBKHandler]
        public string Signature { get; set; }
    }
}
