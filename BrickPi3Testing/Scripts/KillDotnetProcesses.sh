kill -9 $(ps aux | grep 'dotnet' | awk '{print $2}')