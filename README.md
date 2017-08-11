# Save Game Free

Save Game Free is a free and simple but powerful solution for saving and loading game data in Unity Game Engine.

[:rocket: Get Save Game Pro](https://github.com/EmpireAssets/SaveGamePro/)

<p align="center">
	<img src="https://raw.githubusercontent.com/BayatGames/SaveGameFree/master/Assets/BayatGames/SaveGameFree/PressKit/Unity%20Package%20Key%20Images%20-%20Large-01.png" alt="Save Game Free Logo" />
</p>

## Features

The below features made Save Game Free excellent:

- Cross Platform (Windows, Linux, Mac OS X, Android, iOS, tvOS, SamsungTV, WebGL, ...)
- Auto Save
- Web Support
- Encryption
- Easy to Use
- Simple but Powerful yet

## Download

[:sparkles: Download from Asset Store](https://www.assetstore.unity3d.com/#!/content/81519)

[:moneybag: Download from itch.io](https://bayat.itch.io/save-game-free)

[:rocket: Download the latest version from the Releases section](https://github.com/EmpireAssets/SaveGameFree/releases/latest)

[:fire: Download the Source Code](https://github.com/EmpireAssets/SaveGameFree/archive/master.zip)

## Getting Started

Here is a simple usage of Save Game Free:

```csharp
SaveGame.Save<int> ( "score", score );
```

and the full is:

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

## Resources

[:book: Examples](https://github.com/BayatGames/SaveGameFree/wiki/Examples)

[:sparkles: Write Review](https://www.assetstore.unity3d.com/#!/content/81519)

[:fire: Community Thread](https://forum.unity3d.com/threads/released-empireassets-save-game-free.457658/)

[:heart: Contribute to us](https://github.com/BayatGames/SaveGameFree/blob/master/CONTRIBUTING.md)

[:rocket: Add your Game to our Showcase](https://github.com/BayatGames/SaveGameFree/wiki/Showcase)

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

[Twitch.tv](https://www.twitch.tv/bayatgames)

[Tumblr](https://bayatgames.tumblr.com)

[Discord](https://discordapp.com/channels/307041709701988352/307041709701988352)

[YouTube](https://www.youtube.com/channel/UCDLJbvqDKJyBKU2E8TMEQpQ)

[Reddit](https://www.reddit.com/r/bayatgames)

[:e-mail: Email us (hasanbayat1393@gmail.com)](mailto:hasanbayat1393@gmail.com)

## License

MIT @ [Bayat Games](https://github.com/BayatGames)

Made with :heart: by [Bayat Games](https://github.com/BayatGames)
