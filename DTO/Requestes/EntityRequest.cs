using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requestes
{
    public class EntityRequest<TEntity> : BaseRequest
    {
        public TEntity Entity { get; set; }
    }
}
