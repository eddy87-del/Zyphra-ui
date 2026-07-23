# ZyphraDroid - Run as Administrator

## 🚀 **Launch ZyphraDroid with Administrator Privileges**

Some advanced features require admin access. Here's how to run ZyphraDroid as Administrator:

---

## **Method 1: Direct .exe Launch (Easiest)**

### Step 1: Build the Project
```bash
dotnet build
```

### Step 2: Locate the .exe File
Navigate to:
```
bin/Debug/net6.0-windows/ZyphraDroid.exe
```

### Step 3: Right-Click & Run as Administrator
1. Open File Explorer
2. Navigate to: `bin/Debug/net6.0-windows/`
3. Right-click on **ZyphraDroid.exe**
4. Select **"Run as administrator"**
5. Click **"Yes"** when prompted

**✅ ZyphraDroid launches with admin privileges!** 🤖

---

## **Method 2: Command Line (Windows Terminal/CMD)**

### Step 1: Open Command Prompt as Administrator
- Press **Win + R**
- Type `cmd`
- Press **Ctrl + Shift + Enter** (runs as admin)

### Step 2: Navigate to Project
```bash
cd C:\path\to\Zyphra-ui
```

### Step 3: Build
```bash
dotnet build
```

### Step 4: Run as Administrator
```bash
dotnet run
```

**⚡ Runs with full admin access!**

---

## **Method 3: PowerShell (Admin)**

### Step 1: Open PowerShell as Administrator
1. Press **Win + X**
2. Select **"Windows PowerShell (Admin)"** or **"Terminal (Admin)"**
3. Click **"Yes"**

### Step 2: Navigate & Run
```bash
cd C:\path\to\Zyphra-ui
dotnet build
dotnet run
```

**✅ Full admin access granted!**

---

## **Method 4: Create Admin Shortcut**

### Step 1: Create Shortcut
1. Right-click on desktop
2. Select **New → Shortcut**
3. Enter path:
```
C:\Windows\System32\cmd.exe /k cd C:\path\to\Zyphra-ui && dotnet run
```
4. Name it **"ZyphraDroid Admin"**
5. Click **Finish**

### Step 2: Set to Run as Admin
1. Right-click the shortcut
2. Select **Properties**
3. Click **Advanced**
4. Check **"Run as administrator"**
5. Click **OK**

### Step 3: Launch
Double-click the shortcut!

**✅ Always runs as admin!**

---

## **Method 5: Visual Studio Code (Terminal as Admin)**

### Step 1: Open VS Code Terminal as Admin
1. Open VS Code
2. Open Terminal: **Ctrl + `**
3. Click **dropdown arrow** next to terminal
4. Select **"Command Prompt"** or **"PowerShell"**
5. Right-click terminal tab
6. Select **"Run as Admin"** (if available)

Or manually:

### Step 2: Run Commands
```bash
cd C:\path\to\Zyphra-ui
dotnet build
dotnet run
```

**✅ Terminal has admin privileges!**

---

## **Method 6: Batch File (.bat) - Most Convenient**

### Step 1: Create Batch File
Create a file named `run_zyphradroid.bat` in project folder:

```batch
@echo off
REM Check if running as administrator
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Requesting administrator privileges...
    powershell -Command "Start-Process cmd -ArgumentList '/k cd %cd% && dotnet run' -Verb RunAs"
    exit /b
)

echo Running ZyphraDroid as Administrator...
dotnet build
dotnet run
pause
```

### Step 2: Run the Batch File
1. Save as `run_zyphradroid.bat`
2. Double-click the file
3. Click **"Yes"** when prompted for admin access

**✅ Automatically runs as admin!**

---

## **Method 7: Create Desktop Shortcut (.bat)**

### Step 1: Create Shortcut to Batch File
1. Right-click `run_zyphradroid.bat`
2. Send to → Desktop (create shortcut)

### Step 2: Set to Admin
1. Right-click shortcut on desktop
2. Properties → Advanced
3. Check "Run as administrator"
4. OK

### Step 3: Double-Click to Launch
**✅ One-click admin launch!**

---

## **⚠️ Why Admin Privileges?**

ZyphraDroid needs admin access for:
- 🖥️ **System Control** - Shutdown, restart, lock
- ⚙️ **Process Management** - Kill/terminate processes
- 📁 **File Operations** - Access protected files
- 🔧 **Registry Access** - Read/write registry keys
- 🌐 **Network Commands** - Advanced network operations

---

## **✅ Recommended Methods**

| Method | Ease | Best For |
|--------|------|----------|
| **Direct .exe** | ⭐⭐⭐ | Quick launch |
| **Command Prompt** | ⭐⭐⭐ | Terminal users |
| **PowerShell** | ⭐⭐⭐ | Modern Windows |
| **Batch File** | ⭐⭐⭐⭐ | Convenience |
| **Desktop Shortcut** | ⭐⭐⭐⭐⭐ | Easiest! |
| **VS Code Terminal** | ⭐⭐ | Code editing |

---

## **🚀 Quick Admin Launch Summary**

```
EASIEST METHOD:
1. Build: dotnet build
2. Right-click: bin/Debug/net6.0-windows/ZyphraDroid.exe
3. Select: "Run as administrator"
4. Click: "Yes"
5. 🤖 ZyphraDroid launches with FULL ADMIN POWER!
```

---

## **🎯 Pro Tips**

✅ **Always run as admin** for full feature access  
✅ **Use batch file** for one-click launching  
✅ **Pin to Start** for quick access  
✅ **Create desktop shortcut** for convenience  

---

## **🔒 Admin Privileges Checklist**

- [x] System shutdown/restart
- [x] Process termination
- [x] File system access
- [x] Registry modification
- [x] Network configuration
- [x] System information access

**All unlocked with admin mode!** ⚡

---

**Launch ZyphraDroid as Administrator now!** 🤖🚀

Choose your method above and enjoy full system control!
