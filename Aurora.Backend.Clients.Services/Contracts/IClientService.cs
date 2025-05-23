using A3.Backend.Identities.Services.Models;
using Aurora.Backend.Clients.Services.Models.Client;

namespace Aurora.Backend.Clients.Services.Contracts;

public interface IClientService
{
    Task<Result<IEnumerable<ClientUpdateModel>>> GetAll();
    Task<Result<ClientUpdateModel>> Get(Guid guid);
    Task<Result<bool>> Create(ClientCreateModel clientCreateModel);
    Task<Result<bool>> Update(ClientUpdateModel clientUpdateModel);
    Task<Result<bool>> Delete(Guid guid);
}