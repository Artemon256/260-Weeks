all: release

run: release
	dotnet bin/Release/netcoreapp2.2/_260Weeks.dll

release:
	dotnet build -p:Configuration=Release
	chmod 775 bin/Release/netcoreapp2.2/_260Weeks.dll

debug:
	dotnet build -p:Configuration=Debug
	chmod 775 bin/Debug/netcoreapp2.2/_260Weeks.dll

clean:
	dotnet clean

restore:
	dotnet restore
