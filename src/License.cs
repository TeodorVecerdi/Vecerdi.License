namespace Vecerdi.License;

internal static class License {
    public const string MIT = "MIT";
    public const string ISC = "ISC";
    public const string APACHE_2_0 = "Apache-2.0";
    public const string BSD_3_CLAUSE = "BSD-3-Clause";
    public const string BSD_2_CLAUSE = "BSD-2-Clause";
    public const string GPL_3_0 = "GPL-3.0-only";
    public const string GPL_2_0 = "GPL-2.0";
    public const string MPL_2_0 = "MPL-2.0";
    public const string UNLICENSE = "Unlicense";

    public static readonly HashSet<string> All = new() {
        MIT,
        APACHE_2_0,
        BSD_3_CLAUSE,
        BSD_2_CLAUSE,
        GPL_3_0,
        GPL_2_0,
        MPL_2_0,
        UNLICENSE
    };
}