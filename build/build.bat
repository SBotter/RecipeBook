cd ../src/RecipeBook.API

dotnet restore
dotnet build --no-restore
dotnet publish -o ../../deploy