using MCC_Neo.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Models
{
    [Table("Responsaveis")]
    public class Responsavel : PropertyValidateModel
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
        [MaxLength(150, ErrorMessage = "E-mail não pode ser superior a 150 caracteres")]
        [Column(TypeName = "varchar(150)")]
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Nºs telefone não podem ser superiores a 50 caracteres")]
        [Column(TypeName = "varchar(50)")]
        public string Telefones { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Profissao { get; set; }
        public NivelEscolaridade Escolaridade { get; set; }

        // Responsável
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string NomeCracha { get; set; }
        public TipoFuncao TipoFuncao { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Funcao { get; set; }
        public Equipe Equipe { get; set; }
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string EquipeFuncional { get; set; }
        [Description("Cursilho que fez")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string NomeCUR { get; set; }
        public int AnoCUR { get; set; }
        public int MensagemId { get; set; }
        public int GrupoId { get; set; }
        public int CidadeId { get; set; }
        public bool EhResponsavelGrupo { get; set; }

        // Relacionamentos
        public virtual Cursilho Cursilho { get; set; }
        public virtual Mensagem Mensagem { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual Cidade Cidade { get; set; }

        public Responsavel()
        {
            TipoFuncao = TipoFuncao.Responsavel;
            EstadoCivil = EstadoCivil.Solteiro;
            Equipe = Equipe.Vanguarda;
        }
    }
}
