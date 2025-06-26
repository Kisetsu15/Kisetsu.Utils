# Kisetsu.Utils

A lightweight and modular C# utility library offering common helper functions for console interaction, hashing, string manipulation, AES encryption / decryption and JSON file operations.

---

## 📦 Features

- **📚 StringExtensions**
  - Flexible character stripping by mode or character set.
  - Supports removal of substrings, alphanumerics, symbols, and whitespace.
- **🔑 AES**
  - Provides AES encryption and decryption utilities using a passphrase and salt.
  - Generates a cryptographically secure random salt.

- **🧮 Hash**
  - SHA-1, SHA-256, and SHA-512 hashing for any string and stream input.
  - Outputs string or byte array with exception handing.

- **📂 Json**
  - Load and save JSON files to/from `Dictionary<string, T>` using Newtonsoft.Json.
  - Auto-creates directories on save.

- **🖥️ Terminal**
  - Colored console input/output utilities.
  - Type-safe generic `Input<T>()` parsing.
  - `Log()` with timestamp, file, method, and line number using `[CallerInfo]`.

---

## 🧰 Requirements

- .NET 6.0 or later
- Newtonsoft.Json package

Install via NuGet:

```bash
dotnet add package Newtonsoft.Json
````

---

## 🚀 Quick Start

```csharp
using Kisetsu.Utils;

// Strip unwanted characters
string clean = "Hello, World!".Strip(StripMode.Alphanumeric); // => "HelloWorld"

// Compute hash
string sha1 = Hash.Compute("password123", Algorithm.SHA1);

// JSON I/O
var data = Json.Load<int>("settings.json");
Json.Save("backup.json", data);

// Colored log
Terminal.Log("Initialized successfully");

// AES encryption/decryption
var salt = AES.GenerateSalt();
string encrypted = AES.Encrypt("Hello World","mySecretPassphrase", salt);
string decrypted = AES.Decrypt(encrypted, "mySecretPassphrase");
```

---

## 🔧 StripMode Reference

| Mode           | Behavior                              |
| -------------- | ------------------------------------- |
| `None`         | Returns input unchanged               |
| `Whitespace`   | Removes spaces, tabs, and line breaks |
| `Alphanumeric` | Keeps letters and digits only         |
| `Alphabets`    | Keeps letters only                    |
| `Numeric`      | Keeps digits only                     |
| `Symbols`      | Keeps punctuation/symbols only        |

---

## 🧪 Logging Example

```csharp
Terminal.Log("Something happened!");
```

Output:

```
[12:45:32 :: Program.cs :: Main() :: Line 24] Something happened!
```

---

## 📁 Project Structure

```
Kisetsu.Utils/
├── Hash.cs
├── Json.cs
├── StringExtensions.cs
├── AES.cs
└── Terminal.cs
```

---

## 📝 License

MIT License.

---

## ✨ Author

Kisetsu (Dharshik)


