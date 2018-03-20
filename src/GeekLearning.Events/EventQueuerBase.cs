namespace GeekLearning.Events
{
    public abstract class EventQueuerBase
    {
        private readonly IEventQueuer queuer;

        public EventQueuerBase(string storeName, IEventFactory eventQueuerFactory)
        {
            this.queuer = eventQueuerFactory.GetQueuer(storeName);
        }

        public IEventQueuer Queuer => this.queuer;

    }
}
