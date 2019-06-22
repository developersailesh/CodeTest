namespace AppServer.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    ///  Owner Gender Male/Female
    /// </summary>
    public enum Gender
    {

        [EnumMember(Value = "male")]
        Male,

        [EnumMember(Value = "female")]
        Female
    }
}
