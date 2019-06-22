namespace AppServer.Models
{

    using System.Runtime.Serialization;

    /// <summary>
    /// Type of Pet Animal
    /// </summary>
    public enum Type
    {

        [EnumMember(Value = "Cat")]
        Cat,

        [EnumMember(Value = "Dog")]
        Dog,

        [EnumMember(Value = "Fish")]
        Fish
    }
}
