using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class TemplateNotFoundException : TrueReportException 
    {
        public TemplateNotFoundException(string message)
            : base(message)
        {
        }
    }
}
