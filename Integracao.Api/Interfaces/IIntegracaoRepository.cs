using Integracao.Api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.Api.Interfaces
{
    public interface IIntegracaoRepository
    {
        Task<IntegracaoModel> GetByIdAsync(Guid codigoIntegracao, string nomeSistemaIntegracao);
        Task<EnumStatus?> GetStatusAsync(string codigoIntegracao);
        Task SaveAsync(IntegracaoModel resultadoIntegracao);
        Task DeleteAsync(IntegracaoModel resultadoIntegracao);
        Task<IEnumerable<IntegracaoModel>> GetAllAsync();
    }
}
