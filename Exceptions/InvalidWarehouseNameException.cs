namespace WarehouseMenagementAPI.Exceptions
{
    public class InvalidWarehouseNameException : Exception
    {
        public InvalidWarehouseNameException(string? message) : base(message)
        {
        }
    }
}
