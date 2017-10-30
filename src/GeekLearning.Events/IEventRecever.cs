namespace GeekLearning.Events
{
    using GeekLearning.Events.Model;
    interface IEventRecever<TEvent> where TEvent : EventBase
    {
    }
}
