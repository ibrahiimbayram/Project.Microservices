using Services.Order.Models;

namespace Services.Order.MessageQue
{
    public interface IKafka
    {
        void IncomingOrder(ProductModel productModel);
        void CanceledOrder(ProductModel productModel);
        void UpdatedOrder(ProductModel productModel);
        void UpdatedOrderTransactions(int id, ProductModel productModel);
    }
}
