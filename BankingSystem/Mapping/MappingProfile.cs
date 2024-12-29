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
        CreateMap<Account, BalanceDto>()
            .ConstructUsing(src => new BalanceDto(
                src.AccountNumber,
                src.AccountType,
                $"{src.Balance:C}" // Format the balance with a dollar sign
            ));
        CreateMap<Account, CreateAccountDto>();

        CreateMap<Account, AccountsDto>()
            .ConstructUsing(src => new AccountsDto(
                src.AccountNumber,
                src.AccountType,
                $"{src.Balance:C}" // Format the balance with a dollar sign
            ));

       CreateMap<Transaction, TransactionDto>()
                .ConstructUsing(src => new TransactionDto(
                    src.TransactionType,
                    FormatBalance(src.Amount, src.Currency), // Format based on currency type
                    src.Currency,  // Format Amount with dollar sign as _Amount
                    src.TransactionTime.ToString("yyyy-MM-dd HH:mm:ss zzz")
                ));



        // DTO to Entity mappings
        CreateMap<CreateAccountDto, Account>();

        
    }
    /// <summary>Handling Amounts and Currencies for Customers.</summary>
    public static string FormatBalance(decimal balance, string currency)
    {
        // Check the currency type and format accordingly
        var culture = currency switch
        {
            "USD" => new System.Globalization.CultureInfo("en-US"), // USD
            "EUR" => new System.Globalization.CultureInfo("de-DE"), // Euro (Germany)
            "GBP" => new System.Globalization.CultureInfo("en-GB"), // GBP (UK)
            "CHF" => new System.Globalization.CultureInfo("fr-CH"), // CHF (Swiss franc, French-speaking Switzerland)
            "JPY" => new System.Globalization.CultureInfo("ja-JP"), // JPY (Japanese)
            "AED" => new System.Globalization.CultureInfo("ar-AE"), // UAE Dirham (Arabic - UAE)
            "SAR" => new System.Globalization.CultureInfo("ar-SA"), // Saudi Riyal (Arabic - Saudi Arabia)
            "CAD" => new System.Globalization.CultureInfo("en-CA"), // Canadian Dollar (English - Canada)
            "JOD" => new System.Globalization.CultureInfo("ar-JO"), // Jordanian Dinar (Arabic - Jordan)
            _ => new System.Globalization.CultureInfo("en-US"), // Default to USD
        };

        return string.Format(culture, "{0:C2}", balance); // Format with two decimal places
    }
}
