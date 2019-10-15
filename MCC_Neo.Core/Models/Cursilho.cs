using MCC_Neo.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("Cursilhos")]
    public class Cursilho : PropertyValidateModel
    {
        public int CursilhoId { get; set; }
        [MaxLength(200)]
        [Required( ErrorMessage = "Nome do cursilho deve ser informado")]
        [Column(TypeName = "varchar(200)")]
        public string DescricaoCursilho { get; set; }
        [MaxLength(300)]
        [Column(TypeName = "varchar(300)")]
        public string TemaCursilho { get; set; }
        [MaxLength(300)]
        [Column(TypeName = "varchar(300)")]
        public string LemaCursilho { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicioCUR { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFimCUR { get; set; }
        public int CidadeId { get; set; }

        // Relacionamentos
        public virtual Cidade Cidade { get; set; }
        public ICollection<CidadeCursilho> CidadesCursilho { get; set; }
        public virtual ICollection<MensagemCursilho> Mensagens { get; set; }
        public virtual ICollection<Responsavel> Responsaveis { get; set; }
        public virtual ICollection<Candidato> Candidatos { get; set; }

    }
}
