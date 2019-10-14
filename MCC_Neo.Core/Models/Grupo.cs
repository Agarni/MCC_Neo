using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("Grupos")]
    public class Grupo
    {
        public int GrupoId { get; set; }
        [Required(ErrorMessage = "O nome deve ser preenchido")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; }
        public int CursilhoId { get; set; }

        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }
        public virtual ICollection<Responsavel> Responsaveis { get; set; }
        public virtual ICollection<Candidato> Candidatos { get; set; }
    }
}
