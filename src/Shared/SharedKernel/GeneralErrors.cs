namespace SharedKernel;

public static class GeneralErrors
{
    public static class Entity
    {
        public static Error NotFound(string name)
        {
            return Error.NotFound(
                "entity.not.found",
                $"{name} was not found");
        }

        public static Error NotFound(string name, object id)
        {
            return Error.NotFound(
                "entity.not.found",
                $"{name} with id '{id}' was not found");
        }

        public static Error AlreadyExists(string name)
        {
            return Error.Conflict(
                "entity.already.exists",
                $"{name} already exists");
        }

        public static Error InvalidState(string name, string reason)
        {
            return Error.Validation(
                "entity.invalid.state",
                $"{name} has invalid state: {reason}");
        }
    }

    public static class Validation
    {
        public static Error ValueTooSmall<T>(string name, T min)
            where T : struct, IComparable
        {
            return Error.Validation(
                "value.too.small",
                $"{name} must be at least {min}");
        }

        public static Error ValueTooLarge<T>(string name, T max)
            where T : struct, IComparable
        {
            return Error.Validation(
                "value.too.large",
                $"{name} must not exceed {max}");
        }

        public static Error ValueTooLong(string name, int max)
        {
            return Error.Validation(
                "value.too.long",
                $"{name} must not exceed {max} characters");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation(
                "value.is.required",
                $"{label} is required");
        }

        public static Error InvalidFormat(string name, string message)
        {
            return Error.Validation(
                "value.invalid.format",
                $"{name} has invalid format: {message}");
        }
    }
}