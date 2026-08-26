# Code Conventions

Match the existing style rather than introducing new patterns.

- `using` directives go **inside** the namespace block (see `Generator.cs`).
- 4-space indentation, Allman braces, `_camelCase` for private backing fields.
- XML doc comments (`/// <summary>`) on all public types and members, including
  `<param>` and `<returns>` where applicable. Include `<example>` for params-based APIs.
- Helpers stay `internal` (e.g. `StringExtensions`).
- Prefer plain framework types and hand-rolled logic; the library has zero runtime
  dependencies. Keep it that way.
- Source files are UTF-8 with BOM and CRLF-tolerant (`core.autocrlf` is set in CI).
  Don't reformat or strip BOMs from files you aren't otherwise changing.
