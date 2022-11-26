using System.CommandLine;
using System.Text;

namespace Vecerdi.License;

public static class List {
    public static Command GetCommand() {
        Command command = new("list", "List supported SPDX license identifiers");
        command.SetHandler(() => Console.WriteLine(GetAvailableIdentifiers()));
        return command;
    }

    public static string GetAvailableIdentifiers() {
        StringBuilder sb = new();
        sb.AppendLine("Available SPDX identifiers:");
        foreach (string spdx in License.All) {
            sb.AppendLine($"    {spdx}");
        }

        return sb.ToString().TrimEnd();
    }
}