# Save Game Free

Save Game Free is a free and simple but powerful solution for saving and loading game data in Unity Game Engine.

<p align="center">
	<img src="https://raw.githubusercontent.com/BayatGames/SaveGameFree/master/Assets/BayatGames/SaveGameFree/PressKit/Unity%20Package%20Key%20Images%20-%20Large-01.png" alt="Save Game Free Logo" />
</p>

## Features

The below features made Save Game Free excellent:

- Cross Platform (Windows, Linux, Mac OS X, Android, iOS, tvOS, SamsungTV, WebGL, ...)
- Auto Save
- Web Support
- Encryption with improved security
- Async/Await Support for non-blocking file operations
- Enhanced Error Handling with detailed exception messages
- Memory-efficient with automatic resource cleanup
- Easy to Use
- Simple but Powerful
- Unity 6 Compatible

## Download

[:sparkles: Download from Asset Store](https://assetstore.unity.com/packages/tools/input-management/save-game-free-gold-update-81519)

[:rocket: Download the latest version from the Releases section](https://github.com/BayatGames/SaveGameFree/releases/latest)

[:fire: Download the Source Code](https://github.com/BayatGames/SaveGameFree/archive/master.zip)

## Getting Started

### Basic Usage

Here is a simple usage of Save Game Free:

```csharp
SaveGame.Save<int>("score", score);
```

Full example:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bayat.Unity.SaveGameFree;

public class SimpleUsage : MonoBehaviour
{
    public int score;

    void OnApplicationQuit()
    {
        SaveGame.Save<int>("score", score);
    }

    void Start()
    {
        // Load with default value
        score = SaveGame.Load<int>("score", 0);
    }
}
```

### Async Usage (Recommended for large files)

For better performance with large save files, use async methods:

```csharp
using System.Threading.Tasks;
using UnityEngine;
using Bayat.Unity.SaveGameFree;

public class AsyncUsage : MonoBehaviour
{
    public int score;

    async void OnApplicationQuit()
    {
        await SaveGame.SaveAsync<int>("score", score);
    }

    async void Start()
    {
        score = await SaveGame.LoadAsync<int>("score", 0);
    }
}
```

### Encryption Usage

For sensitive data, enable encryption:

```csharp
// Set a custom password (IMPORTANT: Use your own secure password!)
SaveGame.EncodePassword = "YourSecurePassword123!";

// Save with encryption
SaveGame.Save<string>("playerData", sensitiveData, encode: true);

// Load with encryption
var data = SaveGame.Load<string>("playerData", defaultValue: "", encode: true);
```

## Resources

[:book: Documentation](http://docs.bayat.io/savegamefree)

## License

MIT @ [Bayat Games](https://github.com/BayatGames)

Made with :heart: by [Bayat Games](https://github.com/BayatGames)
