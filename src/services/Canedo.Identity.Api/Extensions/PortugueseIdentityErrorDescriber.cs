using Microsoft.AspNetCore.Identity;

namespace Canedo.Identity.Api.Extensions
{
    public class PortugueseIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() => GetIdentityError(nameof(DefaultError), "Ocorreu um erro desconhecido.");
        public override IdentityError ConcurrencyFailure() => GetIdentityError(nameof(ConcurrencyFailure), "Falha de concorrência otimista, o objeto foi modificado.");
        public override IdentityError PasswordMismatch() => GetIdentityError(nameof(PasswordMismatch), "Senha incorreta.");
        public override IdentityError InvalidToken() => GetIdentityError(nameof(InvalidToken), "Token inválido.");
        public override IdentityError LoginAlreadyAssociated() => GetIdentityError(nameof(LoginAlreadyAssociated), "Já existe um usuário com este login.");
        public override IdentityError InvalidUserName(string userName) => GetIdentityError(nameof(InvalidUserName), $"O login '{userName}' é inválido, pode conter apenas letras ou dígitos.");
        public override IdentityError InvalidEmail(string email) => GetIdentityError(nameof(InvalidEmail), $"O email '{email}' é inválido.");
        public override IdentityError DuplicateUserName(string userName) => GetIdentityError(nameof(DuplicateUserName), $"O login '{userName}' já está sendo utilizado.");
        public override IdentityError DuplicateEmail(string email) => GetIdentityError(nameof(DuplicateEmail), $"O email '{email}' já está sendo utilizado.");
        public override IdentityError InvalidRoleName(string role) => GetIdentityError(nameof(InvalidRoleName), $"A permissão '{role}' é inválida.");
        public override IdentityError DuplicateRoleName(string role) => GetIdentityError(nameof(DuplicateRoleName), $"A permissão '{role}' já está sendo utilizada.");
        public override IdentityError UserAlreadyHasPassword() => GetIdentityError(nameof(UserAlreadyHasPassword), "O usuário já possui uma senha definida.");
        public override IdentityError UserLockoutNotEnabled() => GetIdentityError(nameof(UserLockoutNotEnabled), "O lockout não está habilitado para este usuário.");
        public override IdentityError UserAlreadyInRole(string role) => GetIdentityError(nameof(UserAlreadyInRole), $"O usuário já possui a permissão '{role}'.");
        public override IdentityError UserNotInRole(string role) => GetIdentityError(nameof(UserNotInRole), $"O usuário não tem a permissão '{role}'.");
        public override IdentityError PasswordTooShort(int length) => GetIdentityError(nameof(PasswordTooShort), $"As senhas devem conter ao menos {length} caracteres.");
        public override IdentityError PasswordRequiresNonAlphanumeric() => GetIdentityError(nameof(PasswordRequiresNonAlphanumeric), "As senhas devem conter ao menos um caracter não alfanumérico.");
        public override IdentityError PasswordRequiresDigit() => GetIdentityError(nameof(PasswordRequiresDigit), "As senhas devem conter ao menos um digito ('0'-'9').");
        public override IdentityError PasswordRequiresLower() => GetIdentityError(nameof(PasswordRequiresLower), "As senhas devem conter ao menos um caracter em caixa baixa ('a'-'z').");
        public override IdentityError PasswordRequiresUpper() => GetIdentityError(nameof(PasswordRequiresUpper), "As senhas devem conter ao menos um caracter em caixa alta ('A'-'Z').");

        private static IdentityError GetIdentityError(string code, string description) => new IdentityError { Code = code, Description = description };
    }
}
