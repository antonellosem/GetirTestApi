using System;
namespace NewPacg.Common.Entities
{
    /// <summary>
    /// Minimal representation of a user record. Only basic properties
    /// required for early refactoring steps are included.
    /// </summary>
    public class User
    {
        /// <summary>Identifier of the user.</summary>
        public int Id { get; set; }

        /// <summary>Display name.</summary>
        public string? Name { get; set; }
    }
}
