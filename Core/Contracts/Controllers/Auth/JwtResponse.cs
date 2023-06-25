using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Core.Contracts.Controllers.Auth
{
    public sealed record JwtResponse()
    {
        [DataMember(Name = "jwt")]
        public required string Jwt { get; init; }
    }
}
