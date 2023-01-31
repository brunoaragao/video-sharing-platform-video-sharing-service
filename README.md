# Video Sharing Service

This is a video sharing service that allows users to upload videos and share them with other users. The service also provides a Swagger UI to test the API.

Created as part of the [Alura Challenge](https://www.alura.com.br/challenge-back-end) 2021. :rocket:

## Getting Started

:warning: This project is a submodule of the [video-sharing-platform](github.com/brunoaragao/video-sharing-platform) project. If you want to run the entire project, please follow the instructions in the main repository.

If you want to run this service only, please follow the instructions below.

### Prerequisites
1. Install the .NET 7 SDK from [here](https://dotnet.microsoft.com/download/dotnet/7.0)
2. Install the dotnet-user-secrets tool using the command `dotnet tool install --global dotnet-user-secrets`

### Setup
1. Clone this repository
2. In the project's root folder, run the following command to set the connection string for the PostgreSQL database:
    ```
    dotnet user-secrets set "ConnectionStrings:VideoSharingConnection" "Host=localhost;Database=vsp-videosharing-service;Username=postgres;Password=postgres;"
    ```
    *Note: the connection string above is for a local PostgreSQL instance running on the default port with the default username and password. You can change it to match your local setup.*

## How to run
Run `dotnet watch run` in the project's root folder.

## How to use

The service exposes a REST API that can be used to authenticate users. The API is documented using Swagger and can be accessed at the `/swagger` endpoint.


## How to test
The service has a set of unit tests that can be run using the `dotnet test` command.

## Built With
- [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) - The web framework used
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM
- [JWT](https://jwt.io/) - Authentication
- [Swagger](https://swagger.io/) - API documentation
- [xUnit](https://xunit.net/) - Unit testing framework

## Acknowledgments

- [Alura](https://www.alura.com.br/)

## Authors

- **[Bruno Aragão](github.com/brunoaragao)**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.