# Save Game Free
<<<<<<< HEAD
=======

Hi Awesome Visitor.
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db

Save Game Free is a free and simple but powerful solution for saving and loading game data in Unity Game Engine.

[:rocket: Get Save Game Pro](https://github.com/EmpireAssets/SaveGamePro/)

[:heart: Any contribution is Welcome](https://github.com/BayatGames/SaveGameFree/blob/master/CONTRIBUTING.md)

## Features

<<<<<<< HEAD
The below features made Save Game Free excellent:

- Cross Platform (Windows, Linux, Mac OS X, Android, iOS, tvOS, SamsungTV, WebGL, ...)
- Auto Save
- Web Support
- Encryption
- Easy to Use
- Simple but Powerful yet
=======
The Save Game Free has some features that might be useful for you:
- Cross Platform (Windows, Mac, Linux, Android, iOS, Tizen, ...) but WebGL not supported. (You can get Full cross platform support by [Save Game Pro](https://github.com/BayatGames/SaveGamePro/) and with WebGL support)
- Easy to use
- Free to use
- Open Source (You can fork and create it again!)
- Simple but Powerful
- Built-in JSON, XML and Binary Serialization support
- Custom Serialization Support ([Read More](https://github.com/BayatGames/SaveGameFree/wiki/How-to-Create-Custom-Serializer%3F))
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db

## Download

[:sparkles: Download from Asset Store](https://www.assetstore.unity3d.com/#!/content/81519)

[:moneybag: Download from itch.io](https://bayat.itch.io/save-game-free)

<<<<<<< HEAD
[:rocket: Download the latest version from the Releases section](https://github.com/EmpireAssets/SaveGameFree/releases/latest)

[:fire: Download the Source Code](https://github.com/EmpireAssets/SaveGameFree/archive/master.zip)

## Getting Started

Here is a simple usage of Save Game Free:
=======
[:rocket: Download the latest version from the Releases section](https://github.com/BayatGames/SaveGameFree/releases/latest)

[:fire: Download the Source Code](https://github.com/BayatGames/SaveGameFree/archive/master.zip)

## Getting Started

Let me give a simple example.

It is our **GameData** class that contains the game data we want to save, such as score, achievements, ...

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db

```csharp
SaveGame.Save<int> ( "score", score );
```

and the full is:

<<<<<<< HEAD
=======
}
```

Then we use our **GameData** class to do save and load functionally as below:

>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BayatGames.SaveGameFree;

public class SimpleUsage : MonoBehaviour {

	public int score;

	void OnApplicationQuit () {

		SaveGame.Save<int> ( "score", score );

	}

}
```
<<<<<<< HEAD
=======

All done now this class will load game data on start and saves it on exit. thats what we want to implement.
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db

## Resources

[:book: Examples](https://github.com/BayatGames/SaveGameFree/wiki/Examples)

[:sparkles: Write Review](https://www.assetstore.unity3d.com/#!/content/81519)

[:fire: Community Thread](https://forum.unity3d.com/threads/released-empireassets-save-game-free.457658/)

<<<<<<< HEAD
[:rocket: Development Board](https://trello.com/bayatgames)

## Follow Us
=======
[:heart: Contribute to us](https://github.com/BayatGames/SaveGameFree/blob/master/CONTRIBUTING.md)

## Follow US
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db

[:notebook: Check out our development board](https://trello.com/bayatgames)

[Medium Publication](https://medium.com/bayat-games)

[Telegram Channel](https://t.me/BayatGamesOfficial)

[Slack Team](https://bayatgames.slack.com)

[Gitter Channel](https://gitter.im/BayatGames)

[Twitter (Hasan Bayat)](https://www.twitter.com/EmpireWorld1393)

[Google Group](https://groups.google.com/forum/#!forum/bayatgames)

[Google+ Community](https://plus.google.com/communities/108974587311747022650)

[Facebook Page](https://www.facebook.com/Bayat-Games-277386306024083)

<<<<<<< HEAD
[Twitch.tv](https://www.twitch.tv/bayatgames)

[Tumblr](https://bayatgames.tumblr.com)

[Discord](https://discordapp.com/channels/307041709701988352/307041709701988352)

[YouTube](https://www.youtube.com/channel/UCDLJbvqDKJyBKU2E8TMEQpQ)

[Reddit](https://www.reddit.com/r/bayatgames)

=======
>>>>>>> b32da85ab5ed5e39157e6a6e4c04ca99e0b3a4db
[:e-mail: Email us (hasanbayat1393@gmail.com)](mailto:hasanbayat1393@gmail.com)

## License

MIT @ [Bayat Games](https://github.com/BayatGames)

Made with :heart: by [Bayat Games](https://github.com/BayatGames)
