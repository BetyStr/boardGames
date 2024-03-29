namespace DataAccessLayer.Entities;

public enum OrderStatus
{
    Unpaid,
    Paid,
    AwaitingShipment,
    Shipped,
    Delivered
}