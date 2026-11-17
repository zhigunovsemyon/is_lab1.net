namespace is_lab1.net;
using is_lab1.net.LabExceptions;

public class CaesarCipher(Byte shift, String alphabet)
{
	private readonly String _alphabet = alphabet.ToUpper();
	private readonly Byte _shift = (Byte)(shift % alphabet.Length);

	public string Encrypt (string plaintext)
	{
		throw new NotImplementedException();
	}

	public string Decrypt (string ciphertext)
	{
		throw new NotImplementedException();
	}
}
