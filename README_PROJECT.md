# Save Game Free - Project Structure

This repository contains the Save Game Free Unity package following Unity Package Manager (UPM) conventions.

## Package Location

The actual package is located at:
```
Assets/BayatGames/SaveGameFree/
```

## UPM Package Structure

The package follows Unity's recommended layout:

```
Assets/BayatGames/SaveGameFree/
├── package.json              # Package manifest
├── README.md                 # Package documentation
├── CHANGELOG.md              # Version history
├── LICENSE                   # MIT License
├── Runtime/                  # Runtime code
│   ├── io.bayat.savegamefree.asmdef
│   ├── SaveGame.cs
│   ├── SaveGameWeb.cs
│   ├── SaveGameAuto.cs
│   ├── SaveGamePath.cs
│   ├── Serializers/
│   ├── Encoders/
│   └── Types/
├── Editor/                   # Editor-only code
│   └── Tests/
├── Tests/                    # Test assemblies
│   └── Editor/
│       ├── io.bayat.savegamefree.Editor.Tests.asmdef
│       ├── SaveGameTests.cs
│       └── SaveGameExtendedTests.cs
├── Samples~/                 # Package samples (ignored by Unity)
├── Documentation~/           # Documentation files (ignored by Unity)
├── Plugins/                  # Third-party plugins
└── Web/                      # WebGL-specific resources
```

## Installation

### Via Git URL (Recommended for UPM)

1. Open Unity Package Manager (Window > Package Manager)
2. Click the + button
3. Select "Add package from git URL"
4. Enter: `https://github.com/BayatGames/SaveGameFree.git?path=/Assets/BayatGames/SaveGameFree`

### Via Unity Asset Store

Download from the [Unity Asset Store](https://assetstore.unity.com/packages/tools/input-management/save-game-free-gold-update-81519)

### Manual Installation

1. Clone this repository
2. Copy the `Assets/BayatGames/SaveGameFree` folder to your project's `Packages` directory
3. Unity will automatically detect and import the package

## Development

### Project Files

- **Root level**: Unity project files, editor config, git attributes
- **Package level** (`Assets/BayatGames/SaveGameFree/`): All package-specific files

### Building

The package can be exported from the `Assets/BayatGames/SaveGameFree` directory.

### Testing

Tests are located in `Assets/BayatGames/SaveGameFree/Tests/Editor/`

Run tests via:
- Unity Test Runner (Window > General > Test Runner)
- Command line: `Unity -runTests -testPlatform EditMode`

## Version

Current version: **1.0.0**

See [CHANGELOG.md](Assets/BayatGames/SaveGameFree/CHANGELOG.md) for version history.

## License

MIT License - See [LICENSE](Assets/BayatGames/SaveGameFree/LICENSE) for details.

---

Made with :heart: by [Bayat Games](https://bayat.io)
