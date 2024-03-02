
using SmoothLingua.Abstractions;

public interface IChatService
{
    Task Train(int id, Domain domain, CancellationToken cancellationToken);

    Response Handle(int id, Chat input);
}
