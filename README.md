# lasagne
_An enhanced multiplayer patch, patcher, and matchmaking server for Garfield Kart._

(The developers of the game have released an update that enables multiplayer, like this patch. This patch has some more features, as you can see below, and you can still install it if you want them.)

## Features

* Working multiplayer
* Private servers
* Matchmaking server switching / custom matchmaking servers
* Included matchmaking server, patched to allow succsessful compilation on modern GNU/Linux.
* Direct connect
* _Interesting_ multiplayer settings, like:
  - Unlimited powerups
  - Jumping bots
  - All powerups are of one type
* Some other features that you'll have to look for

## Installation

Download the patcher from the releases section and run it. It should find your game and apply the patch to it. The patcher only supports Windows; the game also supports macOS, but I couldn't be bothered to deal with the pain of a multiplatform GUI.

## How Did I Do This?

.NET Intermediate Language (like Java bytecode), is the instruction set for the .NET VM. I is much closer the original code (and preserves more information) than most other instruction sets, like x86. For this reason, IL assembly can be decompiled into real C# that is pretty close to the original code-this code can even be recompiled. dnSpy is a wonderful tool which handles this process (and provices a debugger!), which makes reversing _and patching_ Unity/C# games pretty easy.

## Building from Source

You can run `patcher/build.bat`, to get the dependencies, and build the patcher.

Patching the game itself is tricker. The easiest way to modify the source is to run the patcher on your game, and then decompile the resulting `Assembly-CSharp.dll` in `Garfield Kart/GarfieldKart_nomulti/Managed/Assembly-CSharp.dll`. If you really want the full building from source experience, you will need to decompile a vanilla version using dnSpy (with the offset information comments turned off), export it as a Visual Studio project, and apply `mod/0001-Add-all-game-patches.patch` to it using Git, then get Visual Studio to build it (this is a non-trivial task that I haven't gotten to work, because I didn't need to). Good luck.

## Legal

I am only distributing my changes to the game, not the game itself. You need a working copy to use any of this. The patches are only the differences, not the orignal files. The license that applies to all my work in this repository can be found under a file called `LICENSE`. All other content is copyrighted by its respective owners.
