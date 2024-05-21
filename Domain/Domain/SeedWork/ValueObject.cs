using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.SeedWork
{
     public abstract class ValueObject
    {
        public virtual bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
