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


## Installation

```bash
dotnet tool install -g Vecerdi.License
```

## Usage

```bash
$ license --help

Description:
  Generates a license file based on the given license type.

Usage:
  license [options]

Options:
  --license <license>  The license type (SPDX short identifier; e.g., GPL-3.0) [default: MIT]
  --output <output>    The output file path [default: "./LICENSE"]
  --silent             Whether to suppress console output [default: False]
  --skip-placeholders  Whether to skip placeholder replacement and accept defaults [default: False]
  --version            Show version information
  -?, -h, --help       Show help and usage information
```

### Example
```bash
license --license MIT --output LICENSE.md
```
