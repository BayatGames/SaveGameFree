# Save Game Free

Hi Awesome Visitor.

Save Game Free is a free and simple but powerful solution for saving and loading game data in unity.

[:heart: Any contribution is Welcome](https://github.com/BayatGames/SaveGameFree/blob/master/CONTRIBUTING.md)

## Features

The Save Game Free has some features that might be useful for you:
- Cross Platform (Windows, Mac, Linux, Android, iOS, Tizen, ...) but WebGL not supported. (You can get Full cross platform support by [Save Game Pro](https://github.com/BayatGames/SaveGamePro/) and with WebGL support)
- Easy to use
- Free to use
- Open Source (You can fork and create it again!)
- Simple but Powerful
- Built-in JSON, XML and Binary Serialization support
- Custom Serialization Support ([Read More](https://github.com/BayatGames/SaveGameFree/wiki/How-to-Create-Custom-Serializer%3F))

## Download

[:sparkles: Download from Asset Store](https://www.assetstore.unity3d.com/#!/content/81519)

[:moneybag: Download from itch.io](https://bayat.itch.io/save-game-free)

[:rocket: Download the latest version from the Releases section](https://github.com/BayatGames/SaveGameFree/releases/latest)

[:fire: Download the Source Code](https://github.com/BayatGames/SaveGameFree/archive/master.zip)

## Getting Started

Let me give a simple example.

It is our **GameData** class that contains the game data we want to save, such as score, achievements, ...

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The game data the saves and loads from disk
public class GameData {

  // Sample variable for the game data class
  public int score = 0;

}
```

Then we use our **GameData** class to do save and load functionally as below:

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SaveGameFree;

// Game data manager, saves and loads game data on start and exit.
public class GameDataManager : MonoBehaviour {

  // Our game data
  public static GameData gameData;
  
  // Load data on start
  void Awake () {
    Saver.InitializeDefault ();
    gameData = Saver.Load<GameData> ("gameData");
  }
  
  void Update () {
    
    // Apply your functionally to the game data
    
    if (Time.time % 1) {
      gameData.score = (int)Time.time;
    }
    
  }
  
  // Save data on exit
  void OnApplicationQuit () {
    Saver.Save<GameData> (gameData, "gameData");
  }

}
```

All done now this class will load game data on start and saves it on exit. thats what we want to implement.

## Resources

[:book: Examples](https://github.com/BayatGames/SaveGameFree/wiki/Examples)

[:sparkles: Write Review](https://www.assetstore.unity3d.com/#!/content/81519)

[:fire: Community Thread](https://forum.unity3d.com/threads/released-empireassets-save-game-free.457658/)

[:heart: Contribute to us](https://github.com/BayatGames/SaveGameFree/blob/master/CONTRIBUTING.md)

## Follow US

[:notebook: Check out our development board](https://trello.com/bayatgames)

[Medium Publication](https://medium.com/bayat-games)

[Telegram Channel](https://t.me/BayatGamesOfficial)

[Slack Team](https://bayatgames.slack.com)

[Gitter Channel](https://gitter.im/BayatGames)

[Twitter (Hasan Bayat)](https://www.twitter.com/EmpireWorld1393)

[Google Group](https://groups.google.com/forum/#!forum/bayatgames)

[Google+ Community](https://plus.google.com/communities/108974587311747022650)

[Facebook Page](https://www.facebook.com/Bayat-Games-277386306024083)

[:e-mail: Email us (hasanbayat1393@gmail.com)](mailto:hasanbayat1393@gmail.com)

## License

MIT @ [Bayat Games](https://github.com/BayatGames)

Made with :heart: by [Bayat Games](https://github.com/BayatGames)
