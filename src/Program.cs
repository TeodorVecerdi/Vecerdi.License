using System.CommandLine;
using Vecerdi.License;

RootCommand rootCommand = Root.GetCommand();
rootCommand.AddCommand(List.GetCommand());
return await rootCommand.InvokeAsync(args);