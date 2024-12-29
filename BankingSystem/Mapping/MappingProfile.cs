using AutoMapper;
using BankingSystem.Dtos;
using BankingSystem.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO mappings
        CreateMap<Account, BalanceDto>();
        CreateMap<Account, CreateAccountDto>();
        CreateMap<Account, AccountsDto>();
        CreateMap<Transaction, TransactionDto>();

        // DTO to Entity mappings
        CreateMap<CreateAccountDto, Account>();
    }
}
