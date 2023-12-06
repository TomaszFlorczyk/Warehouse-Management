namespace WarehouseMenagementAPI.Exceptions
{
    public class InvalidWarehouseDataException : Exception
    {
        public InvalidWarehouseDataException(string? message) : base(message)
        {
        }
    }
}
