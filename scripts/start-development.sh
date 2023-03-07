#!/usr/bin/env bash
set -e

PROJECT_DATABASE=src/TaskManager.Infrastructure
SQL_CONTEXT_CLASS=TaskManager.Infrastructure.Contexts.TaskManagerContext
PROJECT_API=src/TaskManager.Api
LAUNCH_PROFILE=TaskManager.Docker

# Apply all migrations
dotnet ef database update --project ${PROJECT_DATABASE} --startup-project ${PROJECT_API} --context ${SQL_CONTEXT_CLASS} --verbose

dotnet watch --project ${PROJECT_API} -- run  --launch-profile ${LAUNCH_PROFILE}
