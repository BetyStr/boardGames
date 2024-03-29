podman login cerit.io
dotnet publish -c Release
podman build -t cerit.io/roman_alexander_mariancik/bookhub-image -f BookHub/Dockerfile .
podman build -t cerit.io/roman_alexander_mariancik/bookhub-api-image -f WebAPI/Dockerfile .
podman push cerit.io/roman_alexander_mariancik/bookhub-image:latest
podman push cerit.io/roman_alexander_mariancik/bookhub-api-image:latest  
