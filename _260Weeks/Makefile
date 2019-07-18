all: release

run: release
	chmod 755 bin/Release/netcoreapp2.2/_260Weeks.dll
	exec bin/Release/netcoreapp2.2/_260Weeks.dll

release:
	dotnet build --configuration:Release

debug:
	mcs -debug *.cs GameLogic/*.cs

clean:
	rm Program.exe Program.exe.mdb
	dotnet clean --configuration:Release
