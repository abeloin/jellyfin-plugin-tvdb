using System.Collections.Generic;
using MediaBrowser.Model.Globalization;

namespace Jellyfin.Plugin.Tvdb.Models
{
    /// <summary>
    /// TvdbCultureDto class.
    /// </summary>
    public class TvdbCultureDto : CultureDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvdbCultureDto"/> class.
        /// </summary>
        /// <param name="name">Language Name in English.</param>
        /// <param name="displayName">Language Name in native language.</param>
        /// <param name="twoLetterISOLanguageName">Jellyfin internal language code(either ISO 639-1, or ISO 639-1 with ISO 3166-1 alpha-2.) </param>
        /// <param name="threeLetterISOLanguageNames">Should be an ISO 639-2(T) terminological code that match the desired TVDB code.</param>
        /// <param name="jellyfinToTvdbOnly">If true, will not be used when looking for a jellyfin code from a TVDB code.</param>
        public TvdbCultureDto(string name, string displayName, string twoLetterISOLanguageName, IReadOnlyList<string> threeLetterISOLanguageNames, bool jellyfinToTvdbOnly) : base(name, displayName, twoLetterISOLanguageName, threeLetterISOLanguageNames)
        {
            JellyfinToTvdbOnly = jellyfinToTvdbOnly;
        }

        /// <summary>
        /// Gets a value indicating whether we should use this entry when looking for a jellyfin code from a TVDB code.
        /// </summary>
        public bool JellyfinToTvdbOnly { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>Returns a string that represents the current object state.</returns>
        public override string ToString()
        {
            return $"Name: {Name}, DisplayName: {DisplayName}, TwoLetterISO: {TwoLetterISOLanguageName}, ThreeLetterISO: [{string.Join(", ", ThreeLetterISOLanguageNames)}], JellyfinToTvdbOnly: {JellyfinToTvdbOnly}";
        }
    }
}
