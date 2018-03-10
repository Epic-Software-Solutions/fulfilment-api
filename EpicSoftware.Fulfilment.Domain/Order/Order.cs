using System;
using System.ComponentModel.DataAnnotations;

namespace EpicSoftware.Fulfilment.Domain.Order
{
    public class Order: Entity
    {
        [Required]
        public string ProductJson { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public long Quantity { get; set; }
        [Required]
        public int WorkflowId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderCompleteDate { get; set; }
        public bool OrderComplete { get; set; }
    }
}