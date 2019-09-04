using System.Collections.Generic;
using System.Linq;

namespace PoC.ES.Api.Results
{
    public class ResultOfCommand
    {
        public bool IsSuccess => !IsInvalid;
        public bool IsInvalid => ErrorMessagens.Any();

        public List<(string Code, string Message)> ErrorMessagens { get; private set; }
            = new List<(string Code, string Message)>();

        public object Data { get; set; }

        public ResultOfCommand AddErrorMessage((string Code, string Message) message)
        {
            ErrorMessagens.Add(message);
            return this;
        }

        public ResultOfCommand AddErrorMessages(IEnumerable<(string Code, string Message)> messages)
        {
            ErrorMessagens.AddRange(messages);
            return this;
        }

        public static ResultOfCommand Create() => new ResultOfCommand();
    }
}
