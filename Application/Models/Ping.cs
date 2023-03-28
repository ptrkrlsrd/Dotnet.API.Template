namespace Template.API.Application.Models;

public record Ping(string Message) 
{
    public Ping() : this("Ping!") 
    { 

    }
}
