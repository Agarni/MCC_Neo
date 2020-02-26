using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Models
{
    public class CidadeCursilho
    {
        public int CursilhoId { get; set; }
        public virtual Cursilho Cursilho { get; set; }
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
