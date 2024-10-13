# Vecerdi.License

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Vecerdi.License)](https://www.nuget.org/packages/Vecerdi.License/)

Generate license files for your projects.

Available SPDX identifiers:

- MIT
- ISC
- Apache-2.0
- BSD-3-Clause
- BSD-2-Clause
- GPL-3.0-only
- GPL-2.0
- MPL-2.0
- Unlicense
- NON-AI-MIT
- NON-AI-APACHE2


## Installation

```bash
dotnet tool install -g Vecerdi.License
```

## Usage

```bash
$ license --help

Description:
  Generates a license file based on the given SPDX identifier.

Usage:
  license [command] [options]

Options:
  -i, --license, --spdx-identifier <SPDX>  The SPDX identifier of the license to generate. [default: NON-AI-MIT]
  -o, --output <file>                      The output file path. [default: ./LICENSE]
  -s, --silent                             Suppress console output.
  -a, --accept-placeholders                Accept default values for placeholders in the license text.
  --version                                Show version information
  -?, -h, --help                           Show help and usage information

Commands:
  list                     List supported SPDX license identifiers
  print <spdx-identifier>  Print the source of a given SPDX license identifier
```

### Example
```bash
license --license NON-AI-MIT --output LICENSE
```
