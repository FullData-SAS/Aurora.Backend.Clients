using A3.Backend.Identities.Services.Models;
using Aurora.Backend.Clients.Services.Contracts;
using Aurora.Backend.Clients.Services.Enumerables;
using Aurora.Backend.Clients.Services.Models.Client;
using Aurora.Backend.Clients.Services.Persistence.Entities;
using Microsoft.Extensions.Logging;

namespace Aurora.Backend.Clients.Services.Implements;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ClientService> _logger;

    public ClientService(IUnitOfWork unitOfWork, ILogger<ClientService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ClientUpdateModel>>> GetAll()
    {
        Result<IEnumerable<ClientUpdateModel>> result = new Result<IEnumerable<ClientUpdateModel>>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<Client>().GetAllAsync();

            if (resultQuery.Count() > 0)
                result = new Result<IEnumerable<ClientUpdateModel>>
                {
                    Message = EResult.SUCCESS.ToString(),
                    Status = EResult.SUCCESS,
                    Response = resultQuery.Select(x => new ClientUpdateModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TaxId = x.TaxId,
                        Location = x.Location,
                        CompanyType = x.CompanyType,
                        Active = x.Active,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    })
                };
            else
                result = new Result<IEnumerable<ClientUpdateModel>>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = null
                };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<IEnumerable<ClientUpdateModel>>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = null
            };
        }
            
        return result;
    }
    
    public async Task<Result<ClientUpdateModel>> Get(Guid guid)
    {
        Result<ClientUpdateModel> result = new Result<ClientUpdateModel>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<Client>().GetByIdAsync(guid);

            if (resultQuery != null)
                result = new Result<ClientUpdateModel>
                {
                    Message = EResult.SUCCESS.ToString(),
                    Status = EResult.SUCCESS,
                    Response = new ClientUpdateModel
                    {
                        Id = resultQuery.Id,
                        Name = resultQuery.Name,
                        TaxId = resultQuery.TaxId,
                        Location = resultQuery.Location,
                        CompanyType = resultQuery.CompanyType,
                        Active = resultQuery.Active,
                        CreatedAt = resultQuery.CreatedAt,
                        UpdatedAt = resultQuery.UpdatedAt
                    }
                };
            else
                result = new Result<ClientUpdateModel>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = null
                };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<ClientUpdateModel>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = null
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Create(ClientCreateModel clientCreateModel)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            await _unitOfWork.GetRepository<Client>().AddAsync(new Client
            {
                Id = Guid.NewGuid(),
                Name = clientCreateModel.Name,
                TaxId = clientCreateModel.TaxId,
                Location = clientCreateModel.Location,
                CompanyType = clientCreateModel.CompanyType,
                Active = clientCreateModel.Active,
                CreatedAt = clientCreateModel.CreatedAt,
                UpdatedAt = clientCreateModel.UpdatedAt
            });
            await _unitOfWork.CommitAsync();

            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Update(ClientUpdateModel clientUpdateModel)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<Client>().GetByIdAsync(clientUpdateModel.Id);
            if (resultQuery == null)
                return new Result<bool>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = false
                };

            resultQuery.Name = clientUpdateModel.Name;
            resultQuery.TaxId = clientUpdateModel.TaxId;
            resultQuery.Location = clientUpdateModel.Location;
            resultQuery.CompanyType = clientUpdateModel.CompanyType;
            resultQuery.Active = clientUpdateModel.Active;
            resultQuery.CreatedAt = clientUpdateModel.CreatedAt;
            resultQuery.UpdatedAt = clientUpdateModel.UpdatedAt;
            
            await _unitOfWork.CommitAsync();
            
            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }
    
    public async Task<Result<bool>> Delete(Guid guid)
    {
        Result<bool> result = new Result<bool>();
        try
        {
            var resultQuery = await _unitOfWork.GetRepository<Client>().GetByIdAsync(guid);
            if (resultQuery == null)
                return new Result<bool>
                {
                    Message = EResult.NO_RESULT.ToString(),
                    Status = EResult.NO_RESULT,
                    Response = false
                };

            resultQuery.Active = false;
            resultQuery.UpdatedAt = DateTime.Now;
            
            await _unitOfWork.CommitAsync();
            
            result = new Result<bool>
            {
                Message = EResult.SUCCESS.ToString(),
                Status = EResult.SUCCESS,
                Response = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} {ex.InnerException?.Message}");
            result = new Result<bool>
            {
                Message = EResult.ERROR.ToString(),
                Status = EResult.ERROR,
                Response = false
            };
        }
            
        return result;
    }

}