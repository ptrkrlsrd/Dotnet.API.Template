namespace Template.Infrastructure.Repositories

public interface IPongRepository
{
    string Get();
}

public class PongRepository : IPongRepository
{
    public string Get() => "Pong";
}
