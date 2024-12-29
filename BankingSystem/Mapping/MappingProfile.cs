using AutoMapper;
using BankingSystem.Dtos;
using BankingSystem.Entities;

/// <summary>Defines mapping configurations for DTOs and entities.</summary>
public class MappingProfile : Profile
{
    /// <summary>Initializes mappings between entities and DTOs.</summary>
    public MappingProfile()
    {
        // Entity to DTO mappings
        CreateMap<Account, BalanceDto>();

        CreateMap<Account, CreateAccountDto>();

        CreateMap<Account, AccountsDto>();

        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.TransactionTime,
                       opt => opt.MapFrom(src => src.TransactionTime.ToString("yyyy-MM-dd HH:mm:ss zzz")));

        // DTO to Entity mappings
        CreateMap<CreateAccountDto, Account>();
    }
}
