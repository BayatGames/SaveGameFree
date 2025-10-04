# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- feat: async save and load methods (`SaveAsync<T>()` and `LoadAsync<T>()`) for non-blocking file operations
- feat: multiple async method overloads matching synchronous API surface
- build: Unity Package Manager (UPM) compliant structure with proper package layout
- build: package.json manifest at package root following UPM conventions
- build: semantic versioning with VERSION file
- build: .editorconfig for consistent code style across editors
- build: .gitattributes for Unity-specific git configuration
- build: .npmignore to exclude unnecessary files from package distribution
- test: comprehensive test suite with 15+ tests in `SaveGameExtendedTests.cs`
- test: async operation tests (SaveAsync, LoadAsync)
- test: encryption/decryption tests with correct and incorrect passwords
- test: serializer tests for Binary, XML, and JSON formats
- test: complex object serialization tests
- test: PlayerPrefs storage mode tests
- test: error handling and edge case tests
- test: tests now located in package Tests/Editor/ following UPM conventions
- docs: async usage examples in README
- docs: encryption usage examples in README
- docs: enhanced XML documentation for `EncodePassword` property with security warnings
- docs: comprehensive inline code documentation
- docs: CHANGELOG.md following Keep a Changelog format
- docs: CONTRIBUTING.md with Conventional Commits guidelines and C# style guide
- docs: migration guide for breaking changes
- docs: README_PROJECT.md explaining UPM package structure

### Changed
- **BREAKING**: build: restructured package to follow Unity Package Manager (UPM) conventions
- **BREAKING**: build: renamed `Scripts/` folder to `Runtime/` following UPM standards
- **BREAKING**: build: renamed `Examples/` folder to `Samples~/` (tilde suffix for Unity to ignore)
- **BREAKING**: build: renamed `Documentation/` folder to `Documentation~/` (tilde suffix for Unity to ignore)
- **BREAKING**: build: assembly definitions renamed to `Bayat.Unity.SaveGameFree.asmdef` and `Bayat.Unity.SaveGameFree.Editor.Tests.asmdef` following PascalCase convention
- **BREAKING**: refactor: namespace changed from `BayatGames.SaveGameFree` to `Bayat.Unity.SaveGameFree` for all namespaces (Runtime, Serializers, Encoders, Types, Examples, Tests)
- **BREAKING**: build: package identifier updated from `io.bayat.games.savegamefree` to `io.bayat.unity.savegamefree` to indicate Unity platform specificity
- **BREAKING**: build: test folder moved from `Editor/Tests/` to `Tests/Editor/` following official Unity package layout conventions
- **BREAKING**: refactor: replaced hardcoded default password with device/application-specific generation using `SystemInfo.deviceUniqueIdentifier` and `Application.identifier`
- build: package.json now located at package root (`Assets/BayatGames/SaveGameFree/`)
- build: README.md, CHANGELOG.md, and LICENSE now located at package root
- build: tests moved from `Assets/Tests/` to `Assets/BayatGames/SaveGameFree/Tests/Editor/`
- build: assembly definitions updated to follow UPM naming conventions
- perf: refactored all stream operations to use `using` statements for automatic resource disposal
- perf: replaced `String.Format()` with string interpolation throughout codebase
- perf: added 1024-byte buffering to StreamWriter/StreamReader operations
- perf: reduced memory allocations through improved resource management
- fix: added `leaveOpen` parameter to StreamWriter/StreamReader to prevent premature stream closure
- fix: improved error handling with comprehensive try-catch blocks
- fix: enhanced error messages to include identifier and exception details
- refactor: conditional error logging based on `LogError` property
- refactor: improved exception propagation from serializers
- refactor: updated `SaveGameJsonSerializer` with proper using statements and error handling
- refactor: added explicit flush calls in serializers for data integrity
- refactor: replaced `Debug.LogWarningFormat` with `Debug.LogWarning` and string interpolation
- fix: corrected MemoryStream writability flag in Load methods

### Removed

- **BREAKING**: removed deprecated `IOSupported()` method (obsolete since earlier versions)

### Fixed
- Fixed potential resource leaks from non-disposed streams
- Fixed potential data corruption from streams closed prematurely
- Improved warning message formatting (replaced `LogWarningFormat` with `LogWarning`)
- Fixed MemoryStream writability flag in Load methods

## Compatibility

- **Unity Version**: Unity 6.0+ (tested on 6000.2.6f2)
- **C# Version**: C# 8.0+ (uses null-coalescing assignment operator `??=`)
- **.NET Version**: .NET Framework 4.7.1+

## Migration Guide

### From Previous Versions

#### Namespace Change
The package namespace has been updated to follow Unity naming conventions:

**Before:**
```csharp
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;
using BayatGames.SaveGameFree.Types;
```

**After:**
```csharp
using Bayat.Unity.SaveGameFree;
using Bayat.Unity.SaveGameFree.Serializers;
using Bayat.Unity.SaveGameFree.Encoders;
using Bayat.Unity.SaveGameFree.Types;
```

**Migration Steps:**
1. Update all `using BayatGames.SaveGameFree` statements to `using Bayat.Unity.SaveGameFree`
2. If you have custom serializers/encoders, update their namespace declarations
3. Recompile your project - Unity will show errors for any missed references

#### Package Identifier
The package identifier in `package.json` has changed:
- **Old**: `io.bayat.games.savegamefree`
- **New**: `io.bayat.unity.savegamefree`

This change indicates the package is Unity-specific.

#### Assembly Definitions
Assembly definition files have been renamed:
- **Runtime**: `Bayat.Unity.SaveGameFree.asmdef` (was `io.bayat.savegamefree.asmdef`)
- **Tests**: `Bayat.Unity.SaveGameFree.Editor.Tests.asmdef`

If your project references these assembly definitions, update the references to the new names.

#### Folder Structure
The package now follows Unity's official UPM package layout:
- `Scripts/` → `Runtime/`
- `Examples/` → `Samples~/`
- `Documentation/` → `Documentation~/`
- `Editor/Tests/` → `Tests/Editor/`

These changes should not affect your usage unless you have custom scripts that reference the old folder paths.

#### Encryption Password
If you were relying on the default encryption password, note that it has changed:

**Before:**
```csharp
// Default password was "h@e#ll$o%^" for all installations
SaveGame.Save("data", myData, encode: true);
```

**After:**
```csharp
// Default password is now unique per device/application
// For production, ALWAYS set your own password:
SaveGame.EncodePassword = "YourSecurePassword123!";
SaveGame.Save("data", myData, encode: true);
```

#### IOSupported() Method
The deprecated `IOSupported()` method has been removed. This method was checking for platform file I/O support but is no longer needed as the library handles platform differences internally.

**Before:**
```csharp
if (SaveGame.IOSupported())
{
    SaveGame.Save("data", myData);
}
```

**After:**
```csharp
// Simply use Save/Load directly
// The library handles platform compatibility automatically
SaveGame.Save("data", myData);
```

#### Async Methods
New async methods are available for better performance:

**Synchronous (still supported):**
```csharp
void SaveData()
{
    SaveGame.Save("data", myData);
}
```

**Asynchronous (recommended for large files):**
```csharp
async Task SaveDataAsync()
{
    await SaveGame.SaveAsync("data", myData);
}
```

## Testing

Run the test suite to verify your installation:

1. Open Unity Test Runner (Window > General > Test Runner)
2. Run all tests in EditMode
3. Verify all tests pass

New tests cover:
- Basic save/load operations
- Async save/load operations
- Encryption with correct/incorrect passwords
- Multiple serializer types
- Complex object serialization
- PlayerPrefs storage mode
- Error handling scenarios

## Performance Notes

- Async methods recommended for files > 100KB
- Using statements ensure proper resource cleanup
- Stream buffering improves I/O performance
- String interpolation reduces string allocation overhead

## Security Notes

- Default encryption password is now device-specific (more secure than hardcoded)
- **IMPORTANT**: Always set custom password for production: `SaveGame.EncodePassword = "YourPassword";`
- Encryption uses existing encoder infrastructure (consider upgrading to stronger encryption for sensitive data)

## Contributors

Thank you to all contributors who helped improve Save Game Free!

---

For more information, visit: http://docs.bayat.io/savegamefree
