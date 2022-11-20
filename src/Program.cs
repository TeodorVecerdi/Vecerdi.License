namespace Vecerdi.License;

internal static class Program {
    /// <summary>
    /// Generates a license file based on the given license type.
    /// </summary>
    /// <param name="license">The license type (SPDX short identifier; e.g., GPL-3.0)</param>
    /// <param name="output">The output file path [default: "./LICENSE"]</param>
    /// <param name="silent">Whether to suppress console output</param>
    /// <param name="skipPlaceholders">Whether to skip placeholder replacement and accept defaults</param>
    public static void Main(string license = License.MIT, FileInfo? output = null, bool silent = false, bool skipPlaceholders = false) {
        if (!TryGetLicenseText(license, out string licenseText)) {
            DisplayLicensesAndExit(license);
        }

        output ??= new FileInfo("./LICENSE");
        if (!silent) Console.WriteLine($"Generating {license} license at '{output}'...");

        Placeholder[] placeholders = GetLicensePlaceholders(license);

        foreach (Placeholder placeholder in placeholders) {
            if (skipPlaceholders) {
                licenseText = licenseText.Replace(placeholder.Key, placeholder.SuggestedValue);
                continue;
            }

            // Ask user for input
            if(!silent) Console.Write($"{placeholder.Name}");
            if (!string.IsNullOrEmpty(placeholder.SuggestedValue)) {
                if(!silent) Console.Write($" [{placeholder.SuggestedValue}]");
            }
            if(!silent) Console.Write(": ");

            if (Console.ReadLine() is not { } input) {
                return;
            }

            string? value = string.IsNullOrWhiteSpace(input) ? placeholder.SuggestedValue : input;
            if (string.IsNullOrEmpty(value)) {
                throw new Exception($"No value provided for '{placeholder.Name}'");
            }

            licenseText = licenseText.Replace(placeholder.Key, value);
        }

        // Write license file
        File.WriteAllText(output.FullName, licenseText);
    }

    private static void DisplayLicensesAndExit(string license) {
        Console.Error.WriteLine($"Unknown SPDX short identifier '{license}'.\n");
        Console.Error.WriteLine("Available SPDX short identifiers:");
        foreach (string spdx in License.All) {
            Console.Error.WriteLine($"    {spdx}");
        }

        Environment.Exit(1);
    }

    private static bool TryGetLicenseText(string license, out string licenseText) {
        string? text = license switch {
            License.MIT => MIT.Text,
            License.ISC => ISC.Text,
            License.APACHE_2_0 => Apache2.Text,
            License.BSD_3_CLAUSE => BSD3Clause.Text,
            License.BSD_2_CLAUSE => BSD2Clause.Text,
            License.GPL_3_0 => GPL3.Text,
            License.GPL_2_0 => GPL2.Text,
            License.MPL_2_0 => MPL2.Text,
            License.UNLICENSE => Unlicense.Text,
            _ => null
        };

        if (text == null) {
            licenseText = string.Empty;
            return false;
        }

        licenseText = text;
        return true;
    }

    private static Placeholder[] GetLicensePlaceholders(string license) {
        return license switch {
            License.MIT => MIT.Placeholders,
            License.ISC => ISC.Placeholders,
            License.APACHE_2_0 => Apache2.Placeholders,
            License.BSD_3_CLAUSE => BSD3Clause.Placeholders,
            License.BSD_2_CLAUSE => BSD2Clause.Placeholders,
            License.GPL_3_0 => GPL3.Placeholders,
            License.GPL_2_0 => GPL2.Placeholders,
            License.MPL_2_0 => MPL2.Placeholders,
            License.UNLICENSE => Unlicense.Placeholders,
            _ => throw new Exception($"Unknown license type '{license}'")
        };
    }

    private static class License {
        public const string MIT = "MIT";
        public const string ISC = "ISC";
        public const string APACHE_2_0 = "Apache-2.0";
        public const string BSD_3_CLAUSE = "BSD-3-Clause";
        public const string BSD_2_CLAUSE = "BSD-2-Clause";
        public const string GPL_3_0 = "GPL-3.0-only";
        public const string GPL_2_0 = "GPL-2.0";
        public const string MPL_2_0 = "MPL-2.0";
        public const string UNLICENSE = "Unlicense";

        public static readonly string[] All = {
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
}