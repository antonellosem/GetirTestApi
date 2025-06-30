using System;
namespace NewPacg.Data.Models
{
    /// <summary>
    /// Configuration options used to initialise database connections.
    /// These settings will be wired to real providers once the
    /// refactoring introduces persistence layers.
    /// </summary>
    public class DatabaseOptions
    {
        /// <summary>Connection string to the underlying datastore.</summary>
        public string? ConnectionString { get; set; }
    }
}
