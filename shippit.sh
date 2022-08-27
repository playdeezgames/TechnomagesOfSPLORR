./publishSchema.sh
dotnet publish ./src/TechnomagesOfSPLORR/TechnomagesOfSPLORR.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/TechnomagesOfSPLORR/TechnomagesOfSPLORR.vbproj -o ./pub-windows -c Release --sc -r win-x64
dotnet publish ./src/TOS.Editor/TOS.Editor.vbproj -o ./pub-linux-editor -c Release --sc -r linux-x64
dotnet publish ./src/TOS.Editor/TOS.Editor.vbproj -o ./pub-windows-editor -c Release --sc -r win-x64
butler push pub-windows thegrumpygamedev/technomages-of-splorr:windows
butler push pub-linux thegrumpygamedev/technomages-of-splorr:linux
butler push pub-windows-editor thegrumpygamedev/technomages-of-splorr:windows-editor
butler push pub-linux-editor thegrumpygamedev/technomages-of-splorr:linux-editor

