# Doofus Adventure Game

A 3D Unity game built for the **Hitwicket Game Developer Challenge**. You control Doofus, a cube character, guiding him across temporary green metallic platforms called **Pulpits** for as long as possible before he falls.

## Gameplay

Doofus loves exploring green platforms called Pulpits — but they don't last long. Guide Doofus to walk on as many Pulpits as possible. Each Pulpit has a countdown timer; when it expires, the Pulpit is destroyed and Doofus falls if he's still on it. Falling off the edge or off a destroyed Pulpit ends the game.

## Controls

- **W / Up Arrow** — Move forward
- **S / Down Arrow** — Move backward
- **A / Left Arrow** — Move left
- **D / Right Arrow** — Move right

## Features

- **Level 1 — Movement & JSON-driven placement:** Doofus's speed and Pulpit timing values are read from `DoofusDiary.json` at runtime, never hardcoded.
- **Level 2 — Scoring:** Score increases by one for every successful move to a new Pulpit.
- **Level 3 — Start & Game Over screens:** A Start Screen gates gameplay until the player is ready; a Game Over Screen shows the final score with a Restart option.
- **Live Pulpit Timer:** Both currently-active Pulpits display a real-time, one-decimal-place countdown (e.g. `3.8`) on their surface, so the player can see exactly how much time remains before each one collapses.

## Configuration — Doofus Diary (`Assets/Data/Resources/DoofusDiary.json`)

All core gameplay values are data-driven from this JSON file:

```json
{
  "player_data": {
    "speed": 3
  },
  "pulpit_data": {
    "min_pulpit_destroy_time": 4,
    "max_pulpit_destroy_time": 5,
    "pulpit_spawn_time": 2.5
  }
}
```

| Field | Meaning |
|---|---|
| `speed` | Doofus's movement speed |
| `min_pulpit_destroy_time` / `max_pulpit_destroy_time` | Each Pulpit's lifetime is randomized between these two values (seconds) |
| `pulpit_spawn_time` | The remaining-time threshold at which the *next* Pulpit spawns adjacent to the current one |

Only two Pulpits ever exist simultaneously, and each new one spawns adjacent to (not on top of) the previous one.

## Project Structure

```text
Assets/
├── Data/Resources/DoofusDiary.json   # Game config (speed, pulpit timings)
├── Materials/PulpitMaterial.mat      # Green metallic Pulpit material
├── Prefabs/Pulpit.prefab             # Pulpit prefab (incl. live timer text)
├── Scenes/SampleScene.unity          # Main game scene
├── Scripts/
│   ├── CameraFollow.cs               # Third-person camera follow
│   ├── DoofusController.cs           # Player movement (new Input System)
│   ├── GameConfig.cs                 # Loads & exposes DoofusDiary.json
│   ├── GameManager.cs                # Game state, Start/Game Over flow
│   ├── Pulpit.cs                     # Pulpit lifecycle, timer, landing detection
│   ├── PulpitManager.cs              # Spawns/manages the two active Pulpits
│   └── ScoreManager.cs               # Score tracking
└── UI/                                # Start Screen, Game Over Screen, Score UI
```

## How to Run

1. Clone this repository.
2. Open the project in **Unity 6+** (Unity Hub → Add → select the project folder).
3. Open `Assets/Scenes/SampleScene.unity`.
4. Press **Play**.
5. Click **Start** on the Start Screen, then use WASD/Arrow keys to guide Doofus across the Pulpits.

## Tech Notes

- Built with Unity's new **Input System** (`UnityEngine.InputSystem`).
- Pulpit timer text uses **3D TextMeshPro** (not Canvas UI) so it renders directly on the platform surface in world space.
- Game state (`IsGameStarted`, `IsGameOver`) gates both movement and scoring to prevent edge-case interactions before Start or after Game Over.

## Screenshots & Gameplay

See the [`Screenshots/`](./Screenshots) folder and [`Gameplay/`](./Gameplay) folder for images and a gameplay recording.

## Author

Built for the Hitwicket Game Developer Challenge (VIT Assignment 2026).
