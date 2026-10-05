# OpenCV + Unity Hand Interaction

This project builds on BIGBOSS-dedsec's OpenCV-Unity-To-Build-3DHands, which provided a Python hand-tracking script and three loose C# scripts that had to be assembled into a Unity scene by hand and could only display the tracked hand. I updated it to run on Python 3.10 with pinned, working versions of MediaPipe and cvzone, since newer MediaPipe releases break the original code. I also fixed bugs in the original scripts: errors before data arrives, decimal parsing on some locales, and a UDP port left locked between runs. I added a script that automatically generates the hand's 21 joints and bones, replacing the manual setup. The MAIN addition is pinch-to-grab interaction, which lets the tracked hand pick up, carry, drop, and throw physics objects in the scene, with visual feedback when a pinch registers. Everything is packaged as a complete Unity 6 project with a Windows installation guide, so anyone can clone it and run it.

## Requirements

- Windows 10 or 11 (64-bit)
- A webcam
- [Git](https://git-scm.com/download/win)
- [Python 3.10](https://www.python.org/downloads/windows/) (3.9 to 3.12 also work; 3.13 and newer will not work)
- [Unity Hub](https://unity.com/download) with Unity 6.0 LTS (6000.0.x)

## Installation

All commands are for Windows PowerShell. Run them one line at a time.

### 1. Clone the repository

```powershell
cd $HOME\Desktop
git clone https://github.com/kirb-cloud/opencv-unity-hand-interaction.git
```

### 2. Set up Python

Install Python 3.10 using the 64-bit Windows installer from python.org. In the installer, tick **Add python.exe to PATH**, then click **Install Now**. Close and reopen PowerShell, then confirm it's installed:

```powershell
py -0
```

You should see `-V:3.10` in the list. Other versions may also be listed, which is fine.

Create and activate a virtual environment in the project's `Python` folder. The `Set-ExecutionPolicy` line is only needed once per computer; type `Y` if prompted.

```powershell
cd $HOME\Desktop\opencv-unity-hand-interaction\Python
py -3.10 -m venv venv
Set-ExecutionPolicy -Scope CurrentUser RemoteSigned
.\venv\Scripts\Activate.ps1
python --version
```

Your prompt should now start with `(venv)`, and the version should be `Python 3.10.x`.

Install the packages. Use these exact versions, since newer versions of MediaPipe removed features this project depends on. This can take a few minutes; wait for the line that starts with `Successfully installed`.

```powershell
pip install mediapipe==0.10.21 cvzone==1.6.1 opencv-python
```

Allow camera access in **Settings > Privacy & security > Camera** by turning on **Let desktop apps access your camera**, and close any other apps using the webcam. Then test it:

```powershell
python HandsMain.py
```

A window should open showing your webcam, with dots and lines drawn on your hand. Press **Ctrl+C** in PowerShell to stop it.

### 3. Set up Unity

1. Install Unity Hub, sign in, and go to **Installs > Install Editor**. Choose Unity 6.0 LTS.
2. In Unity Hub, go to **Projects**, click the arrow next to **Add**, and choose **Add project from disk**.
3. Select the `opencv-unity-hand-interaction` folder (the one containing `Assets`) and click **Add Project**.
4. Open the project. The first launch takes several minutes while Unity rebuilds its cache.
5. In the **Project** panel, open the `Assets` folder and double-click the scene to load it.
6. Open the **Console** tab and make sure there are no red errors.

### 4. Run it

Start Unity first, then Python.

1. In Unity, press **Play**. If Windows Firewall asks about Unity, click **Allow**.
2. In PowerShell, activate the virtual environment and run the script:
```powershell
   cd $HOME\Desktop\opencv-unity-hand-interaction\Python
   .\venv\Scripts\Activate.ps1
   python HandsMain.py
```
3. Hold your hand up to the webcam and watch the **Game** view in Unity. Pinch your thumb and index finger near a cube to pick it up, and open your fingers to drop it.

To stop, press **Ctrl+C** in PowerShell, then press **Play** again in Unity.

Each time you open a new PowerShell window, you need to `cd` into the `Python` folder and activate the virtual environment again before running the script.
