using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("MensagensCursilho")]
    public class MensagemCursilho
    {
        public int CursilhoId { get; set; }
        public int MensagemId { get; set; }

        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }
        public virtual Mensagem Mensagem { get; set; }
    }
}
