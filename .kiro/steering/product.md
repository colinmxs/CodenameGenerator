# Product

CodenameGenerator is a small .NET library (published on NuGet) that generates random
codenames / usernames from bundled word lists. Example output: `rambunctious arthropod`.

## Public API surface

Everything users touch lives in the `CodenameGenerator` namespace:

- `Generator` — the entry point. `Generate()`, `GenerateMany(int)`, `GenerateUnique(string[] reserved)`.
  Configurable via `Separator`, `Parts`, `Casing`, `EndsWith`, or the constructor
  (`new Generator(separator, casing, params WordBank[])`). Accepts an optional `seed`
  for reproducible output.
- `WordBank` — a type-safe pseudo-enum exposing static instances (`WordBank.Nouns`,
  `WordBank.Adjectives`, `WordBank.Cities`, ...) plus `WordBank.All`. Each wraps a
  `WordRepository`.
- `WordRepository` — public so callers can build custom banks over their own words.
- `Casing` — `CamelCase`, `PascalCase`, `UpperCase`, `LowerCase`.
- `Word` — enum of word categories; `WordBank` implicitly converts to `Word` and `string`.

Defaults: `Parts = [Adjectives, Nouns]`, `Separator = " "`, `Casing = LowerCase`, `EndsWith = ""`.

## Behaviour contracts (all covered by tests; don't regress them)

- Setters coerce `null` to a safe value (`""` / empty array).
- Casing uses invariant culture, so output never varies by locale.
- An unseeded `Generator` is thread-safe; `Generate()` works from a snapshot of the
  configuration so concurrent property changes can't tear a result. A seeded generator
  is single-threaded by nature.
- Empty `Parts` yields just the `EndsWith` suffix. An empty `WordBank` throws
  `InvalidOperationException` naming the bank.
- `GenerateUnique` is bounded (10,000 attempts) and throws rather than recursing.
- Word bank entries may contain spaces; the separator is applied between those
  sub-words too (`"Airline Traffic Controller"` -> `airline-traffic-controller`).

## Rules

- This is a published library: treat the public API as a contract. Do not rename or
  remove public members, change default behavior, or alter method signatures without
  flagging it as a breaking change.
- Keep `README.md` in sync when adding a WordBank, Casing, or configuration option.
