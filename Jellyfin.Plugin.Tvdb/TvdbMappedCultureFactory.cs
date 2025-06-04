using MediaBrowser.Model.Globalization;

namespace Jellyfin.Plugin.Tvdb
{
    /// <summary>
    /// Mapped culture specific to theTVDB.
    /// </summary>
    public static class TvdbMappedCultureFactory
    {
        /// <summary>
        /// Map 'French (Canada)' to 'French,' as TheTVDB doesn't have a variant for French and 'frc' isn't a valid ISO 639-2 code.
        /// </summary>
        /// <returns>CultureDto.</returns>
        public static CultureDto FrenchCanadaCulture()
        {
            string name = "French (Canada)";
            string twoLetterISOLanguageName = "fr-CA";
            string[] threeLetterISOLanguageNames = { "fra" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Portuguese (Brazil).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Brazil)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        internal static CultureDto PortugueseBrazilCulture()
        {
            string name = "Portuguese (Brazil)";
            string twoLetterISOLanguageName = "pt-BR";
            string[] threeLetterISOLanguageNames = { "pt" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// For <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Portugal)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        internal static CultureDto PortuguesePortugalCulture()
        {
            string name = "Portuguese (Portugal)";
            string twoLetterISOLanguageName = "pt-PT";
            string[] threeLetterISOLanguageNames = { "por" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Chinese (Taiwan).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Chinese (Taiwan)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        internal static CultureDto ChineseTaiwanCulture()
        {
            string name = "Chinese (Taiwan)";
            string twoLetterISOLanguageName = "zh-TW";
            string[] threeLetterISOLanguageNames = { "zhtw" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }
    }
}
