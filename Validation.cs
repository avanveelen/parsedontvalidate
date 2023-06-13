namespace ParseDontValidate;

public class Validation<TFailure, TSuccess>
{
    private readonly bool isFailure;

    //private readonly List<TFailure> failures;
    private readonly TSuccess success;
    private readonly TFailure fail;
    
    public Validation(TSuccess success)
    {
        this.success = success;
        this.fail = default(TFailure);
        this.isFailure = false;
    }

    public Validation(TFailure fail)
    {
        this.success = default(TSuccess);
        this.fail = fail;
        this.isFailure = true;
    }

    public static implicit operator Validation<TFailure, TSuccess>(TSuccess success)
    {
        return new Validation<TFailure, TSuccess>(success);
    }

    public R Match<R>(Func<TSuccess, R> success, Func<TFailure, R> failure)
    {
        return this.isFailure ? failure(this.fail) : success(this.success);
    }

    public Validation<TFailure, V> SelectMany<U, V>(Func<TSuccess, Validation<TFailure, U>> bind, Func<TSuccess, U, V> project)
    {
        if (this.isFailure)
        {
            return new Validation<TFailure, V>(this.fail);
        }
        
        var s = bind(this.success);
        return s.isFailure
            ? new Validation<TFailure, V>(s.fail)
            : new Validation<TFailure, V>(project(this.success, s.success));
    }
}