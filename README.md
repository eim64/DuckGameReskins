# DuckGameReskins
This is basically Armytecel's clientreskins mod in the sense that others wont need the mod, but with all features as normal reskins plus a couple extra stuff.

You make a .rsk file using the ReskinMaker application and put it in the steamapps\common\DuckGame folder and it should appear ingame.
If others have the mod the skin will be shown to them aswell.

# new stuff
Capes, HD skins and UI.

# Creating a reskin pack
Using the Create Reskin Pack button found in the ReskinMaker you can now create reskin packs that can be uploaded to the workshop, these all are dependant on having the reskin mod installed, but others will still not have to download them making them way better than hatpacks.

# Coding your own components
I wrote it so that its really easy to add new features to skins.
You want to make your reskin the best of all reskins?

Adding in-game stuff:

1. Learn to code, then reference this mod and create a class that extends ReskinComponent.
2. Add the type of your class to the list Reskin.ComponentTypes
3. ????
4. profit.

The components come with plenty of overrides, which should let you do basically anything.
There are 2 examples of this which you can find [in this folder](https://github.com/eim64/DuckGameReskins/tree/master/reskins/build/src/ReskinComponents). The 2 components add a cape and equipment re textures.

Adding stuff to the ReskinMaker:
You'll have to modify the source yourself if you want to change anything here, so heres a rough explanation of how the maker works.

The list contains ItemDatas which contains a User Control and some way of parsing and encoding dataChunks from that control which is then put into the hat texture.
All the stuff in that list is added at the start of the [MainForm.cs class](https://github.com/eim64/DuckGameReskins/blob/master/ReskinMaker/ReskinMaker/MainForm.cs).
I'm really bad at explaining so you're probably better off reading the stuff instead of me overcomplicating it.