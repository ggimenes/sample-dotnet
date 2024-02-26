using System;

namespace SampleDotnet.DDD.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
