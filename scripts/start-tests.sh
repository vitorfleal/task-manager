#!/bin/sh

source ./scripts/apply-migrations.sh

dotnet test tests/TaskManager.Tests/ --configuration Release --logger trx --logger "console;verbosity=normal"