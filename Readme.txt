Run Sonar Cube
----------------
dotnet sonarscanner begin /k:"BracketGenerator" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="sqp_ff689140ea166aadeb3ba6faa02c82ca484c56a8"
dotnet build
dotnet sonarscanner end /d:sonar.login="sqp_ff689140ea166aadeb3ba6faa02c82ca484c56a8"  