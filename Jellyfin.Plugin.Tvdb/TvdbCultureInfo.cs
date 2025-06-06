using System;
using System.Linq;
using Jellyfin.Extensions;
using Jellyfin.Plugin.Tvdb.Factories;
using MediaBrowser.Model.Globalization;

namespace Jellyfin.Plugin.Tvdb
{
    /// <summary>
    /// Tvdb culture info.
    /// </summary>
    internal static class TvdbCultureInfo
    {
        private static CultureDto[] _cultures = Array.Empty<CultureDto>();
        private static CultureDto[] _mappedcultures = Array.Empty<CultureDto>();
        private static CountryInfo[] _countries = Array.Empty<CountryInfo>();

        private static bool IsMapFrenchCanadaToFrench => TvdbPlugin.Instance?.Configuration.IsMapFrenchCanadaToFrench ?? false;

        internal static void SetCultures(CultureDto[] cultures)
        {
            _cultures = cultures;
        }

        /// <summary>
        /// List of special mapping between TheTvDB and Jellyfin.
        /// </summary>
        internal static void SetMappedCultures()
        {
            _mappedcultures = TvdbMappedCultureFactory.GenerateTvdbMappedCultureDto(IsMapFrenchCanadaToFrench);
        }

        internal static void SetCountries(CountryInfo[] countries)
        {
            _countries = countries;
        }

        /// <summary>
        /// Gets the cultureinfo for the given language.
        /// </summary>
        /// <param name="language">Language.</param>
        /// <returns>CultureInfo.</returns>
        internal static CultureDto? GetCultureInfo(string? language)
        {
            if (language == null)
            {
                return default;
            }

            foreach (var mappedCulture in _mappedcultures)
            {
                if (language.Equals(mappedCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
                    || language.Equals(mappedCulture.ThreeLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                {
                    return mappedCulture;
                }
            }

            foreach (var culture in _cultures)
            {
                if (language.Equals(culture.DisplayName, StringComparison.OrdinalIgnoreCase)
                    || language.Equals(culture.Name, StringComparison.OrdinalIgnoreCase)
                    || culture.ThreeLetterISOLanguageNames.Contains(language, StringComparison.OrdinalIgnoreCase)
                    || language.Equals(culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
                {
                    return culture;
                }
            }

            return default;
        }

        /// <summary>
        /// Gets the CountryInfo for the given country.
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
