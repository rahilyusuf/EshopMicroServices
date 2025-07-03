
namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        //public NotFoundException(string message, string details) : base(message)
        //{
        //    Details = details;
        //}
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
        //public string? Details { get; }
    }
}