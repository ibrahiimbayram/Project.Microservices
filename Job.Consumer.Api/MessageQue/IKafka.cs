namespace Job.Consumer.Api.MessageQue
{
    public interface IKafka
    {
        void IncomingOrder();
        void CanceledOrder();
        void UpdatedOrder();
    }
}
