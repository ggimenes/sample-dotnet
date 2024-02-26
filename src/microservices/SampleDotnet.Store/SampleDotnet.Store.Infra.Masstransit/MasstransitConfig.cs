namespace SampleDotnet.Store.Infra.Masstransit
{
    public class MasstransitConfig
    {
        public OrderStateMachineConfig OrderStateMachine { get; set; }

        public class OrderStateMachineConfig
        {
            public int PrefetchCount { get; set; }
            public int PartitionCount { get; set; }
            public int ConcurrentMessageLimit { get; set; }
        }
    }

}
