using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Models
{
    [Table("Mensagens")]
    public class Mensagem
    {
        [Key]
        public int MensagemId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string NomeMensagem { get; set; }
        public bool EhMeditacao { get; set; }
        public bool MensagemAtiva { get; set; }

        public virtual ICollection<MensagemCursilho> Cursilhos { get; set; }
        public virtual ICollection<Responsavel> Responsaveis { get; set; }
    }
}
