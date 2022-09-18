namespace Source.Session;

public class SessionValidator<TSessionValue> : ISessionValidator<TSessionValue> 
    where TSessionValue : SessionBase
{
    public bool ValidateKey(string key)
    {
        List<Exception> errors = new();

        if (string.IsNullOrEmpty(key))
        {
            errors.Add(new Exception("some error message."));
        }

        if (!Guid.TryParse(key, out var _))
        {
            errors.Add(new Exception("Some other error message."));
        }

        if (errors.Count == 0)
        {
            return true;
        }

        throw new AggregateException(errors);
    }

    public bool ValidateValue(TSessionValue value)
    {
        List<Exception> errors = new();

        if (value == null)
        {
            errors.Add(new Exception("Some error message."));
        }

        if (errors.Count == 0)
        {
            return true;
        }

        throw new AggregateException(errors);
    }
}