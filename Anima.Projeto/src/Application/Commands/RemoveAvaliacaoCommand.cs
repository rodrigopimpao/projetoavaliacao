using System;
using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Requests
{
    public class RemoveAvaliacaoCommand : Command<RemoveAvaliacaoRequest, RemoveAvaliacaoResponse>
    {
        public RemoveAvaliacaoCommand(IWriteRepository repository) : base(repository)
        {
        }

        protected override RemoveAvaliacaoResponse Changes(RemoveAvaliacaoRequest request)
        {
            _repository.Remove<Avaliacao>(request.Id);

            return new RemoveAvaliacaoResponse();
        }
    }
}
