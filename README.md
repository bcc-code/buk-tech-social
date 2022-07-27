# BUK Tech Social

Social game development at BUK Sommercamp 2022

## Setting up the environment

### Download and install Visual Studio Code

1. Download Visual Studio Code (VSCode) https://code.visualstudio.com/

**Note:** Do _not_ install Visual Studio! That's a different program.

### Download and install git

#### On Windows

1. Download git from the following link: https://git-scm.com/downloads
2. Follow the instructions from the downloaded file.
3. When asked what your git editor should be, select VSCode.
4. When asked whether git should be added to your PATH, make sure you that option is set.

#### On MacOS
1. Open terminal and run `brew install git`

#### On Linux

Install it using your package manager. On Debian based systems including Ubuntu or Mint you can run `sudo apt install git` in terminal

### Download and install Unity

1. Download Unity hub - https://unity3d.com/get-unity/download
2. Open Unity hub
3. Skip the recommended install: ![Skip recommended installation](./doc/skip-recommended-install.png)
4. Install the latest 2022 version. (Apple Silicon users, see next screenshot below this one) ![Unity install](./doc/install-unity.png)  
  **NOTE:** If you use an Apple Silicon MacOS, you must use the Unity Hub Beta version (enable beta inside Unity Hub) to find Unity 2022 in the list of available installs. Then you must select the Silicon version, not the Intel version ![Mac silicon unity install](./doc/install-mac-silicon.png)

### Clone the git repository

1. Open VSCode
2. Click the _Clone Git Repository button_: ![Clone button in VSCode](./doc/clone-repo.png)
3. Enter this URI: `https://github.com/bcc-code/buk-tech-social.git`

### Load the project in Unity using Unity Hub

Press the play button to play-test the scene and see whether it works.


### World building

To erase the world and build your own world:
- First erase the existing world: Delete the _MAST_Holder_ object from the scene hierarchy.
- Click _Tools_ » _MAST_ » _Open MAST Window_
- In the MAST window, click the folder icon in the bottom left corner to load prefabs.
- Choose one of the folders in _Assets/FSP/_ folder. For example _Modular Terrain_ and click _Open_
- Click one of the loaded prefabs in the MAST window to select it.
- Click in the world view to place a copy of the prefab.
- Now you can build your own world!

#### MAST Hotkeys

To change the height of the drawing grid, press <kbd>Shift</kbd><kbd>W</kbd> and <kbd>Shift</kbd><kbd>S</kbd>.  
To rotate what you are drawing press <kbd>Space</kbd>.  
To mirror what you are drawing press <kbd>F</kbd>.  
To hide the MAST grid, press <kbd>G</kbd>.  
