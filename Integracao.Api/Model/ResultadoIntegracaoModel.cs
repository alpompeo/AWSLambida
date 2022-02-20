using Amazon.DynamoDBv2.DataModel;
using Integracao.Api.Repository.Common;
using System;

namespace Integracao.Api.Model
{
    [DynamoDBTable("TBF8059_ITGR_PLAT_ELET")]
    public class ResultadoIntegracaoModel
    {
        [DynamoDBHashKey("COD_IDEF_ITGR", typeof(GuidTypeConverter))]
        public Guid CodigoIntegracao { get; set; }

        [DynamoDBRangeKey("NOM_SIS_ITGR")]
        public string NomeSistemaIntegracao { get; set; }

        [DynamoDBProperty("COD_STAT_ITGR")]
        public EnumResultadoIntegracao CodigoStatusIntegracao { get; set; }

        [DynamoDBProperty("TXT_ITGR_RESU")]
        public string TextoIntegracaoResultado { get; set; }
    }

    [Flags]
    public enum EnumResultadoIntegracao
    {
        INTEGRACAO_NAO_ENCONTRADA = 0,
        INTEGRACAO_FINALIZADA = 1,
        INTEGRACAO_INICIADA = 3,
        INTEGRACAO_ERRO = 4,
        INTEGRACAO_EZ5 = 7
    }
}
