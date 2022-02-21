using AWS.Utilities.Core.Sns;
using Integracao.Api.Interfaces;
using Integracao.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegracaoController : ControllerBase
    {
        private readonly IIntegracaoRepository _repository;
        private readonly ISnsMessage _snsMessage;

        public IntegracaoController(IIntegracaoRepository repository,
                                    ISnsMessage snsMessage)
        {
            _repository = repository;
            _snsMessage = snsMessage;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntegracaoModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var model = await _repository.GetAllAsync();

            if (!model.Any())
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        [Route("Status/{codigoIntegracao}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string codigoIntegracao)
        {
            var model = await _repository.GetStatusAsync(codigoIntegracao);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        [Route("{codigoIntegracao}/{nomeSistemaIntegracao}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntegracaoModel))]
        public async Task<IActionResult> Get(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            var model = await _repository.GetByIdAsync(codigoIntegracao, nomeSistemaIntegracao);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntegracaoModel))]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> Add()
        {
            var model = new IntegracaoModel()
            {
                CodigoIntegracao = Guid.NewGuid(),
                NomeSistemaIntegracao = "SISTEMA_" + new Random().Next(),
                TextoIntegracaoResultado = "TEXTO_" + new Random().Next(),
                CodigoStatusIntegracao = EnumStatus.INTEGRACAO_INICIADA
            };

            await _repository.SaveAsync(model);

            var responseSns = await SmsMessage(model);

            if (responseSns.HasError)
                return BadRequest(responseSns.Message);

            return Ok(model);
        }

        [HttpPut]
        [Route("update/{codigoIntegracao}/{nomeSistemaIntegracao}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IntegracaoModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            var item = new IntegracaoModel()
            {
                CodigoIntegracao = codigoIntegracao,
                NomeSistemaIntegracao = nomeSistemaIntegracao,
                TextoIntegracaoResultado = "TEXTO_" + new Random().Next()
            };

            await _repository.SaveAsync(item);

            var model = await _repository.GetByIdAsync(codigoIntegracao, nomeSistemaIntegracao);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpDelete]
        [Route("delete/{codigoIntegracao}/{nomeSistemaIntegracao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid codigoIntegracao, string nomeSistemaIntegracao)
        {
            var model = await _repository.GetByIdAsync(codigoIntegracao, nomeSistemaIntegracao);

            if (model == null)
                return NotFound();

            await _repository.DeleteAsync(model);

            return Ok();
        }

        private async Task<ResponseSns> SmsMessage(IntegracaoModel model)
        {
            var messageSms = new StringBuilder();
            messageSms.Append("Aws Sns Service");
            messageSms.AppendLine();
            messageSms.AppendLine();
            messageSms.AppendFormat("CodigoIntegracao: {0}", model.CodigoIntegracao);
            messageSms.AppendLine();
            messageSms.AppendLine();
            messageSms.AppendFormat("NomeSistemaIntegracao: {0}", model.NomeSistemaIntegracao);
            messageSms.AppendLine();
            messageSms.AppendLine();
            messageSms.AppendFormat("TextoIntegracaoResultado: {0}", model.TextoIntegracaoResultado);

            return await _snsMessage.SmsMessage("+Phone", messageSms.ToString());
        }
    }
}
