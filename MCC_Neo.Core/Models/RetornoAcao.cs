using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core
{
    public struct RetornoAcao
    {
        public bool Sucesso { get; set; }
        public string MensagemRetorno { get; set; }
        public Exception ExceptionRetorno { get; set; }
    }
}
