namespace Lib.DBAccess.Helpers;

internal sealed class NoDatabaseRowReturnedException(string message) : Exception(message)
{
}
