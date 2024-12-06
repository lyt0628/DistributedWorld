





namespace QS.Api.Control.Exception
{
    public class DataSourceNotFoundException : System.Exception
    {
        public DataSourceNotFoundException(string uuid, string type) 
            : base($"DataSource does not found for {type} with {uuid}!")
        { }
    }
}