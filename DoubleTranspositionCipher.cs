namespace is_lab1.net;

using System.Linq;
using System.Text;

public class DoubleTranspositionCipher
{
	private const char PaddingChar = '_';

	private readonly int[] _columnPermutation;
	private readonly int[] _rowPermutation;

	private int Rows => _rowPermutation.Length;
	private int Cols => _columnPermutation.Length;
	private int BlockSize => Rows * Cols;

	/// <param name="columnKey">Ключ для перестановки столбцов.</param>
	/// <param name="rowKey">Ключ для перестановки строк.</param>
	public DoubleTranspositionCipher (string columnKey, string rowKey)
	{
		if (string.IsNullOrEmpty(columnKey)) {
			throw new ArgumentException("Ключ столбцов не может быть пустым.", nameof(columnKey));
		}

		if (string.IsNullOrEmpty(rowKey)) {
			throw new ArgumentException("Ключ строк не может быть пустым.", nameof(rowKey));
		}

		_columnPermutation = GetPermutation(columnKey);
		_rowPermutation = GetPermutation(rowKey);
	}

	/// <summary>
	/// Шифрование методом двойной перестановки по ключу.
	/// </summary>
	/// <param name="plainText">Открытый текст.</param>
	public string Encrypt (string plainText)
	{
		if (string.IsNullOrWhiteSpace(plainText)) {
			throw new ArgumentNullException(nameof(plainText));
		}

		// Дополняем текст до целого числа блоков.
		int remainder = plainText.Length % BlockSize;
		if (remainder != 0) {
			plainText += new string(PaddingChar, BlockSize - remainder);
		}

		var result = new StringBuilder(plainText.Length);

		for (int offset = 0; offset < plainText.Length; offset += BlockSize) {
			char[,] table = new char[Rows, Cols];

			// 1. Запись блока по строкам.
			int index = offset;
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					table[r, c] = plainText[index++];
				}
			}

			// 2. Перестановка столбцов по columnKey.
			char[,] afterColumnPermutation = new char[Rows, Cols];
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					afterColumnPermutation[r, c] = table[r, _columnPermutation[c]];
				}
			}

			// 3. Перестановка строк по rowKey.
			char[,] afterRowPermutation = new char[Rows, Cols];
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					afterRowPermutation[r, c] = afterColumnPermutation[_rowPermutation[r], c];
				}
			}

			// 4. Считывание шифртекста по столбцам.
			for (int c = 0; c < Cols; c++) {
				for (int r = 0; r < Rows; r++) {
					result.Append(afterRowPermutation[r, c]);
				}
			}
		}

		return result.ToString();
	}

	/// <summary>
	/// Расшифрование методом двойной перестановки по ключу.
	/// </summary>
	/// <param name="cipherText">Шифртекст.</param>
	/// <param name="removePadding">Удалять ли символы дополнения в конце.</param>
	public string Decrypt (string cipherText, bool removePadding = true)
	{
		if (string.IsNullOrWhiteSpace(cipherText)) {
			throw new ArgumentNullException(nameof(cipherText));
		}

		if (cipherText.Length % BlockSize != 0) {
			throw new ArgumentException(
				$"Длина шифртекста ({cipherText.Length}) должна быть кратна размеру блока ({BlockSize}).",
				nameof(cipherText));
		}

		var result = new StringBuilder(cipherText.Length);

		for (int offset = 0; offset < cipherText.Length; offset += BlockSize) {
			char[,] table = new char[Rows, Cols];

			// 1. Запись шифртекста по столбцам.
			int index = offset;
			for (int c = 0; c < Cols; c++) {
				for (int r = 0; r < Rows; r++) {
					table[r, c] = cipherText[index++];
				}
			}

			// 2. Обратная перестановка строк.
			char[,] afterInverseRowPermutation = new char[Rows, Cols];
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					afterInverseRowPermutation[_rowPermutation[r], c] = table[r, c];
				}
			}

			// 3. Обратная перестановка столбцов.
			char[,] originalTable = new char[Rows, Cols];
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					originalTable[r, _columnPermutation[c]] = afterInverseRowPermutation[r, c];
				}
			}

			// 4. Считывание открытого текста по строкам.
			for (int r = 0; r < Rows; r++) {
				for (int c = 0; c < Cols; c++) {
					result.Append(originalTable[r, c]);
				}
			}
		}

		// TODO: возможно отказаться от флага removePadding
		return (removePadding)
			? result.ToString().TrimEnd(PaddingChar)
			: result.ToString();
	}

	/// <summary>
	/// Возвращает перестановку индексов по ключу.
	/// Например, для ключа "СКАНЕР" результат: [2,4,1,3,5,0].
	/// </summary>
	private static int[] GetPermutation (string key)
	{
		return key
			.Select((ch, index) => new { ch, index })
			.OrderBy(x => char.ToUpperInvariant(x.ch))
			.ThenBy(x => x.index)
			.Select(x => x.index)
			.ToArray();
	}
}