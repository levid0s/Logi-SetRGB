## Logi-SetRGB

Command line program that allows external application to set colours on a Logitech G LightSync RGB keyboard.

The program opens up a named pipe and listens for incoming commands.

This is a very rudimentary program as I have no expeience in C#. I threw this together so that I can change the colours from a PowerShell script.

The following commands are currently supported:

### setkey

The list of key codes can be found in `LogitechGSDK.cs`

```
setkey,$keyId,$red,$green,$blue

example:
setkey,0x0E,255,0,0       # Set Backspace to Red
setkey,0x3b,128,128,128   # Set F1 to gray
```

### setall

Set all keys on the keyboard to a particular colour

```
setall,$red,$green,$blue

example:
setall,0,0,0            # Set all keys to black
setall,255,255,255      # Set all keys to white
```

### shutdown

Shut down the program

```
shutdown
```

### example client (PowerShell)

```powershell
$pipe = New-Object System.IO.Pipes.NamedPipeClientStream('.', $pipeName, [System.IO.Pipes.PipeDirection]::Out)
$pipe.Connect()
$writer = New-Object System.IO.StreamWriter($pipe)
$writer.AutoFlush = $true

$writer.WriteLine("setall,32,32,32")
$writer.WriteLine("setkey,0x3e,160,32,110")
$writer.WriteLine("setkey,0xFFFF1,128,32,110")

$writer.WriteLine("shutdown")

$writer.Close()
$pipe.Close()
```
