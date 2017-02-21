using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.JT808
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public MessageTypeAttribute()
        {
            NoBody = false;
        }

        public ushort ID { get; set; }

        public bool NoBody { get; set; }
    }
}
