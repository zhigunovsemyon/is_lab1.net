namespace is_lab1.net;
using is_lab1.net.LabExceptions;
using System.Text;

public class CaesarCipher(Byte shift, String alphabet)
{
	private readonly string _alphabet = alphabet.ToUpper();
	private readonly Byte _shift = (Byte)(shift % alphabet.Length);
	
	public Result<String,Exception> Encrypt (String plaintext)
	{
		try {
			var txt = VerifyString(plaintext);
			return Result<String, Exception>.CreateSuccess(CaesarTransform(txt, _shift));
		}
		catch (Exception ex) { 
			return Result<String, Exception>.CreateFailure(ex);
		}
	}

	public string Decrypt (String ciphertext)
	{
		var reverseShift = (Byte)((_alphabet.Length - _shift) % _alphabet.Length);
		return CaesarTransform(ciphertext, reverseShift);
	}

	private string CaesarTransform (String text, Byte shift)
	{
		var sb = new StringBuilder(text.Length);
		foreach (char c in text) {
			int idx = _alphabet.IndexOf(c);
			sb.Append(_alphabet[(idx + shift) % _alphabet.Length]);
		}
		return sb.ToString();
	}

	private string VerifyString(String text)
	{
		var upperCaseText = text.ToUpper().ReplaceLineEndings(" ");
		var invalidChars = upperCaseText.Where(c => !_alphabet.Contains(c));
		if (invalidChars.Any()) {
			var msg = $"Строка {upperCaseText} содержит неподдерживаемые символы";
			throw new BadTextException(msg);
		}
		return upperCaseText;
	}
}
