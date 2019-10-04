using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MCC_Neo
{
    public enum Equipe
    {
        Vanguarda,
        Retaguarda
    }

    public enum Sexo
    {
        Masculino,
        Feminino
    }

    public enum EstadoCivil
    {
        Solteiro,
        Casado,
        Separado,
        [Description("Viúvo")]
        Viuvo,
        [Description("2ª união")]
        SegundaUniao
    }

    public enum NivelEscolaridade
    {
        [Description("Não informado")]
        NaoInformado,
        [Description("Ensino fundamental")]
        Fundamental,
        [Description("Ensino médio")]
        Medio,
        [Description("Ensino superior")]
        Superior,
        [Description("Pós graduação")]
        PosGraduado
    }

    public enum TipoFuncao
    {
        [Description("Responsável")]
        Responsavel,
        [Description("Responsável de grupo")]
        ResponsavelGrupo,
        Coordenador,
        Base,
        [Description("Padre (Assessor Ecl.)")]
        AssessorEclesiastico,
        [Description("Ligação")]
        Ligacao,
        [Description("Secretário(a)")]
        Secretario,
        [Description("Responsável Fogão")]
        Fogao,
        [Description("Diretor Espiritual")]
        DiretorEspiritual,
        Copa,
        Auxiliar,
        [Description("Atualização")]
        Atualizacao
    }
}