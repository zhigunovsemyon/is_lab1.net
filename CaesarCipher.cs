namespace is_lab1.net;
using System.Text;
using is_lab1.net.LabExceptions;

public class CaesarCipher(Byte shift, String alphabet)
{
	private readonly (Char, Byte)[] _alphabet = EnumerateAlphabet(alphabet);
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
			foreach ((var ac, var idx) in _alphabet) {
				if (c == ac) {
					charFound = true;
					ret.Add(idx);
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
		var chars = bytes.Select(code => 
			_alphabet.Where(e => (e.Item2 == code)).First().Item1 
		);
		foreach (var c in chars) {
			sb.Append(c);
		}
		return sb.ToString();
	}

	private static (Char, Byte)[] EnumerateAlphabet (String alphabet) 
		=> alphabet
			.ToUpper()
			.Select((c, i) => (c, (Byte)(i + 1)))
			.ToArray();
}
