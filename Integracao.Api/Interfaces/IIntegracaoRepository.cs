using Integracao.Api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.Api.Interfaces
{
    public interface IIntegracaoRepository
    {
        Task<ResultadoIntegracaoModel> GetByIdAsync(Guid codigoIntegracao, string nomeSistemaIntegracao);
        Task<int?> GetStatusAsync(string codigoIntegracao);
        Task AddAsync(ResultadoIntegracaoModel resultadoIntegracao);
        Task DeleteAsync(ResultadoIntegracaoModel resultadoIntegracao);
        Task<IEnumerable<ResultadoIntegracaoModel>> GetAllAsync();
    }
}
