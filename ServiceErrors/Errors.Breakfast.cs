using ErrorOr;

namespace BuberBreakfast.ServiceErrors {
    public static class Errors {
        public static class Breakfast {
            public static Error NotFound => Error.NotFound(
                code: "Breakfast.NotFound",
                description: "Breakfast not found"
            );
            public static Error InvalidPropertyLength(string PropertyName, int MinLength, int MaxLength) => Error.Validation(
                code: $"Breakfast.${PropertyName}",
                description: $"Breakfast {PropertyName} must be at least {MinLength} characters long, and at most {MaxLength} charaters long."
            );

        }
    }
}
