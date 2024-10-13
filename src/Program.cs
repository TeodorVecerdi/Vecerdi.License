using System.CommandLine;
using Vecerdi.License;

var rootCommand = Root.GetCommand();
rootCommand.AddCommand(List.GetCommand());
rootCommand.AddCommand(Print.GetCommand());

return await rootCommand.InvokeAsync(args);