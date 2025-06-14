using System;
using Jellyfin.Plugin.Tvdb.Models;
using MediaBrowser.Model.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Jellyfin.Plugin.Tvdb
{
    /// <summary>
    /// Tvdb culture info.
    /// </summary>
    internal static class TvdbCultureInfo
    {
        private static CultureDto[] _cultures = Array.Empty<CultureDto>();
        private static TvdbCultureDto[] _mappedcultures = Array.Empty<TvdbCultureDto>();
        private static CountryInfo[] _countries = Array.Empty<CountryInfo>();

        internal static void SetCultures(CultureDto[] cultures)
        {
            _cultures = cultures;
        }

        /// <summary>
        /// Sets _mappedcultures.
        /// </summary>
        /// <param name="mappedcultures">Special mapping between TheTvDB and Jellyfin.</param>
        internal static void SetMappedCultures(TvdbCultureDto[] mappedcultures)
        {
            _mappedcultures = mappedcultures;
        }

        internal static void SetCountries(CountryInfo[] countries)
        {
            _countries = countries;
        }

        /// <summary>
        /// Gets the <see cref="CultureDto"/> for the given ISO 639-1 code.
        /// </summary>
        /// <param name="languageCode">ISO 639-1 code (jellyfin.)</param>
        /// <returns>Return the matching <see cref="CultureDto"/>, if found.</returns>
        internal static CultureDto? GetCultureDtoFromIso6391(string? languageCode)
        {
            if (languageCode == null)
            {
                return null;
            }

            foreach (var mappedCulture in _mappedcultures)
            {
                if (languageCode.Equals(mappedCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                {
                    return (CultureDto)mappedCulture;
                }
            }

            return GetCultureDto(languageCode, NullLogger.Instance) ?? default;
        }

        /// <summary>
        /// Gets the <see cref="CultureDto"/> for the given ISO 639-2(T) code.
        /// </summary>
        /// <param name="languageCode">ISO 639-2(T) code (TVDB.)</param>
        /// <param name="logger">ILogger.</param>
        /// <returns>Return the matching <see cref="CultureDto"/>, if found.</returns>
        internal static CultureDto? GetCultureDtoFromIso6392(string? languageCode, ILogger logger)
        {
            if (languageCode == null)
            {
                return null;
            }

            foreach (var mappedCulture in _mappedcultures)
            {
                if (languageCode.Equals(mappedCulture.ThreeLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                {
                    // Ignore a matching result if we want the one from jellyfin.
                    if (!mappedCulture.JellyfinToTvdbOnly)
                    {
                        logger.LogDebug("Found TVDB special mapping for {LanguageCode}: {MappedCultureName}", languageCode, mappedCulture.Name);
                        return (CultureDto)mappedCulture;
                    }
                    else
                    {
                        logger.LogDebug(
                            "Skip: found TVDB special mapping for {LanguageCode}: {MappedCultureName}. [Reason: one way mapping]",
                            languageCode,
                            mappedCulture.Name);
                        logger.LogTrace("TvdbCultureDto object dump: {MappedCultureToString}", mappedCulture.ToString());
                    }
                }
            }

            return GetCultureDto(languageCode, logger) ?? default;
        }

        /// <summary>
        /// Gets the <see cref="CultureDto"/> from jellyfin for the given language.
        /// </summary>
        /// <param name="languageCode">Language in ISO 639-1 or ISO 639-2(T) form.</param>
        /// <param name="logger">ILogger.</param>
        /// <returns>Return the matching <see cref="CultureDto"/>, if found.</returns>
        private static CultureDto? GetCultureDto(string languageCode, ILogger logger)
        {
            foreach (var culture in _cultures)
            {
                if (languageCode.Equals(culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
                    || languageCode.Equals(culture.ThreeLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogDebug("Found jellyfin mapping for {LanguageCode}: {CultureName}", languageCode, culture.Name);
                    logger.LogTrace("CultureDto object dump: {MappedCultureToString}", culture.ToString());

                    return culture;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the <see cref="CountryInfo"/> for the given country.
        /// </summary>
        /// <param name="country"> Country.</param>
        /// <returns>CountryInfo.</returns>
        internal static CountryInfo? GetCountryInfo(string country)
        {
            foreach (var countryInfo in _countries)
            {
                if (country.Equals(countryInfo.Name, StringComparison.OrdinalIgnoreCase)
                    || country.Equals(countryInfo.TwoLetterISORegionName, StringComparison.OrdinalIgnoreCase)
                    || country.Equals(countryInfo.ThreeLetterISORegionName, StringComparison.OrdinalIgnoreCase))
                {
                    return countryInfo;
                }
            }

            return default;
        }
    }
}
