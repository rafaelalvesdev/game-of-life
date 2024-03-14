using System.Net;
using System.Text.Json.Serialization;

namespace GameOfLife.Common.ServiceModels
{
    public class ServiceResult
    {
        public bool IsValid => !Errors.Any();

        public List<Error> Errors { get; set; } = new List<Error>();

        [JsonIgnore]
        public int? StatusCode { get; set; }

        public Exception Exception { get; set; }

        public ServiceResult() { }

        public ServiceResult(IEnumerable<Error> errors) : this() => (Errors) = (errors.ToList());

        public ServiceResult(Error error) : this() => (Errors) = (new List<Error>() { error });

        public ServiceResult(string errorMessage) : this() => (Errors) = (new List<Error>() { new Error(errorMessage) });

        public ServiceResult(int statusCode, string errorMessage) : this() => (StatusCode, Errors) = (statusCode, new List<Error>() { new Error(errorMessage) });
    }

    public class ServiceResult<TData> : ServiceResult
    {
        public TData Data { get; set; }

        public ServiceResult() { }

        public ServiceResult(TData data) => (Data) = (data);

        public ServiceResult(TData data, IEnumerable<Error> errors) : this(data) => (Errors) = (errors.ToList());

        public ServiceResult(TData data, Error error) : this(data) => (Errors) = (new List<Error>() { error });

        public ServiceResult(TData data, int statusCode, string errorMessage) : this(data, new Error(errorMessage)) => (StatusCode) = (statusCode);
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public Error() { }

        public Error(string errorMessage) : this() => (ErrorMessage) = errorMessage;
    }
}
