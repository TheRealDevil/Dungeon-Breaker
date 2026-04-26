# 2D Roguelike Dungeon Crawler

A procedural dungeon crawler built in Unity 6, featuring dynamic room generation, physics-based combat, and mouse-aimed shooting mechanics.

## 🚀 Features

- **Procedural Generation**: Random walk algorithm that generates unique dungeon layouts with Start, Enemy, Treasure, and Boss rooms.
- **Dynamic Room Logic**: Rooms automatically lock doors when the player enters and unlock only after all enemies are defeated.
- **Combat System**: 
  - Mouse-aimed projectile system using the New Input System.
  - Multiple enemy types (Melee Chasers and Ranged Shooters).
  - Custom made animations
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
- `EnemyHealth.cs`: Generic health script for damaging and destroying entities.

## 🎮 How to Play

1. **Move**: WASD keys.
2. **Aim**: Mouse cursor.
3. **Shoot**: Spacebar or Left Mouse Click.
4. **Goal**: Clear rooms to find the Boss room and progress to the next floor.
