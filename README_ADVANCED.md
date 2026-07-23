# Zyphra Advanced Virtual Assistant - Unrestricted Mode

## 🚀 ADVANCED FEATURES - FULL SYSTEM CONTROL

**WARNING: This version has unrestricted system access. Use responsibly!**

### ⚡ Advanced Commands

#### 🖥️ **System Control**
- `shutdown` - Shut down computer in 30 seconds
- `restart` - Restart system in 30 seconds
- `lock` - Lock workstation immediately
- `sleep` - Put computer to sleep mode
- `cancel shutdown` - Cancel pending shutdown

#### ⚙️ **Process Management**
- `kill process [name]` - Terminate specific process
- `list processes` - Show all running processes
- `close [app name]` - Close application

#### 📁 **File Operations**
- `create file [path]` - Create new file
- `delete file [path]` - Delete file permanently
- `read file [path]` - Read file contents
- `write file [path]` - Write to file

#### 🌐 **Network & Connectivity**
- `ip address` - Get system IP address
- `ping [host]` - Test connectivity
- `dns lookup [domain]` - Resolve DNS
- `network info` - Display network details

#### ���� **System Information**
- `disk space` - Show all drive information
- `installed programs` - List installed software
- `system info` - CPU, RAM, OS details
- `environment` - System path and variables

#### 🔧 **Registry Access**
- `registry read [key]` - Read registry value
- `registry write [key] [value]` - Modify registry
- `system keys` - Access system registry

#### 📱 **Applications**
- `open notepad`, `open calculator`, `open paint`
- `open explorer`, `open powershell`, `open cmd`
- `open task manager`, `open media player`

#### 😄 **Fun Commands**
- `joke` - Programming jokes
- `help` - Show all commands

### 📋 System Requirements

- Windows 10/11 (32-bit or 64-bit)
- .NET 6.0 Runtime
- Administrator privileges (recommended for full features)
- Microphone (optional)
- Speakers (for audio responses)

### 🚨 Safety Warnings

⚠️ **Be careful with these commands:**
- **Shutdown/Restart** - Will close all programs
- **Kill Process** - Can crash system if critical process terminated
- **Delete File** - Permanent data loss
- **Registry Modification** - Can break Windows if modified incorrectly
- **Network Commands** - Requires proper permissions

### 🔐 Responsible Use

1. Always understand a command before executing
2. Backup important files before file operations
3. Avoid killing system processes
4. Use administrator mode for sensitive operations
5. Test with safe commands first

### 💻 Advanced Usage Examples

```
"what is my IP address?"
→ Returns system IP and network info

"list all running processes"
→ Shows all active processes with details

"disk space"
→ Shows free/used space on all drives

"open task manager"
→ Launches Windows Task Manager

"restart in 30 seconds"
→ Initiates system restart

"lock the computer"
→ Immediately locks workstation
```

### 🛠️ Technical Details

**Built with:**
- C# with .NET 6.0
- Windows Presentation Foundation (WPF)
- System.Diagnostics for process control
- System.IO for file operations
- System.Net for network operations
- Windows Registry API access

**Capabilities:**
- Direct system process execution
- File system manipulation
- Network interface access
- Registry read/write
- Environment variable access
- System shutdown/restart functions

### ⚠️ Legal & Ethical Notice

This tool is provided for legitimate system administration and personal use. Users are responsible for:
- Compliance with local laws and regulations
- Proper authorization and permissions
- Ethical use of system commands
- Backup and data protection
- Security implications

### 🔒 Data & Privacy

- All operations execute locally
- No data sent to external servers
- Logs stored in `%AppData%\Zyphra\logs.txt`
- Complete audit trail of all commands
- User assumes all responsibility

### 🐛 Troubleshooting

**Administrator Required:** Some commands need elevated privileges
- Run Visual Studio as Administrator
- Run executable with "Run as Administrator"

**Registry Access Denied:** Use Admin mode

**Process Won't Terminate:** System protection - use Task Manager

### 📞 Support

For advanced features or custom commands, review system documentation and Windows API references.

---

**⚡ Zyphra Advanced - Full System Control**  
*Unrestricted. Powerful. Use Responsibly.*

**Disclaimer:** This software is provided "as-is". Users accept all risks and responsibility for system changes.
