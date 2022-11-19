namespace Vecerdi.License;

internal static class Program {
    /// <summary>
    /// Generates a license file based on the given license type.
    /// </summary>
    /// <param name="license">The license type</param>
    /// <param name="output">The output file path [default: "./LICENSE"]</param>
    public static void Main(License license = License.MIT, FileInfo? output = null) {
        output ??= new FileInfo("./LICENSE");
        Console.WriteLine($"Generating {license} license at '{output}'...");
        string licenseText = s_LicenseTexts[license];
        Placeholder[] placeholders = s_LicensePlaceholders[license];

        foreach (Placeholder placeholder in placeholders) {
            // Ask user for input
            Console.Write($"{placeholder.Name}");
            if (!string.IsNullOrEmpty(placeholder.SuggestedValue)) {
                Console.Write($" [{placeholder.SuggestedValue}]");
            }
            Console.Write(": ");
            string? input = Console.ReadLine();

            if (input == null) {
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

    private static readonly Dictionary<License, string> s_LicenseTexts = new() {
        [License.MIT] =
            """
        Copyright <YEAR> <COPYRIGHT HOLDER>

        Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
        """
    };

    private static readonly Dictionary<License, Placeholder[]> s_LicensePlaceholders = new() {
        [License.MIT] = new[] {
            new Placeholder("Year", "<YEAR>", DateTime.Now.Year.ToString()),
            new Placeholder("Copyright Holder", "<COPYRIGHT HOLDER>", "Teodor Vecerdi")
        }
    };

    public enum License {
        MIT,
    }

    private readonly record struct Placeholder(string Name, string Key, string? SuggestedValue);
}