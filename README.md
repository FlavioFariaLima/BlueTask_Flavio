# BlueTask_Flavio

# Dungeon Danger


# Basic Commands

WASD - To move the Character;
Click with the mouse to shoot in the direction the cursor is pointing;
It is not possible to move and fire the spell at the same time. I like hardcore;
Approach the wizard to interact. Drag to sell or to buy;
Shoot the boxes or vases to break them;
You can also push some objects;
Beware of enemies;

# Project Description

I implemented a simple loot collection system for a character within a Unity game.

PlayerCharacter Interaction: Character to detect and collect loot items upon contact.

Loot Behavior: I designed a behavior for the loot, allowing each item to have a unique name.

Player's Inventory: A basic inventory system where items collected by the player are stored.

 The inventory system leverages Unity's Event System for drag-and-drop functionality. It allows players to equip, unequip, buy, and sell items via an intuitive UI. 

The equipment system, Character_Equipment,  manages equipped items and updates the character's sprite renderers to reflect the current gear. Both systems are highly modular, enabling easy expansion for new item types and functionalities. Overall, this project serves as a robust foundation for any inventory-related requirements in game development.

I established an item classification using enumeration for different gear types. Leveraging Unity's ScriptableObjects, I created a blueprint, "Inventory_ItemBlueprint", to define items. This blueprint encompasses essential attributes like item name, icons, type, in-game sprite, and its value, streamlining the process of adding new items to our inventory and equipment system.

I developed an enemy behavior system for our game, Behavior_Enemy. I integrated basic enemy configurations, such as movement speed, detection range, and health. The enemy constantly roams within a set radius and uses a distance check to detect and fire magic projectiles at the player. Upon defeat, the enemy has the potential to drop in-game loot. The built-in error checks ensure that references are correctly set, providing a seamless gameplay experience.

I implemented a main menu script to manage the game flow. It provides functionalities to pause, resume, and exit the game, automatically binding to Unity's user interface buttons.

