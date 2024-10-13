using System.CommandLine;

namespace Vecerdi.License;

public static class Print {
    public static Command GetCommand() {
        Command command = new("print", "Print the source of a given SPDX license identifier");

        Argument<string> spdxIdentifier = new(
            name: "spdx-identifier",
            description: "The SPDX identifier of the license to print."
        );

        command.AddArgument(spdxIdentifier);
        command.SetHandler(identifier => {
            if (!License.All.Contains(identifier)) {
                Console.WriteLine($"Unknown SPDX identifier '{identifier}'.");
                return;
            }

            var licenseText = Root.GetLicenseText(identifier);
            Console.WriteLine(licenseText);
        }, spdxIdentifier);

        return command;
    }
}
