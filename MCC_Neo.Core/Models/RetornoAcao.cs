using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core
{
    public class RetornoAcao
    {
        public bool Sucesso { get; set; }
        public string MensagemRetorno { get; set; }
        public Exception ExceptionRetorno { get; set; }
    }

    public class RetornoAcao<T> : RetornoAcao
    {
        public T Result { get; set; }
    }
}
