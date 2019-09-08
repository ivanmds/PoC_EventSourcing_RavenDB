namespace PoC.ES.Api.Results
{
    public class ResultOfCommandData<TData> : ResultOfCommand where TData : class
    {
        public TData Data { get; set; }
    }
}
