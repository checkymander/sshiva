# sshiva
A quickly written C# application that allows you to run SSH commands against a host or list of hosts.


# Usage
```
Execute a command using a password:

sshiva.exe /hosts=192.168.2.5,192.168.2.12,192.168.2.13 /user=checkymander /password="P@ssw0rd" /command="whoami"


Execute a command using a key:

sshiva.exe /hosts=192.168.2.5,192.168.2.12,192.168.2.13 /user=checkymander /key="C:\users\checkymander\.ssh\checkymanderkey" /command="whoami"
```
