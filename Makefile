all: run

run: release
	exec _260Weeks/bin/Release/netcoreapp2.1/_260Weeks.dll

release:
	msbuild _260Weeks/ -property:Configuration=Release
	chmod 755 _260Weeks/bin/Release/netcoreapp2.1/_260Weeks.dll

debug:
	msbuild _260Weeks/ -property:Configuration=Debug
	chmod 755 _260Weeks/bin/Debug/netcoreapp2.1/_260Weeks.dll

clean:
	rm -rf _260Weeks/bin
