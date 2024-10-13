using System.CommandLine;
using System.Text;

namespace Vecerdi.License;

public static class Root {
    public static RootCommand GetCommand() {
        Option<string> spdxIdentifier = new(
            aliases: ["--spdx-identifier", "--license", "-i"],
            description: "The SPDX identifier of the license to generate.",
            getDefaultValue: () => License.MIT
        ) {
            IsRequired = false,
            Arity = ArgumentArity.ExactlyOne,
            ArgumentHelpName = "SPDX"
        };

        spdxIdentifier.AddValidator(result => {
            string? identifier = result.GetValueOrDefault<string>();
            if (identifier is null || !License.All.Contains(identifier)) {
                result.ErrorMessage = GetIdentifierErrorMessage(identifier);
            }
        });

        Option<FileInfo> output = new(
            aliases: ["--output", "-o"],
            description: "The output file path.",
            getDefaultValue: () => new FileInfo("./LICENSE")
        ) {
            IsRequired = false,
            Arity = ArgumentArity.ExactlyOne,
            ArgumentHelpName = "file"
        };

        Option<bool> silent = new(
            aliases: ["--silent", "-s"],
            description: "Suppress console output."
        ) {
            IsRequired = false,
            Arity = ArgumentArity.ZeroOrOne
        };

        Option<bool> acceptPlaceholders = new(
            aliases: ["--accept-placeholders", "-a"],
            description: "Accept default values for placeholders in the license text."
        ) {
            IsRequired = false,
            Arity = ArgumentArity.ZeroOrOne
        };

        RootCommand rootCommand = new() {
            Name = "license",
            Description = "Generates a license file based on the given SPDX identifier."
        };

        rootCommand.AddOption(spdxIdentifier);
        rootCommand.AddOption(output);
        rootCommand.AddOption(silent);
        rootCommand.AddOption(acceptPlaceholders);
        rootCommand.SetHandler(GenerateLicense, spdxIdentifier, output, silent, acceptPlaceholders);

        return rootCommand;
    }

    public static string GetLicenseText(string identifier) {
        return identifier switch {
            License.MIT => MIT.Text,
            License.ISC => ISC.Text,
            License.APACHE_2_0 => Apache2.Text,
            License.BSD_3_CLAUSE => BSD3Clause.Text,
            License.BSD_2_CLAUSE => BSD2Clause.Text,
            License.GPL_3_0 => GPL3.Text,
            License.GPL_2_0 => GPL2.Text,
            License.MPL_2_0 => MPL2.Text,
            License.UNLICENSE => Unlicense.Text,
            _ => throw new Exception($"Unknown SPDX identifier '{identifier}'")
        };
    }

    private static void GenerateLicense(string identifier, FileInfo output, bool silent, bool acceptPlaceholders) {
        string licenseText = GetLicenseText(identifier);
        if (!silent) Console.WriteLine($"Generating {identifier} license at '{output}'...");

        Placeholder[] placeholders = GetLicensePlaceholders(identifier);

        foreach (Placeholder placeholder in placeholders) {
            if (acceptPlaceholders) {
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

    private static string GetIdentifierErrorMessage(string? identifier) {
        StringBuilder sb = new();
        sb.AppendLine($"Unknown SPDX identifier '{identifier ?? "null"}'.\n");
        sb.AppendLine(List.GetAvailableIdentifiers());

        return sb.ToString();
    }

    private static Placeholder[] GetLicensePlaceholders(string identifier) {
        return identifier switch {
            License.MIT => MIT.Placeholders,
            License.ISC => ISC.Placeholders,
            License.APACHE_2_0 => Apache2.Placeholders,
            License.BSD_3_CLAUSE => BSD3Clause.Placeholders,
            License.BSD_2_CLAUSE => BSD2Clause.Placeholders,
            License.GPL_3_0 => GPL3.Placeholders,
            License.GPL_2_0 => GPL2.Placeholders,
            License.MPL_2_0 => MPL2.Placeholders,
            License.UNLICENSE => Unlicense.Placeholders,
            _ => throw new Exception($"Unknown SPDX identifier '{identifier}'")
        };
    }
}