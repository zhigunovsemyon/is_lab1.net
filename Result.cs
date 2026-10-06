namespace is_lab1.net;

public class Result<T,Err>
{
	public bool IsValid { get; init; }

	public T? Value { get; init; }

	public Err? Error { get; init; }

	private Result(T result)
	{
		this.IsValid = true;
		this.Value = result;
	}

	private Result(Err error)
	{
		this.IsValid = false;
		this.Error = error;
	}

	public static Result<T, Err> CreateSuccess(T result) => new (result);

	public static Result<T, Err> CreateFailure(Err error) => new (error);
}
