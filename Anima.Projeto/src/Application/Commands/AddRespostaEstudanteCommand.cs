using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddRespostaEstudanteCommand : Command<AddRespostaEstudanteRequest, AddRespostaEstudanteResponse>
    {
        public AddRespostaEstudanteCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddRespostaEstudanteResponse Changes(AddRespostaEstudanteRequest request)
        {

            var resposta = new RespostaEstudante(request.QuestaoId, request.UsuarioId, request.AlternativaId);
             
            _repository.Add(resposta);

            return new AddRespostaEstudanteResponse
            {
                Id = resposta.Id,
                CreatedAt = resposta.CreatedAt
            };
        }
    }
}
