namespace Tsuki.Framework.Context;

public interface IBindingsContext
{
    nint GetProcAddress(string procName);
}