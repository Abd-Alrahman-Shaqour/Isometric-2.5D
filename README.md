# MadHook-Isometric-2.5D
My submission for the MadHook Tech test

# Project Scalability
Introduction
This project has been designed with scalability in mind, allowing for the efficient addition of new levels and easy integration of new scripts to control events across scenes. Below are key features and practices that contribute to the project's scalability.

# Features
## 1. Scene Architecture
The project follows a modular scene architecture that separates different aspects of the game, such as gameplay, UI, and events. Each scene focuses on a specific functionality, making it easier to manage and expand.

## 2. Scriptable Objects
Scriptable Object Architecture
The extensive use of Scriptable Objects decouples data from code, enabling easy modification and extension without altering the underlying scripts. This is particularly useful for defining assets like weapons, enemies, and player stats.

## 3. Game Events System
A robust Game Events system based on Scriptable Objects allows scripts to communicate across scenes. New scripts can simply subscribe to relevant events, providing a clean and organized way to control behavior.

## 4. Singleton Design Pattern
The Singleton design pattern is employed for manager classes like GameManager, UIManager, and SaveManager. This ensures that essential systems persist across scenes and can be easily accessed and modified.

## 5. Centralized Reference Management
A centralized reference management system helps find and set references dynamically during runtime. This optimizes the process of locating essential objects and UI elements, ensuring a smooth and error-free workflow.

# Adding a New Level
Adding a new level involves setting up the map using a tile editor, configuring events, and utilizing prefab assets. Below is a step-by-step guide:

1. Create a New Scene
Start by duplicating an existing level scene as a template for the new level.

2. Design Gameplay Elements
Utilize a tile editor to design the map layout for the new level. Place assets, enemies, and other gameplay elements within the scene.

3. Configure Scriptable Objects
Adjust Scriptable Objects to define specific attributes for the new level. These may include enemy types, weapons, or any level-specific data. This decouples data from code, allowing for easy modification.

4. Subscribe to Game Events
If the new level requires specific interactions or events, create new scripts that subscribe to existing Game Events or add new ones. For example, the glowing orb script may handle a unique event triggering special effects.

5. Integrate Exit Trigger Prefab
To facilitate scene transitions, use an exit trigger prefab. Place this prefab at the end of the level or wherever a scene transition is desired. Configure the exit trigger to load the next scene when activated.

6. Build and Test
Build the project and thoroughly test the new level. Ensure that all assets, events, and scene transitions function as expected. Debug and iterate as needed.

# Scene Event Control: Glowing Orb Script
The GlowingOrb script serves as a powerful example of how a single script can efficiently control various events within a scene. Its modular design and event-driven approach make it a key component for scalable and easily expandable gameplay. Let's explore how this script becomes a central piece in managing scene events:

## Features
1. **Dynamic Sprite Change**: The orb dynamically changes its appearance based on the game state.
2. **Interaction with Enemies**: The orb interacts with the EnemySpawnManager to influence the state of enemy spawns.
   Audio Feedback: Utilizes the AudioManager to play audio feedback on specific events.
3. **Player Interaction**: Reacts to player interaction, triggering events when the player collides with it.

## Event Orchestration
### Level State Management
The GlowingOrb script seamlessly integrates with the EnemySpawnManager to respond to changes in the level state. This creates a centralized control hub for managing events tied to different game phases or scenarios.
```csharp
// Example of reacting to level state changes
private void EnemySpawnManagerOnLevelStateChanged(LevelState levelState)
{
    if (levelState == LevelState.AllEnemiesDead)
    {
        // Perform actions when all enemies are defeated
        _spriteRenderer.sprite = goldenOrb;
        _audioManager.Play("DoorOpen");
        doorExit.SetActive(true);
        _gameManager.SaveGame();
        _gameManager.LevelCompleted();
    }     
}
```
### Player Interaction
The script's interaction with the player showcases its role in controlling events triggered by player actions. This pattern allows for the easy introduction of interactive elements across various levels.
```csharp
// Example of player interaction triggering specific events
private void OnTriggerEnter2D(Collider2D other)
{
    if(other.CompareTag("Player"))
        if (_enemySpawnManager.levelState != LevelState.AllEnemiesDead)
        {
            // Perform actions when the player interacts with the orb
            _spriteRenderer.sprite = corruptedOrb;
            _enemySpawnManager.UpdateEnemyState(LevelState.SpawnEnemies, 5);
            GetComponent<BoxCollider2D>().enabled = false;
            doorExit.SetActive(false);
        }
}
```
 ### Audio Management
By integrating with the AudioManager, the script ensures that audio events are centrally managed. This design simplifies the addition of new sounds or adjustments to existing audio feedback.
```csharp
// Example of reacting to game state changes
private void GameManagerOnLevelStateChanged(GameState gameState)
{
    if(gameState == GameState.GameOver)
        audioSource.Stop();
}
```
## Scalability Through Modularity
The **GlowingOrb** script's modularity and event-driven structure serve as a blueprint for maintaining and expanding scene events. Its adaptability makes it an excellent reference for creating new scripts that seamlessly integrate with existing systems, fostering a scalable game architecture.
