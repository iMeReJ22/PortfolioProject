namespace KanbanBackend.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"{name} with key '{key}' was not found") { }
        public NotFoundException(string name, object key, object key2)
            : base($"{name} with key '{key}, {key2}' was not found") { }
    }
}
