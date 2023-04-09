## Logi-SetRGB

Command line program that allows external application to set colours on a Logitech G LightSync RGB keyboard.

The program opens up a named pipe and listens for commands.

This is a very rudimentary program as I have no expeience in C#. I threw this together so that I can change the colours from PowerShell.

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
