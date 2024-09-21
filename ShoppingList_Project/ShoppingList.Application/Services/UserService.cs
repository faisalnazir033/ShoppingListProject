using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Repositories;

namespace ShoppingList.Application.Services;

public class UserService(IUserRepository usersRepository) : IUserService
{
    private readonly IUserRepository usersRepository = usersRepository;

    public async Task AddUserAsync(User user)
    {
        await usersRepository.AddUserAsync(user);
    }  

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await usersRepository.GetUserByEmailAsync(email);
    }
}
