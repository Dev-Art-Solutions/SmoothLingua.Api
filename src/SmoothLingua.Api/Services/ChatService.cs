using SmoothLingua.Abstractions;
using SmoothLingua.Api.Exceptions;
using SmoothLingua.Api.Models;

namespace SmoothLingua.Api.Services
{
    public class ChatService : IChatService
    {
        public Dictionary<int, IAgent> agents = new Dictionary<int, IAgent>();

        public Response Handle(int id, Chat input)
        {
            if (!agents.TryGetValue(id, out var agent))
            {
                throw new MissingAgentException(id);
            }

            return agent.Handle(input.ConversationId, input.Input);
        }

        public async Task Train(int id, Domain domain, CancellationToken cancellationToken)
        {
            DomainValidator.Validate(domain);

            MemoryStream ms = new MemoryStream();
            var trainer = new Trainer();
            await trainer.Train(domain, ms, cancellationToken);

            agents[id] = await AgentLoader.Load(new MemoryStream(ms.GetBuffer()), cancellationToken);
        }
    }
}
