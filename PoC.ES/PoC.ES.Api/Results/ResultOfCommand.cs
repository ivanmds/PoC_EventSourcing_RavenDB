using System.Collections.Generic;
using System.Linq;

namespace PoC.ES.Api.Results
{
    public class ResultOfCommand
    {
        public bool IsSuccess => !ErrorMessagens.Any();
        public List<(string Code, string Message)> ErrorMessagens { get; private set; } 
            = new List<(string Code, string Message)>();

        public void AddMessage((string Code, string Message) message) => ErrorMessagens.Add(message);
    }
}
