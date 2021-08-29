using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Abstractions
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
