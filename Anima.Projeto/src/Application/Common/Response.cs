using System;
using System.Collections.Generic;
using System.Linq;

namespace Anima.Projeto.Application.Common
{
    public abstract class Response
    {
        private List<string> _errors { get; set; } = new List<string>();
        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();
        public bool IsSuccess => !_errors.Any();
        public void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }
    }
}
