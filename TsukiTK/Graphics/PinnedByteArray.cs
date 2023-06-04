using System.Runtime.InteropServices;

namespace Tsuki.Framework.Graphics;

public class PinnedByteArray : IDisposable
{
    private bool isValid;

    private byte[]? Value;
	private GCHandle hValue;
	private nint pValue;

    public static PinnedByteArray nullv = new PinnedByteArray();

    private PinnedByteArray() => isValid = false;

	~PinnedByteArray() => Free();

	public PinnedByteArray(byte[] value)
	{
		Value = value;
		hValue = GCHandle.Alloc(Value, GCHandleType.Pinned);
		pValue = hValue.AddrOfPinnedObject();

		isValid = true;
	}

	public static implicit operator nint(PinnedByteArray p)
	{
		if (p != null) return p.pValue;

		return nint.Zero;
		
	}

	public static implicit operator GCHandle( PinnedByteArray p )
		=> p.hValue;

	public static implicit operator byte[]? (PinnedByteArray p)
	{
		if (p == null) return null;

		return p.Value;
	}

    public void Free()
    {
        if(isValid)
        {
            isValid = false;
			Value = null;
			hValue.Free();
			pValue = IntPtr.Zero;
        }
    }

    public void Dispose() => Free();
}