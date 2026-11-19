namespace is_lab1.net;
using System.Text;
using is_lab1.net.LabExceptions;

public class CaesarCipher(Byte shift, String alphabet)
{
	private readonly string _alphabet = alphabet.ToUpper();
	private readonly Byte _shift = (Byte)(shift % alphabet.Length);

	public Byte[] Encrypt (string plaintext)
	{
		var normalizedText = VerifyAndConvertPlaintext(plaintext);
		return normalizedText;
		throw new NotImplementedException();
	}

	public string Decrypt (IEnumerable<Byte> ciphertext)
	{
		return BuildStringFromBytes(ciphertext);
		throw new NotImplementedException();
	}

	private Byte[] VerifyAndConvertPlaintext (string text)
	{
		var upperCaseText = text.ToUpper().ReplaceLineEndings(" ");
		var ret = new List<Byte>(text.Length);

		foreach (char c in upperCaseText) {
			Boolean charFound = false;
			for (Byte i = 0; i < _alphabet.Length; i++) { 
				var ac = _alphabet[i];
				if (c == ac) {
					charFound = true;
					ret.Add(i);
					break;
				}
			}
			if (!charFound) {
				//TODO: свой класс исключений, мб переделать под резалт
				throw new NotImplementedException();
			}
		}
		return [..ret];
	}

	private string BuildStringFromBytes (IEnumerable<Byte> bytes)
	{
		var sb = new StringBuilder(bytes.Count());
		var x = _alphabet[0];
		var chars = bytes.Select(code => _alphabet[code]);
		foreach (var c in chars) {
			sb.Append(c);
		}
		return sb.ToString();
	}
}
