namespace is_lab1.net.LabExceptions;

public class BadKeyException (string msg, Exception? innerException = null) 
	: Exception(msg, innerException)
{ }
