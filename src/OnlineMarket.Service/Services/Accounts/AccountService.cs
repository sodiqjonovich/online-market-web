using OnlineMarket.DataAccess.Interfaces;
using OnlineMarket.Domain.Entities.Users;
using OnlineMarket.Service.Common.Security;
using OnlineMarket.Service.Dtos.Accounts;
using OnlineMarket.Service.Interfaces.Accounts;

namespace OnlineMarket.Service.Services.Accounts;
public class AccountService : IAccountService
{
    private readonly IUnitOfWork _repository;
    public AccountService(IUnitOfWork unitOfWork)
    {
        this._repository = unitOfWork;
    }
    public Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterAsync(AccountRegisterDto accountRegisterDto)
    {
        var emailedUser = await _repository.Users.FirstOrDefaultAsync(x => x.Email == accountRegisterDto.Email);
        if (emailedUser is not null) throw new Exception();

        var phonedUser = await _repository.Users.FirstOrDefaultAsync(x => x.PhoneNumber == accountRegisterDto.PhoneNumber);
        if (phonedUser is not null) throw new Exception();

        var hasherResult = PasswordHasher.Hash(accountRegisterDto.Password);
        var user = (User) accountRegisterDto;
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        _repository.Users.Add(user);
        var dbResult = await _repository.SaveChangesAsync();
        return dbResult > 0;
    }
}
