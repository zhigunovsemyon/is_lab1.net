namespace is_lab1.net.LabExceptions;

public class InvalidModeException (string msg, Exception? innerException = null)
	: Exception(msg, innerException)
{ }
