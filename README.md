# sshiva
A quickly written C# application that allows you to run SSH commands against a host or list of hosts. 

Targeted for .NET 4.0 hosts, should work fine on Win10+, but ensure the proper .NET version exists on any earlier versions.

# Usage
```
Execute a command using a password:

sshiva.exe /hosts:192.168.2.5,192.168.2.12,192.168.2.13 /user:checkymander /password:"P@ssw0rd" /command:"whoami"

sshiva.exe /hosts:192.168.2.5,192.168.2.12,192.168.2.13 /user:checkymander /b64:"UEBzc3cwcmQ=" /command:"whoami"


Execute a command using a key:

sshiva.exe /hosts:192.168.2.5,192.168.2.12,192.168.2.13 /user:checkymander /key:"C:\users\checkymander\.ssh\checkymanderkey" /command:"whoami"
```
