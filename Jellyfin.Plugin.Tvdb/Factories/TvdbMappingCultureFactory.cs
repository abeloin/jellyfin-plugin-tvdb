using System.Collections.Generic;
using Jellyfin.Plugin.Tvdb.Models;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.Tvdb.Factories
{
    /// <summary>
    /// TvdbMappingCultureFactory class.
    /// </summary>
    public static class TvdbMappingCultureFactory
    {
        /// <summary>
        /// Map 'French (Canada)' to 'French,' as TheTVDB doesn't have a variant for French and 'frc' isn't a valid ISO 639-2 code for French.
        /// </summary>
        /// <returns>TvdbCultureDto.</returns>
        private static TvdbCultureDto FrenchCanadaCulture()
        {
            var name = "French (Canada)";
            var displayName = "Français (Canada)";
            var twoLetterISOLanguageName = "fr-CA";
            string[] threeLetterISOLanguageNames = { "fra" };
            bool jellyfinToTvdbOnly = true;
            return new TvdbCultureDto(name: name, displayName: displayName, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames, jellyfinToTvdbOnly: jellyfinToTvdbOnly);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Portuguese (Brazil).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Brazil)'.
        /// </summary>
        /// <returns>TvdbCultureDto.</returns>
        private static TvdbCultureDto PortugueseBrazilCulture()
        {
            var name = "Portuguese (Brazil)";
            var displayName = "Português (Brasil)";
            var twoLetterISOLanguageName = "pt-BR";
            string[] threeLetterISOLanguageNames = { "pt" };
            bool jellyfinToTvdbOnly = false;
            return new TvdbCultureDto(name: name, displayName: displayName, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames, jellyfinToTvdbOnly: jellyfinToTvdbOnly);
        }

        /// <summary>
        /// For <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Portuguese (Portugal)'.
        /// </summary>
        /// <returns>TvdbCultureDto.</returns>
        private static TvdbCultureDto PortuguesePortugalCulture()
        {
            var name = "Portuguese (Portugal)";
            var displayName = "Português (Portugal)";
            var twoLetterISOLanguageName = "pt-PT";
            string[] threeLetterISOLanguageNames = { "por" };
            bool jellyfinToTvdbOnly = false;
            return new TvdbCultureDto(name: name, displayName: displayName, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames, jellyfinToTvdbOnly: jellyfinToTvdbOnly);
        }

        /// <summary>
        /// TheTVDB uses a different ISO 639-2 code for Chinese (Taiwan).
        /// Also for <see cref="TvdbSdkExtensions.CreateRemoteImageInfo"/> to map back to 'Chinese (Taiwan)'.
        /// </summary>
        /// <returns>TvdbCultureDto.</returns>
        private static TvdbCultureDto ChineseTaiwanCulture()
        {
            var name = "Chinese (Taiwan)";
            var displayName = "漢語 (繁體字)";
            var twoLetterISOLanguageName = "zh-TW";
            string[] threeLetterISOLanguageNames = { "zhtw" };
            bool jellyfinToTvdbOnly = false;
            return new TvdbCultureDto(name: name, displayName: displayName, twoLetterISOLanguageName: twoLetterISOLanguageName, threeLetterISOLanguageNames: threeLetterISOLanguageNames, jellyfinToTvdbOnly: jellyfinToTvdbOnly);
        }

        /// <summary>
        /// Generate TvdbCultureDtos for special mapping between Jellyfin and theTVDB.
        /// </summary>
        /// <param name="isMapFrenchCanadaToFrench"> Map French(Canada) to French when searching.</param>
        /// <param name="logger">Instance of the <see cref="ILogger{TvdbClientManager}"/> interface.</param>
        /// <returns>IEnumerable TvdbCultureDto.</returns>
        public static IEnumerable<TvdbCultureDto> GenerateTvdbMapping(bool isMapFrenchCanadaToFrench, ILogger logger)
        {
            List<TvdbCultureDto> mappedcultures = new List<TvdbCultureDto>();

            mappedcultures.Add(TvdbMappingCultureFactory.PortuguesePortugalCulture());
            mappedcultures.Add(TvdbMappingCultureFactory.PortugueseBrazilCulture());
            mappedcultures.Add(TvdbMappingCultureFactory.ChineseTaiwanCulture());

            if (isMapFrenchCanadaToFrench)
            {
                logger.LogDebug("Mapping French(Canada) to French option enable");
                mappedcultures.Add(TvdbMappingCultureFactory.FrenchCanadaCulture());
            }

            return mappedcultures;
        }
    }
}
