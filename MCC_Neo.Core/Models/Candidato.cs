using MCC_Neo.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC_Neo.Core.Models
{
    [Table("Candidatos")]
    public class Candidato : PropertyValidateModel
    {
        // Classe Pessoa
        public int Id { get; set; }
        public int CursilhoId { get; set; }
        [Required(ErrorMessage = "O nome deve ser preenchido")]
        [MaxLength(100, ErrorMessage = "Nome não pode ser superior a 100 caracteres")]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataNascimento { get; set; }
        public Sexo SexoPessoa { get; set; }
        [MaxLength(300, ErrorMessage = "Endereço não pode ser superior a 300 caracteres")]
        [Column(TypeName = "varchar(300)")]
        public string Endereco { get; set; }
        [MaxLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string Email { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Telefones { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Profissao { get; set; }
        public NivelEscolaridade Escolaridade { get; set; }
        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }

        // Candidato
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string NomeCracha { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Paroquia { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string RestricaoAlimentar { get; set; }
        public int ConheceMCC { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string NomePadrinho { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CURPadrinho { get; set; }
        public bool PadrinhoParticipaEV { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PadrinhoTelefones { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PadrinhoParoquia { get; set; }
        public int GrupoId { get; set; }

        public int CandidatoCidadeId { get; set; }
        public int PadrinhoCidadeId { get; set; }

        // Relacionamentos
        [Required(ErrorMessage = "Informe a cidade do candidato")]
        public virtual Cidade CandidatoCidade { get; set; }
        [Required(ErrorMessage = "Informe a cidade do padrinho")]
        public virtual Cidade PadrinhoCidade { get; set; }
        public virtual Grupo Grupo { get; set; }

        // Campos calculados
        [NotMapped]
        public int? Idade
        {
            get
            {
                if (DataNascimento != null)
                {
                    var hoje = DateTime.Today;
                    var idade = (hoje.Year - DataNascimento.Value.Year);
                    if (DataNascimento.Value > hoje)
                        idade--;

                    return idade;
                }

                return null;
            }
        }

        [NotMapped]
        public string Observacoes { get; set; }
    }
}
