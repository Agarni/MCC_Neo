using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Models
{
    [Table("Parametrizacoes")]
    public class Parametrizacao
    {
        public int ParametrizacaoId { get; set; }
        public int CursilhoId { get; set; } // Cursilho ativo
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CoordenadorGED { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ViceCoordenadorGED { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string DiretorEspiritual { get; set; }

        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }
    }
}
