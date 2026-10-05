# OpenCV + Unity Hand Interaction

Real-time webcam hand tracking with **OpenCV** and **MediaPipe**, streamed into **Unity** to control a virtual 3D hand that can grab and interact with objects.

> Based on [BIGBOSS-dedsec/OpenCV-Unity-To-Build-3DHands](https://github.com/BIGBOSS-dedsec/OpenCV-Unity-To-Build-3DHands).

---

## Requirements

- Windows 10 or 11 (64-bit)
- A webcam
- [Git](https://git-scm.com/download/win)
- [Python **3.10**](https://www.python.org/downloads/windows/) (3.9–3.12 also work; **3.13 and newer will not work**)
- [Unity Hub](https://unity.com/download) with **Unity 6.0 LTS** (6000.0.x)

---

## Installation

All commands are for **Windows PowerShell**. Run each command on its own line and press **Enter** before running the next one.

### 1. Clone the repository

```powershell
cd $HOME\Desktop
```
```powershell
git clone https://github.com/kirb-cloud/opencv-unity-hand-interaction.git
```

### 2. Set up Python

**Install Python 3.10.** Download the 64-bit Windows installer from python.org. In the installer, tick **"Add python.exe to PATH"**, then click **Install Now**. Close and reopen PowerShell, then check it's installed:

```powershell
py -0
```

You should see `-V:3.10` in the list. It's fine if other versions are also listed.

**Create a virtual environment** in the project's `Python` folder:

```powershell
cd $HOME\Desktop\opencv-unity-hand-interaction\Python
```
```powershell
py -3.10 -m venv venv
```

**Allow PowerShell to run the activation script** (only needed once per computer). Type `Y` if prompted:

```powershell
Set-ExecutionPolicy -Scope CurrentUser RemoteSigned
```

**Activate the virtual environment:**

```powershell
.\venv\Scripts\Activate.ps1
```

Your prompt should now start with `(venv)`. Check the version:

```powershell
python --version
```

It should say `Python 3.10.x`.

**Install the packages:**

```powershell
pip install mediapipe==0.10.21 cvzone==1.6.1 opencv-python
```

> Use these exact versions. Newer versions of MediaPipe removed features this project depends on.

This can take a few minutes. Wait for the line that starts with `Successfully installed`.

**Allow camera access.** Go to **Settings → Privacy & security → Camera** and turn on **Let desktop apps access your camera**. Close any other apps using the webcam.

**Test it:**

```powershell
python HandsMain.py
```

A window should open showing your webcam, with dots and lines drawn on your hand. Press **Ctrl+C** in PowerShell to stop it.

### 3. Set up Unity

1. Install **Unity Hub**, sign in, and go to **Installs → Install Editor**. Choose **Unity 6.0 LTS**.
2. In Unity Hub, go to **Projects**, click the arrow next to **Add**, and choose **Add project from disk**.
3. Select the `opencv-unity-hand-interaction` folder (the one containing `Assets`) and click **Add Project**.
4. Open the project. The first launch takes several minutes while Unity rebuilds its cache.
5. In the **Project** panel, open the `Assets` folder and double-click the scene to load it.
6. Open the **Console** tab and make sure there are no red errors.

### 4. Run it

Start Unity first, then Python.

1. In Unity, press **▶ Play**. If Windows Firewall asks about Unity, click **Allow**.
2. In PowerShell, activate the virtual environment and run the script:

```powershell
cd $HOME\Desktop\opencv-unity-hand-interaction\Python
```
```powershell
.\venv\Scripts\Activate.ps1
```
```powershell
python HandsMain.py
```

3. Hold your hand up to the webcam and watch the **Game** view in Unity. Pinch your thumb and index finger near a cube to pick it up, and open your fingers to drop it.

To stop, press **Ctrl+C** in PowerShell, then press **▶ Play** again in Unity.

> Each time you open a new PowerShell window, you need to `cd` into the `Python` folder and activate the virtual environment again before running the script.
