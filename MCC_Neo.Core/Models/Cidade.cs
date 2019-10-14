using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("Cidades")]
    public class Cidade
    {
        public int CidadeId { get; set; }
        [MaxLength(70)]
        [Column(TypeName = "varchar(70)")]
        public string NomeCidade { get; set; }
        public int CidadeAtiva { get; set; }

        public virtual ICollection<Cursilho> Cursilhos { get; set; }
        //[InverseProperty("CandidatoCidade")]
        public virtual ICollection<Candidato> CidadeCandidatos { get; set; }
        //[InverseProperty("PadrinhoCidade")]
        public virtual ICollection<Candidato> CidadePadrinhos { get; set; }
        public virtual ICollection<Responsavel> CidadeResponsaveis { get; set; }
    }
}
