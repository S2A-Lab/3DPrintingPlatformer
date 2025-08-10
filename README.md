# Operation Printport

Operation Printport is a pixel-art 2D platformer with an educational twist. The player uses a Printport to collect materials and printer parts, upgrading their 3D printer to create blocks that help navigate levels and overcome obstacles. The game blends classic platforming gameplay with an introduction to 3D printing concepts.

## Features
• Singleplayer pixel-art platformer built in Unity 6  
• Educational 3D printing gameplay, collect resources and printer parts to create buildable blocks  
• Eight unique scenes  
  • Main Menu  
  • Intro Scene  
  • Act 0 to Act 6 (Levels 1 to 4 plus two end-game stages)  
• Powered by  
  • Cinemachine  
  • New Unity Input System  
  • Unity Tilemap  

## Requirements
Unity Version: Unity 6 (Linux Debian version recommended for Linux users, but works on Windows and macOS)  
Operating Systems: Linux, Windows, macOS  

## How to Download and Open the Project

1. Install Unity Hub  
   Download Unity Hub for your operating system at [https://unity.com/download](https://unity.com/download)  
   Follow the installation instructions for your platform  

2. Install Unity 6  
   Open Unity Hub and go to the Installs tab  
   Click Add and select Unity 6  
   Add Linux Build Support if on Linux or appropriate platform modules for your target build  

3. Clone or Download the Repository  
   ```bash
   git clone https://github.com/<your-username>/<your-repo>.git
   ```
   Or click Code then Download ZIP in GitHub  

4. Open in Unity  
   In Unity Hub, click Add Project and select the folder where you cloned or downloaded the project  
   Open the project, no extra setup steps required  

## Project Structure
```
Assets/
  Scripts/
    Player/       # Player movement, abilities, input
    UI/           # UI elements and menus
    Game/         # Game state, scoring, level logic
  Scenes/
    Main Menu
    IntroScene
    Act0
    Act1
    Act2
    Act3
    Act4
    Act5
    Act6
```

## Contributing
Fork the repository as contributors cannot push directly to the main repo  
Make your changes in your own fork  
Submit a pull request if you would like your changes reviewed and possibly merged  
There are no enforced coding style or naming conventions, once forked it is your project to modify  

## Future Ideas
• More printable object types with different gameplay effects  
• Additional acts and secret levels  
• Expanded educational content on 3D printing techniques  

## Known Issues
• Minor camera clipping in some tilemap corners  
• Occasional frame drops on very large print builds  
