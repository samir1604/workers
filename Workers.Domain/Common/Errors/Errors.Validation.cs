
using ErrorOr;

namespace Workers.Domain.Common.Errors;

public static class Validation
{
    public static class User
    {
        public static Error EmptyFirstName => Error.Validation(
            code: "User.EmptyFirstName",
            description: "Nombre de usuario no valido"
        );

        public static Error MaxLength => Error.Validation(
            code: "User.EmptyFirstName",
            description: "Sobrepasado el maximo de caracteres"
        );

    }
}
