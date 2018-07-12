# DuckGameReskins
This is basically Armytecel's clientreskins mod in the sense that others wont need the mod, but with all features as normal reskins plus a couple extra stuff.

You make a .rsk file using the ReskinMaker application and put it in the steamapps\common\DuckGame folder and it should appear ingame.
If others have the mod the skin will be shown to them aswell.

#How to make a reskin
Open up the reskin maker, here you'll find a multiple options, and if you click them you'll be greeted by more options such as which image to use or change the resolution of the skins.
If you hover over a setting it will tell what the problem is, and if you want extra info right click it and select extra info.

You can use the Save button in the toolbar to save the hat as a .png file, which you can then open with the open button.
But the buttons of most importance is the Import and Export buttons, using these you can save and open .rsk files.

If you want higher resolution on your skin, you can change stuff in the Texture Settings tab. For instance, if you set Duck Sprite Size to 64x64, then each frame of the duck will require 64x64 pixels (double the normal amount) but will be scaled down so that it appears as if the duck had a higher definition.
The Duck Sprite Size is used for the Duck Texture, Quack Texture, and Controlled Texture; so all of these has to have the correct size.

# Creating a reskin pack
Using the Create Reskin Pack button found in the ReskinMaker you can now create reskin packs that conains multiple reskins and can be uploaded to the workshop, these all are dependant on having the reskin mod installed, but others will still not have to download them making them way better than hatpacks.

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