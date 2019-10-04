using MCC_Neo.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Models
{
    public class Pessoa : PropertyValidateModel
    {
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
    }
}
