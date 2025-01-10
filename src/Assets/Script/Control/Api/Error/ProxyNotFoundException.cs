namespace QS.Api.Control.Error
{
    public class ProxyNotFoundException : System.Exception
    {
        public ProxyNotFoundException(string uuid, string type)
            : base($"DataSource does not found for {type} with {uuid}!")
        { }
    }
}