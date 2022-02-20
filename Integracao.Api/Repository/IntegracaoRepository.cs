using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Integracao.Api.Interfaces;
using Integracao.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracao.Api.Repository
{
    public class IntegracaoRepository : IIntegracaoRepository
    {
        private IDynamoDbContext<ResultadoIntegracaoModel> _context;

        public IntegracaoRepository(IDynamoDbContext<ResultadoIntegracaoModel> context)
        {
            _context = context;
        }

        public async Task<ResultadoIntegracaoModel> GetByIdAsync(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            //TODO: Exemplo utilizando Scan, mas não é performatico.
            //Utilzar somente quando não há um indice definido na tabela.
            //List<ScanCondition> conditions = new List<ScanCondition>();
            //conditions.Add(new ScanCondition("CodigoIntegracao", ScanOperator.Equal, codigoIntegracao));
            //conditions.Add(new ScanCondition("NomeSistemaIntegracao", ScanOperator.Equal, nomeSistemaIntegracao));
            //var result = await _context.ScanAsync(conditions);

            return (await _context.QueryAsync(codigoIntegracao.ToString(), QueryOperator.Equal, new object[] { nomeSistemaIntegracao })).SingleOrDefault();
        }

        public async Task<IEnumerable<ResultadoIntegracaoModel>> GetAllAsync()
        {
            return (await _context.ScanAsync(new List<ScanCondition>())).ToList();
        }

        public async Task AddAsync(ResultadoIntegracaoModel resultadoIntegracao)
        {
            await _context.Save(resultadoIntegracao);
        }

        public async Task DeleteAsync(ResultadoIntegracaoModel resultadoIntegracao)
        {
            await _context.DeleteByIdAsync(resultadoIntegracao);
        }

        public async Task<int?> GetStatusAsync(string codigoIntegracao)
        {
            var result = (await _context.QueryAsync(codigoIntegracao.ToString())).FirstOrDefault();

            return (int?)result?.CodigoStatusIntegracao;
        }
    }
}

