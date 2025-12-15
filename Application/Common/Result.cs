namespace Application.Common
{
    /// <summary>
    /// 统一返回结果基类
    /// </summary>
    public class Result
    {
        public bool Success { get; init; }
        public string? Error { get; init; }

        protected Result() { }

        protected Result(bool success, string? error = null)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok() => new(true);
        public static Result Fail(string error) => new(false, error);

        // 转换为泛型版本
        public Result<T> ToGeneric<T>(T? data = default)
            => new() { Success = this.Success, Error = this.Error, Data = data };
    }

    /// <summary>
    /// 带数据的返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        public T? Data { get; init; }

        public static Result<T> DataResult(T data) => new() { Success = true, Data = data };
        public static new Result<T> Fail(string error) => new() { Success = false, Error = error };

        // 从基类创建
        public static Result<T> FromBase(Result result, T? data = default)
            => new() { Success = result.Success, Error = result.Error, Data = data };
    }
}