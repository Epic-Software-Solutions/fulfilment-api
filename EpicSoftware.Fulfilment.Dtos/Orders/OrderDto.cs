using System;

namespace EpicSoftware.Fulfilment.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ProductJson { get; set; }
        public string ProductId { get; set; }
        public int UserId { get; set; }
        public long Quantity { get; set; }
        public int WorkflowId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderCompleteDate { get; set; }
        public bool OrderComplete { get; set; }
    }
}