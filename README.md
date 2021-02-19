# JsonApiDotNetCore.Demo.Auth



## Installation

1. Restore NuGet packages with `dotnet restore` (or let your IDE handle this for you).
2. Spin up a postgres docker container.
3. Replace the connection string placeholders with the right values; for the username, database name and password.

## Usage

1. Run the `JsonApiDotNetCore.Demo.Auth.Api` project with `dotnet run` (or run it through your IDE).
2. Send an HTTP request through your favorite browser or API client software to `https://localhost:5001/api/cars/{id}`

## Authentication
Enable authentication by supplying an API key with the request.

### Query method:
```
https://localhost:5001/api/cars/{id}?apiKey={VALUE}
```

### Header method:
```
Authorization: 'Bearer {VALUE}'
```

### Available keys:
- `HpjtmVYExiltL1lmtGbvqUr0mUQ1Ngke`
- `pTXohlfeqMCWxyTqtfNqb6QeeAWgdEpV`

See `KeyRepository` for the implementation of this key store.