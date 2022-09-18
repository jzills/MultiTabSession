namespace Source.Session;

public interface ISessionValidator<TSessionValue>
{
    bool ValidateKey(string key);
    bool ValidateValue(TSessionValue value);
}