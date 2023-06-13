using ParseDontValidate.Option;

namespace ParseDontValidate
{
    public static class F
    {
        public static None None => new();

        public static Option<T> Some<T>(T value) => new(value);
    }

    namespace Option
    {
        public struct None
        {
            internal static None Default => new();
        }

        public struct Some<T>
        {
            internal readonly T Value;

            public Some(T value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.Value = value;
            }
        }
    }


    public readonly struct Option<T>
    {
        private readonly T value;
        private readonly bool isSome;

        public Option(T value)
        {
            this.value = value;
            this.isSome = true;
        }

        public static implicit operator Option<T>(None _)
        {
            return new Option<T>();
        }

        public static implicit operator Option<T>(Some<T> value)
        {
            return new Option<T>(value.Value);
        }

        public static implicit operator Option<T>(T value)
        {
            return value == null ? F.None : new Option<T>(value);
        }

        public R Match<R>(Func<T, R> some, Func<R> none)
        {
            return this.isSome ? some(value) : none();
        }

        public Option<R> Map<R>(Func<T, R> selector)
        {
            return this.isSome
                ? selector(this.value)
                : F.None;
        }

        public Option<R> Bind<R>(Func<T, Option<R>> f)
        {
            return this.Match(f, () => F.None);
        }

        public IEnumerable<T> ToIEnumerable()
        {
            if (isSome)
            {
                yield return this.value;
            }
        }

        public Option<T> Where(Func<T, bool> selector)
        {
            return this.Match(selector, () => false)
                ? this.value
                : F.None;
        }

        public Option<TC> SelectMany<TB, TC>(Func<T, Option<TB>> bind, Func<T, TB, TC> project)
        {
            if (!this.isSome)
            {
                return new Option<TC>();
            }
        
            var option = bind(this.value);
            return option.isSome
                ? project(this.value, option.value)
                : new Option<TC>();
        }

        public Option<R> Select<R>(Func<T, R> selector)
        {
            return Map(selector);
        }
    }
}