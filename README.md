# Bank exercise

## About
Api built using C# .Net Core 8
UI built using Vue 3


## How to develop

This has been developed using windows, so the following commands are for Windows.

For testing and running E2E tests, keep in mind that the porject is using an InMemoryDatabase for simplicity and the DB clears once the API restarts.

### Pre-requisites

- .Net 8.0 SDK - get it from here https://dotnet.microsoft.com/en-us/download
- npm
- node

### Start the app locally

To start the API, navigate to `./bank-api` folder on your terminal and run
```bash
dotnet restore
dotnet run --launch-profile http
```

To start the UI, navigate to `./bank-ui` and run
```bash
npm i
npm run dev
```

Once the API is running, you can check the API definition in `http://localhost:5045/swagger/index.html`
The UI should be running in `http://localhost:5173/`

## Run E2E tests

Firt, start the app locally, then, to run playwright in dev mode, open another terminal and run the following commands:

Install playwright:
```bash
npm playwright install
```

Run playwright in dev/UI mode
```bash
npm playwright test --ui
```
