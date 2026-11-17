namespace is_lab1.net;
using is_lab1.net.LabExceptions;

public class CaesarCipher(Byte shift, String alphabet)
{
	private readonly String _alphabet = alphabet.ToUpper();
	private readonly Byte _shift = (Byte)(shift % alphabet.Length);

	public string Encrypt (string plaintext)
	{
		var normalizedText = VerifyAndNormalizePlaintext(plaintext);
		throw new NotImplementedException();
	}

	public string Decrypt (string ciphertext)
	{
		throw new NotImplementedException();
	}

	private string VerifyAndNormalizePlaintext (string text)
	{
		var upperCaseText = text.ToUpper();
		foreach (char c in upperCaseText) {
			if (!_alphabet.Contains(c)) {
				// разработать класс исключений, переделать под Result
				throw new NotImplementedException();
			}
		}
		return upperCaseText;
	}
}
