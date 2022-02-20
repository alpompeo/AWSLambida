using Integracao.Api.Interfaces;
using Integracao.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegracaoController : ControllerBase
    {
        private IIntegracaoRepository _repository;
        private readonly ILogger<IntegracaoController> _logger;

        public IntegracaoController(ILogger<IntegracaoController> logger,
                                    IIntegracaoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ResultadoIntegracaoModel>> Get()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("Status/{codigoIntegracao}")]
        public async Task<int?> Get(string codigoIntegracao)
        {
            try
            {
                return await _repository.GetStatusAsync(codigoIntegracao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{codigoIntegracao}/{nomeSistemaIntegracao}")]
        public async Task<ResultadoIntegracaoModel> Get(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            try
            {
                return await _repository.GetByIdAsync(codigoIntegracao, nomeSistemaIntegracao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ResultadoIntegracaoModel> Add()
        {
            try
            {
                var item = new ResultadoIntegracaoModel()
                {
                    CodigoIntegracao = Guid.NewGuid(),
                    NomeSistemaIntegracao = "SISTEMA_" + new Random().Next(),
                    TextoIntegracaoResultado = "TEXTO_" + new Random().Next(),
                    CodigoStatusIntegracao = EnumResultadoIntegracao.INTEGRACAO_INICIADA
                };

                await _repository.AddAsync(item);

                return await _repository.GetByIdAsync(item.CodigoIntegracao, item.NomeSistemaIntegracao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("update/{codigoIntegracao}/{nomeSistemaIntegracao}")]
        public async Task<ResultadoIntegracaoModel> Update(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            try
            {
                var item = new ResultadoIntegracaoModel()
                {
                    CodigoIntegracao = codigoIntegracao,
                    NomeSistemaIntegracao = nomeSistemaIntegracao,
                    TextoIntegracaoResultado = "TEXTO_" + new Random().Next()
                };

                await _repository.AddAsync(item);

                return await _repository.GetByIdAsync(item.CodigoIntegracao, item.NomeSistemaIntegracao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        [Route("delete/{codigoIntegracao}/{nomeSistemaIntegracao}")]
        public async Task Delete(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            try
            {
                var resultadoIntegracaoModel = await _repository.GetByIdAsync(codigoIntegracao, nomeSistemaIntegracao);

                if (resultadoIntegracaoModel == null)
                    throw new Exception("Não foi encontrado a integração.");

                await _repository.DeleteAsync(resultadoIntegracaoModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
