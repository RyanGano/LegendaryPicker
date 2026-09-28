# LegendaryPicker

LegendaryPicker is a React and TypeScript web application with a C# Minimal API backend.

## Projects

- `LegendaryPickerApp` — Vite, React, and TypeScript frontend.
- `LegendaryPickerService` — ASP.NET Core Minimal API.

## Run locally

Start the frontend from `LegendaryPickerApp` with `npm install` followed by `npm run dev`.

Start the backend from the repository root with `dotnet run --project LegendaryPickerService`. The health endpoint is available at `/api/health`, and `/api/setup?players=3` returns a random legal setup for 1 to 5 players (30 requests per minute per client).

## GitHub Pages

The frontend is built and deployed automatically to GitHub Pages when changes are pushed to `main`. The site URL is https://ryangano.github.io/LegendaryPicker/.

## Azure App Service

`.github/workflows/deploy-api.yml` deploys the API to the App Service web app named by the `AZURE_WEBAPP_NAME` repository variable when a push to `main` changes the service. The tests run first, and a failure stops the deploy.
