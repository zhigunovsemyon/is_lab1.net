namespace is_lab1.net.LabExceptions;

public class BadFileException (string msg, Exception? innerException = null)
	: Exception(msg, innerException)
{ }
