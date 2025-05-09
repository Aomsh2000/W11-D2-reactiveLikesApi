# Reactive Like App

A demo application that displays posts and allows users to like them with real-time updates using Server-Sent Events (SSE).

## Tech Stack

- **Backend**: ASP.NET Core 8
  - RESTful API to fetch posts and handle likes
  - Real-time updates via SSE (`/api/subscribe`)

- **Frontend**: Angular
  - Displays posts and like counts
  - Like button triggers backend updates
  - Real-time UI updates using SSE
  - Visual highlight when a post is updated

## Getting Started

### Backend

1. Run the ASP.NET Core API:
   ```bash
   dotnet run
   ```
2. Make sure the API exposes:
```bash
  GET /api/posts
  
  POST /api/posts/{id}/like
  
  GET /api/subscribe (SSE stream)
```
### Frontend
Install dependencies:

```bash
npm install
```
Run the Angular app:

```bash

npm start
```
The app runs at http://localhost:4200 and connects to the backend for data and updates.

### Notes
CORS must be enabled on the backend to allow frontend communication.
EventSource (SSE) requires running in a browser environment — SSR or Node won't support it directly.
