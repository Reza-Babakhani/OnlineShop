using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
  public  class OperationResult
    {
        public OperationResult()
        {
            Errors = new List<OperationError>();
        }
        public bool IsSuccess { get; set; }

        public IList<OperationError> Errors { get; set; }
    }
}
