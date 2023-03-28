using Riok.Mapperly.Abstractions;
using Template.Infrastructure.Repositories;

namespace Template.API.Application.Models;


[Mapper]
public partial class PingMapper
{
    public partial Ping Map(PongEntity pingEntity);
}
