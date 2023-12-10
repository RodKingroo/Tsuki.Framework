using System.Text;
using Tsuki.Graphics.Khronos.Enums;

namespace Tsuki.Graphics.Khronos;

/*public class KhronosVersion : IEquatable<KhronosVersion>, IComparable<KhronosVersion>
{
    public int Major { get; }
    public int Minor { get; }
    public int Revision { get; }
    public ClientApi Api { get; }
    public string Profile { get; }
    
    public KhronosVersion(int major, int minor, int revision, ClientApi api, string profile)
    {
        Major = major;
        Minor = minor;
        Revision = revision;
        Api = api;
        Profile = profile;
    }

    public KhronosVersion(int major, int minor, int revision, ClientApi api) : this(major, minor, revision, api, null)
    {
        
    }
    
    public KhronosVersion(int major, int minor, ClientApi api) : this(major, minor, 0, api)
    {
        
    }

    public virtual int VersionId
        => Major * 100 + Minor * 10;

    public int CompareTo(KhronosVersion? other)
    {
        if (ReferenceEquals(this, other))
            return 0;
        if (ReferenceEquals(null, other))
            return +1;

        if (Api != other.Api)
            throw new InvalidOperationException("different API version are not comparable");

        return Major.CompareTo(other.Major) != 0 ? Major.CompareTo(other.Major) :
            Minor.CompareTo(other.Minor) != 0 ? Minor.CompareTo(other.Minor) :
            Revision.CompareTo(other.Revision);
    }

    // public static KhronosVersion ParseFeature(string featureName)
    // {
    //     if (featureName == null) throw new ArgumentException(nameof(featureName));
    //     if (featureName == "GL_VERSION_ES_CM_1_0") return KhronosVersion(1, 0, 0, ClientApi.Gles);
    // }

    public static bool operator ==(KhronosVersion left, KhronosVersion right) => left.Equals(right);
    public static bool operator !=(KhronosVersion left, KhronosVersion right) => !left.Equals(right);

    public static bool operator >(KhronosVersion left, KhronosVersion right) => left.CompareTo(right) > 0;
    public static bool operator <(KhronosVersion left, KhronosVersion right) => left.CompareTo(right) < 0;
    public static bool operator >=(KhronosVersion left, KhronosVersion right) => left.CompareTo(right) >= 0;
    public static bool operator <=(KhronosVersion left, KhronosVersion right) => left.CompareTo(right) <= 0;

    public override string ToString()
    {
        return $"Version={Major}.{Minor} .{Revision} API={Api.ToString()} .{Profile}";
    }
    
    
}*/