# OSCmd

**OSCmd** is a library that enables easy shell command execution and system control across multiple operating systems.

> OSCmd is currently released as a preview version. Feedback through issues or pull requests would be appreciated.

---
## Features

- Platform-specific command execution (sync/async)
- Operating system information retrieval
- System controls
    - Reboot
    - Shutdown
    - Sleep

## Installation

1. Open Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:

 ```
 https://github.com/ctrl3d/OSCmd.git
 ```

Alternatively, open Packages/manifest.json and add the following to the dependencies block:

```json
{
    "dependencies": {
      "work.ctrl3d.oscmd": "https://github.com/ctrl3d/OSCmd.git"
  }
}
```

## Usage

```csharp
csharp using work.ctrl3d.OS;

var osCmd = new OSCmd();
string output = osCmd.Run("echo", "hello world");
// string asyncOutput = await osCmd.RunAsync("echo", "hello async"); // Async

///System control
osCmd.System.Reboot();
osCmd.System.Suhtdown();
osCmd.System.Sleep();

```
---

## Contribution

Issues and PRs are always welcome!