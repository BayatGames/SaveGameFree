# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- feat: async save and load methods (`SaveAsync<T>()` and `LoadAsync<T>()`) for non-blocking file operations
- feat: multiple async method overloads matching synchronous API surface
- test: comprehensive test suite with 15+ tests in `SaveGameExtendedTests.cs`
- test: async operation tests (SaveAsync, LoadAsync)
- test: encryption/decryption tests with correct and incorrect passwords
- test: serializer tests for Binary, XML, and JSON formats
- test: complex object serialization tests
- test: PlayerPrefs storage mode tests
- test: error handling and edge case tests
- docs: async usage examples in README
- docs: encryption usage examples in README
- docs: enhanced XML documentation for `EncodePassword` property with security warnings
- docs: comprehensive inline code documentation
- docs: CHANGELOG.md following Keep a Changelog format
- docs: migration guide for breaking changes

### Changed

- **BREAKING**: refactor: replaced hardcoded default password with device/application-specific generation using `SystemInfo.deviceUniqueIdentifier` and `Application.identifier`
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
