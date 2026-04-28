# ⚔️ Dungeon Breaker
A top-down 2D action game built in Unity. Navigate a dangerous dungeon, choose your hero, and survive the boss encounter while racking up a high score.

## 🚀 Features

- **Procedural Generation**: Random walk algorithm that generates unique dungeon layouts with Start, Enemy, and Boss rooms.
- **Dynamic Room Logic**: Rooms automatically lock doors when the player enters and unlock only after all enemies are defeated.
- **Character Selection System**: Choose between unique character classes (Desert Knight and Marksman) with distinct stats and visuals driven by ScriptableObjects.
- **Persistent Game Management**: A Singleton GameManager tracks your health, score, and selected character across multiple scenes and floors.
- **Combat System**: 
  - Mouse-aimed projectile system using the New Input System.
  - Player iFrames: Visual flickering effect providing temporary invincibility after taking damage.
  - Multiple enemy types (Melee Chasers and Ranged Shooters).
  - Enemy Damage Flash: Enemies flash red when hit, using a custom math-based flicker logic for instant player feedback.
  - Smart Enemy AI: Enemies detect and chase the player when they enter a specific range, featuring automatic sprite-flipping to always face their target.
- **Physics-Based Movement**: Custom 2D movement for players and enemies using Rigidbody2D.

## 🛠️ Technical Details

- **Engine**: Unity 6 (6000.0.38f1 LTS)
- **Input**: Unity Input System Package
- **Rendering**: 2D Universal Render Pipeline (URP) or Standard 2D
- **Scripting**: C#

## 📂 Project Structure

- `BoardManager.cs`: Handles tilemap-based floor generation.
- `DungeonGenerator.cs`: The "brain" that maps out the room positions and types.
- `RoomController.cs`: Manages individual room states, door locking, and enemy spawning.
- `GameManager.cs`	The central brain. Handles high scores, scene transitions, and cross-scene data persistence.
- `CharacterData.cs`	A ScriptableObject blueprint for defining character stats and animator controllers.

## 🎮 How to Play

**Download**: Head to the Releases section and download the latest .zip file.
**Extract**: Unzip the folder to your desktop.
**Run**: Double-click the .exe to launch the game.

- **Controlls**
  1. **Move**: WASD keys.
  2. **Aim**: Mouse cursor.
  3. **Shoot**: Spacebar or Left Mouse Click. 
  4. **Esc**: Return to Menu
  5. **Goal**: Clear rooms to find the Boss room and progress to the next floor.
