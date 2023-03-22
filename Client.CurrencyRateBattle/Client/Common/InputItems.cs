using Client.Common.CodeWays;
using Client.Common.Validator;
using Client.Interfaces;
using Client.Models;
using FluentValidation.Results;

namespace Client.Common;

public class InputItems : IInputItems
{
    private readonly IHttpClientWorker _httpClientWorker;
    private readonly LoginValidator _loginValidator;
    private readonly NameValidator _nameValidator;
    private readonly PassValidator _passValidator;

    public InputItems(IHttpClientWorker httpClientWorker, NameValidator nameValidator, PassValidator passValidator, LoginValidator loginValidator)
    {
        _httpClientWorker = httpClientWorker;
        _loginValidator = loginValidator;
        _nameValidator = nameValidator;
        _passValidator = passValidator;
    }

    public (ValidationResult, string) InputData(UserItem userItem)
    {
        while (true)
        {
            ConsoleColors.White($"Enter your {userItem}: ");

            var prop = Console.ReadLine();

            ValidationResult result;

            //validation user items
            switch (userItem)
            {
                case UserItem.Login:
                    result = _loginValidator.Validate(prop!);
                    break;

                case UserItem.Pass:
                    result = _passValidator.Validate(prop!);
                    prop = Hash.GetHash(prop!);
                    break;

                case UserItem.Name:
                    result = _nameValidator.Validate(prop!);
                    break;

                default:
                    throw new ArgumentException(userItem.ToString());
            }

            return (result, prop!);
        }
    }

    public bool SendRequest(HttpWay httpWay, User user)
    {
        HttpResponseMessage result;

        //choose url way
        switch (httpWay)
        {
            case HttpWay.Login:
                result = _httpClientWorker.PostLogin(user!);
                break;

            case HttpWay.Registration:
                result = _httpClientWorker.PostRegistration(user!);
                break;

            case HttpWay.ChangeName:
                result = _httpClientWorker.PostNameToUser(user.Login!, user.Name!);
                break;

            case HttpWay.ChangePassword:
                result = _httpClientWorker.PostPasswordToUser(user.Login!, user.Password!);
                break;

            default:
                throw new ArgumentException(httpWay.ToString());
        }

        if (result.IsSuccessStatusCode)
        {
            ConsoleColors.Green("\nSuccess enter!\n");
            ConsoleColors.Yellow("Press any button to continue");

            _ = Console.ReadKey();

            return true;
        }

        ConsoleColors.Red(result.Content.ReadAsStringAsync().Result);

        return false;
    }
}
