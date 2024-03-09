using SmoothLingua.Abstractions;
using SmoothLingua.Api.Models;

namespace SmoothLingua.Api.Services
{
    public interface IChatService
    {
        Task Train(int id, Domain domain, CancellationToken cancellationToken);

        Response Handle(int id, Chat input);
    }

}
