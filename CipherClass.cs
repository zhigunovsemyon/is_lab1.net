namespace is_lab1.net;
using LabExceptions;

public class CipherClass
{
	private readonly String _alphabet;
	private readonly CaesarCipher _caesar;

	public CipherClass(string keyString, string alphabet)
	{
		// числоЦезаря слово цифры
		var words = keyString.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
		if (words.Length != 3) {
			throw new BadKeyException("Неправильный формат ключа! Ключ: " + keyString);
		}

		if (!Byte.TryParse(words[0], out var caesarNum)) {
			var msg = "Неправильный формат ключа! Ожидалось число, получено: " + words[0];
			throw new BadKeyException(msg);
		}

		_caesar = new(caesarNum, alphabet);
		_alphabet = alphabet;
	}

	public Result<String, Exception> Encrypt (string plaintext)
	{
		return _caesar.Encrypt(plaintext);
	}

	public string Decrypt(string ciphertext)
	{
		return _caesar.Decrypt(ciphertext);
	}
}
