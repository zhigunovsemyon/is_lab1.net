namespace is_lab1.net;

/*
 * Задание:
 * Во всех вариантах необходимо разработать программы для шифрования и
 * расшифрования сообщений двумя указанными методами. Обрабатываемые сообщения,
 * результаты шифрования и ключи должны храниться в текстовых файлах. Числовые
 * ключи могут вводиться с клавиатуры.
 *
 * 1-я ступень: система шифрования Цезаря
 * 2-я ступень: шифрующие таблицы с двойной перестановкой по ключу
 * Алфавит: 35 символов (А…Я, пробел, «.» )
 */
internal static class Program
{
	private const int ExitSuccess = 0;
	private const int ExitFailure = -1;

	private static int Main()
	{
		var args = Environment.GetCommandLineArgs();
		var execname = args[0];
		if (args.Length != 5) {
			Program.Usage(execname);
			return ExitFailure;
		}

		var modename = args[1];
		var inputFile = args[2];
		var keyFile = args[3];
		var outputFile = args[4];

		var mode = Program.GetMode(modename);
		if (mode == Mode.InvalidMode) {
			Console.WriteLine("{0} не является действующим режимом", modename);
			Program.Usage(execname);
			return ExitFailure;
		}

		var readFileReturn = Program.ReadFile(inputFile);
		if (!readFileReturn) {
			Program.DisplayFileError(inputFile, readFileReturn.Error);
			return ExitFailure;
		}
		var inputFileContent = readFileReturn.Value ?? "";

		var keyFileReturn = Program.ReadFile(inputFile);
		if (!keyFileReturn) {
			Program.DisplayFileError(keyFile, keyFileReturn.Error);
			return ExitFailure;
		}
		var keyFileContent = keyFileReturn.Value ?? "";

		Console.WriteLine("Входной файл:\n{0}", inputFileContent);
		Console.WriteLine("Ключ:\n{0}", keyFileContent);
		return ExitSuccess;
	}

	private static void Usage(string execname)
	{
		Console.WriteLine("{0} (d|e) input_file key_file output_file", execname);
	}

	private static Mode GetMode(string modename)
	{
		if (modename.Length != 1) {
			return Mode.InvalidMode;
		}

		char modechar = modename[0];
		return modechar switch
		{
			'd' or 'D' => Mode.DecryptMode,
			'e' or 'E' => Mode.EncryptMode,
			_ => Mode.InvalidMode,
		};
	}

	private static Result<string, Exception> ReadFile(string path)
	{
		try {
			using var sr = new StreamReader(path);
			var result = sr.ReadToEnd();
			return Result<string, Exception>.CreateSuccess(result);
		}
		catch (Exception e) {
			return Result<string, Exception>.CreateFailure(e);
		}
	}

	private static void DisplayFileError(string path, Exception? exception)
	{
		Console.WriteLine("Ошибка при чтении файла {0}:", path);
		while (exception != null) {
			Console.WriteLine(exception.Message);
			exception = exception.InnerException;
		}
	}
}
