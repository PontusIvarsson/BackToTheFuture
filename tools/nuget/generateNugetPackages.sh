''./NuGet.exe -setApiKey [secretkeymust be specified once]
./NuGet.exe pack ../../nuspecs/BackToTheFuture.Hosting.nuspec -outputdirectory ../../artifacts/nupkg
./NuGet.exe push ../../artifacts/nupkg/BackToTheFuture.Hosting.0.1.nupkg
