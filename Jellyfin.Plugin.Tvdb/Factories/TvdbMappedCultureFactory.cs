using System.Collections.Generic;
using MediaBrowser.Model.Globalization;

namespace Jellyfin.Plugin.Tvdb.Factories
{
    /// <summary>
    /// Mapped culture specific to theTVDB.
    /// </summary>
    public static class TvdbMappedCultureFactory
    {
        /// <summary>
        /// Map 'French (Canada)' to 'French,' as TheTVDB doesn't have a variant for French and 'frc' isn't a valid ISO 639-2 code for French.
        /// </summary>
        /// <returns>CultureDto.</returns>
        private static CultureDto FrenchCanadaCulture()
        {
            var name = "Français (Canada)";
            var twoLetterISOLanguageName = "fr-CA";
            string[] threeLetterISOLanguageNames = { "fra" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Portuguese (Brazil).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Brazil)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        private static CultureDto PortugueseBrazilCulture()
        {
            var name = "Português (Brasil)";
            var twoLetterISOLanguageName = "pt-BR";
            string[] threeLetterISOLanguageNames = { "pt" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// For <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Portugal)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        private static CultureDto PortuguesePortugalCulture()
        {
            var name = "Português (Portugal)";
            var twoLetterISOLanguageName = "pt-PT";
            string[] threeLetterISOLanguageNames = { "por" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Chinese (Taiwan).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Chinese (Taiwan)'.
        /// </summary>
        /// <returns>CultureDto.</returns>
        private static CultureDto ChineseTaiwanCulture()
        {
            var name = "漢語 (繁體字)";
            var twoLetterISOLanguageName = "zh-TW";
            string[] threeLetterISOLanguageNames = { "zhtw" };
            return new CultureDto(name: name, displayName: name, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames);
        }

        /// <summary>
        /// Generate an array of CultureDto about the mapping between Jellyfin and theTVDB special cases.
        /// </summary>
        /// <param name="isMapFrenchCanadaToFrench"> Map French(Canada) to French when searching.</param>
        /// <returns>Overriden CultureDto array.</returns>
        public static CultureDto[] GenerateTvdbMappedCultureDto(bool isMapFrenchCanadaToFrench)
        {
            List<CultureDto> mappedcultures = new List<CultureDto>();

            mappedcultures.Add(TvdbMappedCultureFactory.PortuguesePortugalCulture());
            mappedcultures.Add(TvdbMappedCultureFactory.PortugueseBrazilCulture());
            mappedcultures.Add(TvdbMappedCultureFactory.ChineseTaiwanCulture());

            if (isMapFrenchCanadaToFrench)
            {
                mappedcultures.Add(TvdbMappedCultureFactory.FrenchCanadaCulture());
            }

            return mappedcultures.ToArray();
        }
    }
}
