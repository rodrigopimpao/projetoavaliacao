using Anima.Projeto.Application.Common;
using Anima.Projeto.Application.Requests;
using Anima.Projeto.Application.Responses;
using Anima.Projeto.Domain.Core.Entities;
using Anima.Projeto.Domain.Shared.Interfaces;
using System;
using System.Linq;

namespace Anima.Projeto.Application.Commands
{
    // os comandos são instruçoes que alteram o estado do servidor
    public class AddNotaCommand : Command<AddNotaRequest, AddNotaResponse>
    {
        public AddNotaCommand(IWriteRepository repository) : base(repository)
        {
        }

        //todo metodo handle tem que caracterizar um transação
        protected override AddNotaResponse Changes(AddNotaRequest request)
        {


            Avaliacao avaliacao = _repository.AsQueryableString<Avaliacao>("Questaos", "Questaos.Alternativas").SingleOrDefault(x => x.Id == request.AvaliacaoId);
            var valor = 0.0;
            if (avaliacao != null)
            {
                var totalQuestoes = 0;
                var totalCorretas = 0;
                foreach (Questao questao in avaliacao.Questaos)
                {

                    var respostaEstudante = _repository.AsQueryable<RespostaEstudante>().FirstOrDefault(x => x.UsuarioId == request.EstudanteId && x.QuestaoId == questao.Id);


                    if (respostaEstudante != null)
                    {
                        foreach (Alternativa alternativa in questao.Alternativas)
                        {
                            if (alternativa.Correta == true)
                            {
                                if (alternativa.Id == respostaEstudante.AlternativaId)
                                {
                                    totalCorretas++;
                                }
                            }
                        }
                    }

                    
                    totalQuestoes++;
                }


                valor = (totalCorretas*10.0)/totalQuestoes;



            }



            var nota = new Nota(valor, request.EstudanteId, request.AvaliacaoId);

            _repository.Add(nota);

            return new AddNotaResponse
            {
                Id = nota.Id,
                CreatedAt = nota.CreatedAt
            };
        }
    }
}
