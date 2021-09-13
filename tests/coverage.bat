cd .

dotnet build .\Application.Unit.Tests\Application.Unit.Tests.csproj

coverlet .\Application.Unit.Tests\bin\Debug\net5.0\Application.Tests.dll --target "dotnet" --targetargs "test --no-build" --exclude "[*]Application.Unit.Tests*" --output ".\Application.Unit.Tests\coverage.json"

dotnet build .\CalculoJurosService.Integration.Tests\CalculoJurosService.Integration.Tests.csproj

coverlet .\CalculoJurosService.Integration.Tests\bin\Debug\net5.0\CalculoJurosService.Integration.Tests.dll --target "dotnet" --targetargs "test --no-build" --exclude "[*]CalculoJurosService.Integration.Tests*" --output ".\CalculoJurosService.Integration.Tests\coverage.json"

dotnet build .\TaxaJurosService.Integration.Tests\TaxaJurosService.Integration.Tests.csproj

coverlet .\TaxaJurosService.Integration.Tests\bin\Debug\net5.0\Application.Tests.dll --target "dotnet" --targetargs "test --no-build" --exclude "[*]TaxaJurosService.Integration.Tests*" --output ".\TaxaJurosService.Integration.Tests"