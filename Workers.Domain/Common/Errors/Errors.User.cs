using ErrorOr;

namespace Workers.Domain.Common.Errors;

public static class Errors
{
    public static class User {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "El correo esta en uso."
        );

        public static Error InvalidCredentials => Error.Conflict(
            code: "User.LoginConflict",
            description: "Credenciales no validas."
        );
    }
}