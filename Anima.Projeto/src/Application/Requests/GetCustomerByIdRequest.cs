using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class GetCustomerByIdRequest : Request
    {
        public Guid Id { get; set; }
    }
}
