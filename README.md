# moxy
Moxy (Mocking Proxy) is a reverse proxy that will proxy a single call to any backend path, cache the result.

Any further calls to a previously proxied backend path will then return the exact same result for 10 mins.

There is also the option to manually purge the cache using the /_/purge [Post] endpoint

## Configuration
All you need to do is set the `BackendToMock/Address` value in the `appsettings.json`

## Requirements
.NET 7 (At least preview 6) 
