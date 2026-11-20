namespace is_lab1.net;
using LabExceptions;
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
			Usage(execname);
			return ExitFailure;
		}

		var modename = args[1];
		var inputFile = args[2];
		var keyFile = args[3];
		var outputFile = args[4];

		try {
			var mode = GetMode(modename);
			var inputFileContent = ReadFile(inputFile);
			var keyFileContent =  ReadFile(keyFile);

			Console.WriteLine("Входной файл:\n{0}", inputFileContent);
			Console.WriteLine("Ключ:\n{0}", keyFileContent);
			return ExitSuccess;
		}
		catch (Exception ex) { 
			return EndWithError(ex);
		}
	}

	private static void Usage(string execname)
	{
		Console.WriteLine("{0} (d|e) input_file key_file output_file", execname);
	}

	
	/// <exception cref="InvalidModeException">Неправильно заданный режим работы</exception>
	private static Mode GetMode(string modename)
	{
		if (modename.Length != 1) {
			throw new InvalidModeException($"{modename} не является действующим режимом");
		}

		char modechar = modename[0];
		return modechar switch
		{
			'd' or 'D' => Mode.DecryptMode,
			'e' or 'E' => Mode.EncryptMode,
			_ => throw new InvalidModeException($"{modechar} не является действующим режимом"),
		};
	}
	
	/// <exception cref="BadFileException">Ошибка при чтении файла</exception>
	private static String ReadFile(string path)
	{
		try {
			using var sr = new StreamReader(path);
			return sr.ReadToEnd();
		}
		catch (Exception e) {
			throw new BadFileException($"Ошибка при чтении файла {path}", e);
		}
	}

	private static int EndWithError(Exception? exception)
	{
		while (exception != null) {
			Console.WriteLine(exception.Message);
			exception = exception.InnerException;
		}
		return ExitFailure;
	}
}
