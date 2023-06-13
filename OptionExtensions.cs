namespace ParseDontValidate;

public static class OptionExtensions
{
    public static Validation<TError, TValue> ToValidation<TValue, TError>(this Option<TValue> option, TError error)
    {
        return option.Match(value => new Validation<TError, TValue>(value),
            () => new Validation<TError, TValue>(error));
    }
}