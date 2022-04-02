using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Common
{
    public abstract class Query<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response
    {

        protected IReadRepository _repository;

        public Query(IReadRepository repository)
        {
            _repository = repository;
        }
        public abstract TResponse Handle(TRequest request);
    }
}
