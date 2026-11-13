namespace is_lab1.net;

public class Result<T,TErr>
{
	public bool IsValid { get; init; }

	public T? Value { get; init; }

	public TErr? Error { get; init; }

	private Result(T result)
	{
		this.IsValid = true;
		this.Value = result;
	}

	private Result(TErr error)
	{
		this.IsValid = false;
		this.Error = error;
	}

	public static Result<T, TErr> CreateSuccess(T result) => new (result);

	public static Result<T, TErr> CreateFailure(TErr error) => new (error);
}
