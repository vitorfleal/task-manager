#!/bin/sh

PROJECT=src/TaskManager.Infrastructure
BUILD_PROJECT=src/TaskManager.Api
SQL_CONTEXT_CLASS=TaskManagerContext

dotnet ef database update --project ${PROJECT} --startup-project ${BUILD_PROJECT} --context ${SQL_CONTEXT_CLASS}