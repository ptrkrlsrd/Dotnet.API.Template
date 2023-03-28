namespace Template.Infrastructure.Repositories;

public interface IPongRepository
{
    PongEntity Get();
}

public class PongRepository : IPongRepository
{
    public PongEntity Get() => new PongEntity("Pong");
}
