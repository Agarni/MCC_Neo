using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Models
{
    public class CidadeCursilho
    {
        public int CursilhoId { get; set; }
        public Cursilho Cursilho { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
    }
}
