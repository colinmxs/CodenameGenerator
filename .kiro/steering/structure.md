# Structure

```
CodenameGenerator.sln
Dockerfile                       # build/test/pack; the only supported build path here
src/CodenameGenerator/           # the library (packable)
  Generator.cs                   # generation logic
  WordBank.cs                    # static WordBank instances + WordBank.All
  WordData.cs                    # internal loader for the embedded word lists
  Word.cs / Casing.cs            # enums
  StringExtensions.cs            # internal FirstCharToUpper helper
  WordRepos/
    WordRepository.cs            # lazy word source; public ctor takes string[]
    *Repository.cs               # one tiny class per list, ~15 lines each
  WordData/*.txt                 # the actual words, one per line, UTF-8, embedded
tests/CodenameGenerator.Tests/   # MSTest, net8.0
  GeneratorTests.cs
  WordBankTests.cs
  TestWordBank.cs                # two-word banks + CountingRepository test double
appveyor.yml                     # CI: build, test, push to NuGet on master
```

## Rules

- Word lists live in `WordData/*.txt`, not in code. They are `EmbeddedResource` items
  named `CodenameGenerator.WordData.<Bank>.txt` and loaded by `WordData.Load(name)`.
  Some are large (`Nouns` 82k words, `LastNames` 89k, `Cities` 48k); don't read them
  in full, use targeted greps.
- Adding a word list means four coordinated changes: a `WordData/<Name>.txt` file, a
  `*Repository` class passing `() => WordData.Load("<Name>")` to the base ctor, a
  `Word` enum member, and a `static readonly WordBank` field in `WordBank.cs` (also add
  it to `WordBank.All`, declared last so the other static fields are initialized first).
- Word list totals are asserted in tests (`298738` words across 17 banks). Editing
  word data means updating that assertion deliberately, not incidentally.
- Tests should use `TestWordBank` (two-word banks) when asserting on specific output.
  Reserve the real banks for content spot-checks.
