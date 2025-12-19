namespace SharedKernel;

public static class GeneralErrors
{
    public static class Entity
    {
        public static Error NotFound(string name)
        {
            return Error.NotFound(
                "common.entity.not_found",
                $"{name} was not found");
        }

        public static Error NotFound(string name, object id)
        {
            return Error.NotFound(
                "common.entity.not_found",
                $"{name} with id '{id}' was not found");
        }

        public static Error AlreadyExists(string name)
        {
            return Error.AlreadyExist(
                "common.entity.already_exists",
                $"{name} already exists");
        }

        public static Error InvalidState(string name, string reason)
        {
            return Error.Validation(
                "common.entity.invalid_state",
                $"{name} has invalid state: {reason}");
        }
    }

    public static class Validation
    {
        public static Error ValueTooSmall<T>(string name, T min)
            where T : struct, IComparable
        {
            return Error.Validation(
                "common.validation.too_small",
                $"{name} must be at least {min}",
                ErrorField.Normalize(name));
        }

        public static Error ValueTooLarge<T>(string name, T max)
            where T : struct, IComparable
        {
            return Error.Validation(
                "common.validation.too_large",
                $"{name} must not exceed {max}",
                ErrorField.Normalize(name));
        }

        public static Error ValueTooLong(string name, int max)
        {
            return Error.Validation(
                "common.validation.too_long",
                $"{name} must not exceed {max} characters",
                ErrorField.Normalize(name));
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation(
                "common.validation.required",
                $"{label} is required",
                ErrorField.Normalize(label));
        }

        public static Error InvalidFormat(string name, string message)
        {
            return Error.Validation(
                "common.validation.invalid_format",
                $"{name} has invalid format: {message}",
                ErrorField.Normalize(name));
        }
    }
}
