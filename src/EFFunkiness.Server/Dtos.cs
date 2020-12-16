using System;

namespace EFFunkiness.Server
{
    public record ClientDto(Guid ClientId, string Name, UserDto CreatedByUser);

    public record UserDto(Guid UserId, string Name);
}
