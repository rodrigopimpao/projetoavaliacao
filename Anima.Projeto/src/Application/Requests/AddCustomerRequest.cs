using System;
using Anima.Projeto.Application.Common;

namespace Anima.Projeto.Application.Requests
{
    public class AddCustomerRequest : Request
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
