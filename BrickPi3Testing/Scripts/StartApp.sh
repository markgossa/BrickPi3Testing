/home/pi/.dotnet/dotnet /home/pi/Apps/net5.0-windows10.0.17763.0/BrickPi3Testing2.dll &>/dev/null &
ps aux | grep 'dotnet' | sed -n '2 p' | awk '{print $2}'
read -t 3