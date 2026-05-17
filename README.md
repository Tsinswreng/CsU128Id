## Tsinswreng.CsU128Id

一個面向 `UInt128` 的輕量 ID 工具庫。

它主要提供三件事：

- 一個簡單的 `U128Id` 值類型包裝
- `UInt128` 與 16 字節大端序字節數組之間的互轉
- 一套比十六進制更短的 64 進制字符串編碼

### 特點

- 直接以 `UInt128` 爲核心值，便於與你自己的 ID 類型封裝結合
- 內置自定義 64 進制字母表
- `0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_`
- 提供 `TryLow64BaseToUInt128`，解析失敗時可避免拋異常
- 提供基於 `Ulid` 包的 128 位 ID 生成方式

### 安裝

```bash
dotnet add package Tsinswreng.CsU128Id --version 0.0.1-alpha
```

### 目標框架

當前包目標框架爲：

- `net10.0`

### 快速開始

```csharp
using Tsinswreng.CsU128Id;

var id = U128Id.NewUlid();
var text = id.ToLow64Base();
var bytes = id.ToByteArr();

var parsed = ToolU128Id.Low64BaseToUInt128(text);
var restored = new U128Id(parsed);
```

### API 概覽

#### `U128Id`

`U128Id` 是一個很薄的包裝：

- 持有一個 `UInt128 Value`
- `ToString()` 會輸出自定義 64 進制字符串

```csharp
var id = new U128Id(123);
Console.WriteLine(id.Value);      // 123
Console.WriteLine(id.ToString()); // 1x
```

#### 擴展方法

對任何實現了 `IU128Id` 的結構體，都可以使用：

- `T.NewUlid()`
- `z.ToLow64Base()`
- `z.ToByteArr()`
- `T.FromByteArr(byte[])`

如果你想包裝成自己的業務 ID 類型，可以沿用這套接口。

```csharp
using Tsinswreng.CsU128Id;

public record struct UserId : IU128Id{
	public UInt128 Value { get; set; }
}

var userId = UserId.NewUlid();
var text = userId.ToLow64Base();
```

#### 字節互轉

- `ToolU128Id.ToByteArr(UInt128)`
- `ToolU128Id.ByteArrToUInt128(byte[])`
- `ToolU128Id.BytesToUInt128(Span<byte>)`

字節序採用大端序：

- 前 8 字節爲高 64 位
- 後 8 字節爲低 64 位

#### 自定義 64 進制字符串

此庫提供的不是標準 Base64，而是用於整數表示的 64 進制編碼。

字母表：

```txt
0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_
```

主要方法：

- `ToolU128Id.ToLow64Base(UInt128)`
- `ToolU128Id.Low64BaseToUInt128(string)`
- `ToolU128Id.TryLow64BaseToUInt128(string, out UInt128)`

例子：

```csharp
ToolU128Id.ToLow64Base(0);   // "0"
ToolU128Id.ToLow64Base(1);   // "1"
ToolU128Id.ToLow64Base(63);  // "_"
ToolU128Id.ToLow64Base(64);  // "10"
```

#### Base64Url 互轉

如果你需要與標準 Base64Url 表示互通，也可以使用：

- `ToolU128Id.ToBase64Url(UInt128)`
- `ToolU128Id.Base64UrlToUInt128(string)`

### 異常與返回約定

- `Low64BaseToUInt128` 遇到非法字符時會拋 `ArgumentException`
- `TryLow64BaseToUInt128` 遇到非法字符時返回 `false`
- `BytesToUInt128` 遇到非 16 字節輸入時會拋 `ArgumentException`

### 適用場景

- 想用 `UInt128` 作爲內部主鍵或 ID
- 想得到比十六進制更短、又可直接放進 URL 或文件名的字符串表示
- 想封裝自己的強類型 ID，同時保留簡單的生成與編解碼能力

### v0.0.1-alpha

- 初始 alpha 版本
- 提供 `U128Id`、大端序字節互轉、Low64Base 編解碼、Base64Url 編解碼
