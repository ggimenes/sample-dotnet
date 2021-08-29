﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Abstractions
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEnumerable<IDomainEvent> domainEvents);
    }
}