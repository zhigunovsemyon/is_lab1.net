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
internal class Program
{
	const int EXIT_SUCCESS = 0;
	const int EXIT_FAILURE = -1;

	static int Main()
	{
		var args = Environment.GetCommandLineArgs();
		var execname = args[0];
		if (args.Length != 5) {
			Program.Usage(execname);
			return EXIT_FAILURE;
		}

		var modename = args[1];
		var input_file = args[2];
		var key_file = args[3];
		var output_file = args[4];

		var mode = Program.GetMode(modename);
		if (mode == Mode.INVALID_MODE) {
			Console.WriteLine("{0} не является действующим режимом", modename);
			Program.Usage(execname);
			return EXIT_FAILURE;
		}

		var readFileReturn = Program.ReadFile(input_file);
		if (!readFileReturn.IsValid) {
			Program.DisplayFileError(input_file, readFileReturn.Error);
			return EXIT_FAILURE;
		}
		var inputFileContent = readFileReturn.Value ?? "";

		var keyFileReturn = Program.ReadFile(input_file);
		if (!keyFileReturn.IsValid) {
			Program.DisplayFileError(key_file, keyFileReturn.Error);
			return EXIT_FAILURE;
		}
		var keyFileContent = keyFileReturn.Value ?? "";

		Console.WriteLine("Входной файл:\n{0}", inputFileContent);
		Console.WriteLine("Ключ:\n{0}", keyFileContent);
		return EXIT_SUCCESS;
	}

	static void Usage(string execname)
	{
		Console.WriteLine("{0} (d|e) input_file key_file output_file", execname);
	}

	static Mode GetMode(string modename)
	{
		if (modename.Length != 1) {
			return Mode.INVALID_MODE;
		}

		char modechar = modename[0];
		return modechar switch
		{
			'd' or 'D' => Mode.DECRYPT_MODE,
			'e' or 'E' => Mode.ENCRYPT_MODE,
			_ => Mode.INVALID_MODE,
		};
	}

	static Result<string, Exception> ReadFile(string path)
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

	static void DisplayFileError(string path, Exception? exception)
	{
		Console.WriteLine("Ошибка при чтении файла {0}:", path);
		while (exception != null) {
			Console.WriteLine(exception.Message);
			exception = exception.InnerException;
		}
	}
}
