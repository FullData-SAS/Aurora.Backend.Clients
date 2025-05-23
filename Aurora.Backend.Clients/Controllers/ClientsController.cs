using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using A3.Backend.Identities.Services.Models;
using Aurora.Backend.Clients.Services.Contracts;
using Aurora.Backend.Clients.Services.Enumerables;
using Aurora.Backend.Clients.Services.Models.Client;
using Aurora.Backend.Clients.Services.Utils;

namespace Aurora.Backend.Clients.Controllers;

public class ClientsController : BaseController
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;
    
    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    [HttpGet]
    [Route("GetAll")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<ClientUpdateModel>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _clientService.GetAll();
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }
    
    [HttpGet]
    [Route("Get")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ClientUpdateModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Get([Required][FromQuery] Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _clientService.Get(id);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

    [HttpPost]
    [Route("Create")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Create(ClientCreateModel request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _clientService.Create(request);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }
    
    [HttpPut]
    [Route("Update")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Update(ClientUpdateModel request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _clientService.Update(request);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

    [HttpDelete]
    [Route("Delete")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<bool>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<object>))]
    public async Task<IActionResult> Delete([Required][FromQuery] Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(GetApiResponse(EResult.ERROR, TextValues.MODEL_IS_NOT_VALID, GetErrorListFromModelState()));
    
            var result = await _clientService.Delete(id);
            
            return result.Status == EResult.SUCCESS 
                ? Ok(GetApiResponse(EResult.SUCCESS, EResult.SUCCESS.ToString(), result.Response))
                : BadRequest(GetApiResponse(EResult.ERROR, result.Message, null));
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Concat(ex.Message, Environment.NewLine, ex.StackTrace));
            return StatusCode((int)HttpStatusCode.InternalServerError, GetApiResponse(EResult.ERROR, TextValues.GENERAL_ERROR, null));
        }
    }

}