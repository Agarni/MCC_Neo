using System;
using System.Linq;

namespace MCC_Neo.Core.Models
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class CursilhistaDTO
    {
        public string Nome { get; set; }
        public string NomeCracha { get; set; }
        public string CursilhoRealizado { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }
        public string NomeCidade { get; set; }
        public string NomeGrupo { get; set; }
        public Equipe EquipeAtuacao { get; set; }
        public string DescricaoFuncao { get; set; }
        public string EquipeFuncional { get; set; }
        public string Mensagem { get; set; }
    }
}
