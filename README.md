# Assignment 5: Shader Graphs – Lightsaber

COMP 590: Virtual Reality and Game Development

## Demo Video
[Video link here]

## How to Run
1. Open the project in Unity 6 (URP).
2. Open `Assets/Scenes/SampleScene`.
3. Press **Play**, click inside the Game view, and press **Space** to turn the lightsaber off/on.

## What's Included
- **Part 2–3:** Lightsaber blade using a custom URP Lit Shader Graph (`BladeShader`) with exposed properties (BladeColor, OuterGlow, InnerGlow, PulseSpeed) driving a pulsing emission, plus Bloom post-processing on the Global Volume.
- **Part 4:** `SaberScript.cs` (on `BladeBase`) extends/collapses the blade along its local y axis using an empty parent object as the pivot, and deactivates the blade when fully collapsed. Hilt and blade are grouped under a `Lightsaber` parent.
- **Part 5 extensions:**
  - A point light attached to the blade that lights up the floor and turns off with the blade.
  - Power-on / power-off sound effects and a looping hum that fades out as the blade collapses.

## Outside Resources
- Sound effects: [site name + link for each sound]
- [Any other resources used]

## Notes
- [Anything else the grader should know]
