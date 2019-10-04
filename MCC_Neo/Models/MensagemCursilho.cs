using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Models
{
    public class MensagemCursilho
    {
        public int CursilhoId { get; set; }
        public int MensagemId { get; set; }

        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }
        public virtual Mensagem Mensagem { get; set; }
    }
}
