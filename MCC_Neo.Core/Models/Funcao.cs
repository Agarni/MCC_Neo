using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("Funcoes")]
    public class Funcao
    {
        [Key]
        public int FuncaoId { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Descricao { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Descritivo { get; set; }
        public bool FuncaoAtiva { get; set; }

        public virtual ICollection<Responsavel> Responsaveis { get; set; }
    }
}
