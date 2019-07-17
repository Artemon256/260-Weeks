all: release

run: release
	exec source/bin/Release/The260WeeksGame.exe

release:
	msbuild -p:Configuration=Release
	chmod 755 source/bin/Release/The260WeeksGame.exe

debug:
	mcs -debug source/*.cs source/The260WeeksGame/*.cs -resource:"source/The260WeeksGame/String data/First Names.txt" -resource:"source/The260WeeksGame/String data/Media Names.txt" -resource:"source/The260WeeksGame/String data/Second Names.txt" -resource:"source/The260WeeksGame/String data/Social Groups.xml"

clean:
	rm -rf source/bin
	rm -rf source/obj
	rm source/Program.exe
	rm source/Program.exe.mdb

# build:
# 	msbuild
